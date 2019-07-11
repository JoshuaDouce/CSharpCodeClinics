using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WeatherData
{
    public class TextFileWeatherDataParser : IWeatherDataParser
    {
        public string SourcePath { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public TextFileWeatherDataParser(string dataDirectory)
        {
            SourcePath = dataDirectory;
            FromDate = null;
            ToDate = null;
        }

        public IEnumerable<WeatherDataRecord> GetWeatherData()
        {
            if (IsValidSource())
            {
                return GetDataFromTextFiles();
            }
            else
            {
                return new List<WeatherDataRecord>();
            }
        }

        private bool IsValidSource()
        {
            if (!Directory.Exists(SourcePath))
            {
                return false;
            }

            if (IsDataFolderEmpty())
            {
                return false;
            }

            return true;
        }

        private List<WeatherDataRecord> GetDataFromTextFiles()
        {
            var data = new List<WeatherDataRecord>();

            var files = FilterFiles(Directory.GetFiles(SourcePath));

            foreach (var file in files)
            {
                LoadDataFromYearlyFile(data, file);
            }

            return FilteredData(data);
        }

        private List<WeatherDataRecord> FilteredData(List<WeatherDataRecord> data)
        {
            if (FromDate == null)
            {
                return data;
            }

            if (FromDate != null && ToDate == null)
            {
                return GetDataForFromDate(data);
            }

            return GetDataForFromAndToDate(data);
        }

        private List<WeatherDataRecord> GetDataForFromAndToDate(List<WeatherDataRecord> data)
        {
            return data
                .Where(x => x.Timestamp.Year >= FromDate.Value.Year && x.Timestamp.Year <= ToDate.Value.Year)
                .Where(x => x.Timestamp.Month >= FromDate.Value.Month && x.Timestamp.Month <= ToDate.Value.Month)
                .Where(x => x.Timestamp.Day >= FromDate.Value.Day && x.Timestamp.Day <= ToDate.Value.Day)
                .ToList();
        }

        private List<WeatherDataRecord> GetDataForFromDate(List<WeatherDataRecord> data)
        {
            return data
                .Where(x => x.Timestamp.Year == FromDate.Value.Year)
                .Where(x => x.Timestamp.Month == FromDate.Value.Month)
                .Where(x => x.Timestamp.Day == FromDate.Value.Day)
                .ToList();
        }

        private string[] FilterFiles(string[] files)
        {
            var filteredFiles = new List<string>();

            if (FromDate == null)
            {
                return files;
            }

            foreach (var file in files)
            {
                if (file.Contains(FromDate.Value.Year.ToString()))
                {
                    filteredFiles.Add(file);
                }
            }

            return filteredFiles.ToArray();
        }

        private void LoadDataFromYearlyFile(List<WeatherDataRecord> data, string file)
        {
            var yearlyDataFile = File.ReadAllLines(file);

            for (int i = 1; i < yearlyDataFile.Length; i++)
            {
                try
                {
                    var record = ParseLine(yearlyDataFile[i]);
                    data.Add(record);
                }
                catch (Exception)
                {
                    Console.WriteLine($"Unable to parse line no: {i} in file: {file}");
                }
            }
        }

        private WeatherDataRecord ParseLine(string line)
        {
            var splitData = line.Split('\t');

            var record = new WeatherDataRecord()
            {
                Timestamp = DateTimeStringParser.ParseDateTimeString(splitData[0]),
                AirTemperature = decimal.Parse(splitData[1]),
                BarometricPressure = decimal.Parse(splitData[2]),
                DewPoint = decimal.Parse(splitData[3]),
                Humidity = decimal.Parse(splitData[4]),
                WindDir = decimal.Parse(splitData[5]),
                WindGust = decimal.Parse(splitData[6]),
                WindSpeed = decimal.Parse(splitData[7])
            };

            return record;
        }

        private bool IsDataFolderEmpty()
        {
            return Directory.GetFiles(SourcePath).Length == 0;
        }
    }
}

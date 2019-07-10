using System;
using System.Collections.Generic;
using System.IO;
using PondOreille.Models;

namespace PondOreille.Parsers
{
    //should read in a collection of text files and convert into the releavnt data objects
    public class TextFileWeatherDataParser : IWeatherDataParser
    {
        public string SourcePath { get; set; }

        public TextFileWeatherDataParser(string dataDirectory)
        {
            SourcePath = dataDirectory;
        }

        public IEnumerable<WeatherDataRecord> GetWeatherData()
        {
            if (!Directory.Exists(SourcePath))
            {
                return new List<WeatherDataRecord>(); ;
            }

            if (IsDataFolderEmpty())
            {
                return new List<WeatherDataRecord>(); ;
            }

            return GetData();
        }

        private List<WeatherDataRecord> GetData()
        {
            var data = new List<WeatherDataRecord>();

            foreach (var file in Directory.GetFiles(SourcePath))
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

            return data;
        }

        private WeatherDataRecord ParseLine(string line)
        {
            var splitData = line.Split('\t');

            var record = new WeatherDataRecord() {
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

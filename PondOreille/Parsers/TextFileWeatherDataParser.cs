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
            var data = new List<WeatherDataRecord>();
            //read files in directory
            if (!Directory.Exists(SourcePath))
            {
                return new List<WeatherDataRecord>();
            }

            if (Directory.GetFiles(SourcePath).Length == 0)
            {
                return new List<WeatherDataRecord>();
            }
            //parse all files into objects

            //return list of data
            return data;
        }
    }
}

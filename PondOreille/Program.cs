using PondOreille.Parsers;
using System;

namespace PondOreille
{
    class Program
    {
        private static readonly string DataSource = 
            @"C:\Users\Doucej\Source\Repos\CSharpCodeClinics\PondOreilleTests\TestFiles\";
        static void Main(string[] args)
        {
            Console.WriteLine("Example usage <fromDate> <toDate>");
            Console.WriteLine("2019_10_12 2019_12_02");

            var parser = new TextFileWeatherDataParser(DataSource);

            try
            {
                parser.FromDate = DateTimeStringParser.ParseDateString(args[0]);
                parser.ToDate = DateTimeStringParser.ParseDateString(args[1]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Unable to Parse provded dates please use format yyyy_mm_dd");
            }

            var data = parser.GetWeatherData();

            Console.WriteLine("Timestamp               |Air Temp|Baro Press|Dew Point|Humidity|Wind Dir|Wind Gust|Wind Speed");
            foreach (var record in data)
            {
                Console.WriteLine(record.ToString());
            }
        }
    }
}
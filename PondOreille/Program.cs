using PondOreille.Parsers;
using System;

namespace PondOreille
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Example usage <fromDate> <toDate>");
            Console.WriteLine(" <2019_10_12> <2019_12_02>");

            try
            {
                var from = DateTimeStringParser.ParseDateString(args[0]);
                var to = DateTimeStringParser.ParseDateString(args[1]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Unable to Parse provded dates please use format yyyy_mm_dd");
            }
        }
    }
}

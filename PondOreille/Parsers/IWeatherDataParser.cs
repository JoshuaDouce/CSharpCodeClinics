using PondOreille.Models;
using System;
using System.Collections.Generic;

namespace PondOreille.Parsers
{
    public interface IWeatherDataParser
    {
        IEnumerable<WeatherDataRecord> GetWeatherData();
    }
}

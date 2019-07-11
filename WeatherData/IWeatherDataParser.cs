using System.Collections.Generic;

namespace WeatherData
{
    public interface IWeatherDataParser
    {
        IEnumerable<WeatherDataRecord> GetWeatherData();
    }
}

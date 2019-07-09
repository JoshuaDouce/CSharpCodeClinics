using System;
using System.Collections.Generic;
using System.Text;

namespace PondOreille.Models
{
    public class WeatherDataRecord
    {
        public DateTime Timestamp { get; set; }
        public decimal AirTemperature { get; set; }
        public decimal BarometricPressure { get; set; }
        public decimal DewPoint { get; set; }
        public decimal Humidity { get; set; }
        public decimal WindDir { get; set; }
        public decimal WindGust { get; set; }
        public decimal WindSpeed { get; set; }
    }
}

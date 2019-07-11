using NUnit.Framework;
using PondOreille;
using PondOreille.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PondOreilleTests
{
    class UtilsTests
    {
        [Test]
        public void GetCoefficent_Returns_CorrectValue() {
            //act
            var data = new List<WeatherDataRecord>();
            data.Add(new WeatherDataRecord() {
                Timestamp = new DateTime(2012, 01, 01, 01, 01, 01),
                AirTemperature = 10,
                BarometricPressure = 10,
                DewPoint = 10,
                Humidity = 50, 
                WindDir = 10,
                WindGust = 10,
                WindSpeed = 10
            });;
            data.Add(new WeatherDataRecord()
            {
                Timestamp = new DateTime(2012, 01, 01, 02, 01, 01),
                AirTemperature = 10,
                BarometricPressure = 10.5m,
                DewPoint = 10,
                Humidity = 50,
                WindDir = 10,
                WindGust = 10,
                WindSpeed = 10
            }); ;
            data.Add(new WeatherDataRecord()
            {
                Timestamp = new DateTime(2012, 01, 01, 03, 01, 01),
                AirTemperature = 10,
                BarometricPressure = 12,
                DewPoint = 10,
                Humidity = 50,
                WindDir = 10,
                WindGust = 10,
                WindSpeed = 10
            }); ;
            data.Add(new WeatherDataRecord()
            {
                Timestamp = new DateTime(2012, 01, 01, 04, 01, 01),
                AirTemperature = 10,
                BarometricPressure = 14,
                DewPoint = 10,
                Humidity = 50,
                WindDir = 10,
                WindGust = 10,
                WindSpeed = 10
            }); ;
            data.Add(new WeatherDataRecord()
            {
                Timestamp = new DateTime(2012, 01, 01, 05, 01, 01),
                AirTemperature = 10,
                BarometricPressure = 14.5m,
                DewPoint = 10,
                Humidity = 50,
                WindDir = 10,
                WindGust = 10,
                WindSpeed = 10
            }); ;
            data.Add(new WeatherDataRecord()
            {
                Timestamp = new DateTime(2012, 01, 01, 06, 01, 01),
                AirTemperature = 10,
                BarometricPressure = 16.5m,
                DewPoint = 10,
                Humidity = 50,
                WindDir = 10,
                WindGust = 10,
                WindSpeed = 10
            }); ;
            data.Add(new WeatherDataRecord()
            {
                Timestamp = new DateTime(2012, 01, 01, 07, 01, 01),
                AirTemperature = 10,
                BarometricPressure = 20,
                DewPoint = 10,
                Humidity = 50,
                WindDir = 10,
                WindGust = 10,
                WindSpeed = 10
            });

            var result = Utils.GetCoeffectient(data, new DateTime(2012, 01, 01, 01, 01, 01));

            //assert
            Assert.That(result, Is.Not.EqualTo(0));
            Assert.That(result, Is.EqualTo(0.59560229445506685));
        }
    }
}

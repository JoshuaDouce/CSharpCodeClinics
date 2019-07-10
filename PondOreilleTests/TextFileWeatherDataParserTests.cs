using NUnit.Framework;
using PondOreille.Models;
using PondOreille.Parsers;
using System;
using System.Collections.Generic;
using System.IO;

namespace PondOreilleTests
{
    public class TextFileWeatherDataParserTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetWeatherData_InvalidDirectory_Returns_EmptyList()
        {
            //arrange
            var parser = new TextFileWeatherDataParser("NoSuchPath");

            //act
            var result = parser.GetWeatherData();

            //assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetWeatherData_WithEmptyDataDirectory_Returns_EmptyList()
        {
            //arrange
            var dir = Directory.CreateDirectory("Test");
            var parser = new TextFileWeatherDataParser(dir.FullName);

            //act
            var result = parser.GetWeatherData();

            //assert
            Assert.That(result, Is.Empty);

            //cleanup
            dir.Delete();
        }

        [Test]
        public void GetWeatherData_WithValidData_Returns_PopulatedList()
        {
            //arrange
            var dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent;
            var testFilesDir = dir.GetDirectories("TestFiles")[0];
            var parser = new TextFileWeatherDataParser(testFilesDir.FullName);

            //act
            var result = parser.GetWeatherData();

            //assert
            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        public void GetWeatherData_WithValidData_Returns_CorrectlyParsedRecord() {
            //arrange
            var dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent;
            var testFilesDir = dir.GetDirectories("TestFiles")[0];
            var parser = new TextFileWeatherDataParser(testFilesDir.FullName);

            //act
            var result = parser.GetWeatherData() as List<WeatherDataRecord>;

            //assert
            Assert.That(result[0].Timestamp, Is.EqualTo(new DateTime(2012, 1, 1, 0, 2, 14)));
            Assert.That(result[0].AirTemperature, Is.EqualTo(34.3));
            Assert.That(result[0].BarometricPressure, Is.EqualTo(30.5));
            Assert.That(result[0].DewPoint, Is.EqualTo(26.9));
            Assert.That(result[0].Humidity, Is.EqualTo(74.2));
            Assert.That(result[0].WindDir, Is.EqualTo(346.4));
            Assert.That(result[0].WindGust, Is.EqualTo(11));
            Assert.That(result[0].WindSpeed, Is.EqualTo(3.6));
        }
    }
}
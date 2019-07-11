using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherData;

namespace WeatherDataTests
{
    [TestClass]
    public class WeatherDataParserTests
    {
        private DirectoryInfo TestDataFolder;
        private TextFileWeatherDataParser TextDataParser;

        [TestInitialize]
        public void Setup()
        {
            TestDataFolder =
                Directory
                .GetParent(Directory.GetCurrentDirectory()).Parent
                .GetDirectories("TestFiles")[0];
            TextDataParser = new TextFileWeatherDataParser(TestDataFolder.FullName);
        }

        [TestMethod]
        public void GetWeatherData_InvalidDirectory_Returns_EmptyList()
        {
            //arrange
            var parser = new TextFileWeatherDataParser("NoSuchPath");

            //act
            var result = parser.GetWeatherData() as List<WeatherDataRecord>;

            //assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetWeatherData_WithEmptyDataDirectory_Returns_EmptyList()
        {
            //arrange
            var dir = Directory.CreateDirectory("Test");
            var parser = new TextFileWeatherDataParser(dir.FullName);

            //act
            var result = parser.GetWeatherData() as List<WeatherDataRecord>;

            //assert
            Assert.AreEqual(0, result.Count);

            //cleanup
            dir.Delete();
        }

        [TestMethod]
        public void GetWeatherData_WithValidData_Returns_PopulatedList()
        {
            //act
            var result = TextDataParser.GetWeatherData() as List<WeatherDataRecord>;

            //assert
            Assert.AreNotEqual(0, result.Count);
        }

        [TestMethod]
        public void GetWeatherData_WithValidData_Returns_CorrectlyParsedRecord()
        {
            //act
            var result = TextDataParser.GetWeatherData() as List<WeatherDataRecord>;

            //assert
            Assert.AreEqual(result[0].Timestamp, new DateTime(2012, 1, 1, 0, 2, 14));
            Assert.AreEqual(result[0].AirTemperature, 34.3m);
            Assert.AreEqual(result[0].BarometricPressure, 30.5m);
            Assert.AreEqual(result[0].DewPoint, 26.9m);
            Assert.AreEqual(result[0].Humidity, 74.2m);
            Assert.AreEqual(result[0].WindDir, 346.4m);
            Assert.AreEqual(result[0].WindGust, 11m);
            Assert.AreEqual(result[0].WindSpeed, 3.6m);
        }
    }
}

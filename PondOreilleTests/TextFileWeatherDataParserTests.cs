using NUnit.Framework;
using PondOreille.Parsers;
using System.IO;

namespace Tests
{
    public class Tests
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
    }
}
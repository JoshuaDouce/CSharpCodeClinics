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
    }
}
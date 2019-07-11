using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherData;

namespace WeatherDataTests
{
    [TestClass]
    public class DateTimeParserTests
    {
        [TestMethod]
        public void ParseDate_WithValidDateString_ReturnsCorrectDate()
        {
            //act
            var result = DateTimeStringParser.ParseDateTimeString("2012_01_01 00:02:14	34.3	30.5	26.9	74.2	346.4	11	3.6");

            //assert
            Assert.AreEqual(result, new DateTime(2012, 1, 1, 0, 2, 14));
        }
    }
}

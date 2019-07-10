using NUnit.Framework;
using PondOreille.Parsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PondOreilleTests
{
    class DateTimeStringParserTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ParseDate_WithValidDateString_ReturnsCorrectDate()
        {
            //act
            var result = DateTimeStringParser.ParseDateTimeString("2012_01_01 00:02:14	34.3	30.5	26.9	74.2	346.4	11	3.6");

            //assert
            Assert.That(result, Is.EqualTo(new DateTime(2012, 1, 1, 0, 2, 14)));
        }

        [Test]
        public void ParseDate_WithOnlyDate_ReturnsOnlyDate() {
            //act
            var result = DateTimeStringParser.ParseDateString("2012_01_01");

            //assert
            Assert.That(result, Is.EqualTo(new DateTime(2012, 1, 1)));
        }
    }
}

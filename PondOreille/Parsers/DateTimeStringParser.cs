using System;
using System.Collections.Generic;
using System.Text;

namespace PondOreille.Parsers
{
    public static class DateTimeStringParser
    {
        public static DateTime ParseDateTimeString(string dateTimeString)
        {
            var year = int.Parse(dateTimeString.Substring(0, 4));
            var month = int.Parse(dateTimeString.Substring(5, 2));
            var day = int.Parse(dateTimeString.Substring(8, 2));
            var hour = int.Parse(dateTimeString.Substring(11, 2));
            var minute = int.Parse(dateTimeString.Substring(14, 2));
            var second = int.Parse(dateTimeString.Substring(17, 2));

            return new DateTime(year, month, day, hour, minute, second);
        }

        public static DateTime ParseDateString(string dateString) {
            var year = int.Parse(dateString.Substring(0, 4));
            var month = int.Parse(dateString.Substring(5, 2));
            var day = int.Parse(dateString.Substring(8, 2));

            return new DateTime(year, month, day);
        }
    }
}

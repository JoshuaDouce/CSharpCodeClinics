using System;
using System.Collections.Generic;

namespace WeatherData
{
    public static class Utils
    {
        public static double GetCoeffectient(IEnumerable<WeatherDataRecord> weatherDataRecords, DateTime start)
        {
            var arrX = new List<double>();
            var arrY = new List<double>();

            foreach (var record in weatherDataRecords)
            {
                arrX.Add((double)record.BarometricPressure);
                arrY.Add((record.Timestamp - start).TotalHours);
            }

            var (intersect, slope) = MathNet.Numerics.Fit.Line(arrX.ToArray(), arrY.ToArray());

            return slope;
        }
    }
}

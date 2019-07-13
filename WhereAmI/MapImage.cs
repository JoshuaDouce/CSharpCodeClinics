using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WhereAmI
{
    class MapImage
    {
        public static void Show(GeoCoordinate location)
        {
            string filename = $"{location.Latitude:##.000},{location.Longitude:##.000},{location.HorizontalAccuracy:####}m.jpg";

            DownloadMapImage(BuildURI(location), filename);

            OpenWithDefaultApp(filename);
        }

        private static void DownloadMapImage(Uri target, string filename)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(target, filename);
            }
        }

        private static Uri BuildURI(GeoCoordinate location)
        {
            var appId = "{Your app Id}";
            var appCode = "{your app Code}";

            var hereApiDns = "image.maps.cit.api.here.com";
            var hereApiUrl = $"https://{hereApiDns}/mia/1.6/mapview";
            var hereApiSecrets = $"&app_id={appId}&app_code={appCode}";

            var latlon = $"&lat={location.Latitude}&lon={location.Longitude}";

            return new Uri(hereApiUrl + $"?u={location.HorizontalAccuracy}{hereApiSecrets}{latlon}");
        }

        private static void OpenWithDefaultApp(string filename)
        {
            var si = new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                Arguments = $"/C start {filename}",
                WindowStyle = ProcessWindowStyle.Hidden
            };

            Process.Start(si);
        }
    }
}

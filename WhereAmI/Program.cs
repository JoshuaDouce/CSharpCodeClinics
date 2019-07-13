using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAmI
{
    class Program
    {
        private static GeoCoordinateWatcher Watcher;
        static void Main(string[] args)
        {
            Watcher = new GeoCoordinateWatcher();
            Watcher.StatusChanged += Watcher_StatusChanged;
            Watcher.PositionChanged += Watcher_PositionChanged;
            Watcher.MovementThreshold = 100;
            Watcher.Start();
            Console.ReadKey();

        }

        private static void Watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            Watcher.Stop();

            Console.WriteLine($"Position Changed: {e.Position.Location}");

            MapImage.Show(e.Position.Location);
        }

        private static void Watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            Console.WriteLine($"Status Changed: {e.Status}");
        }
    }
}

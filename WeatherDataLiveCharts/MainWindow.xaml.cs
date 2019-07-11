using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeatherData;

namespace WeatherDataLiveCharts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SeriesCollection MySeriesCollection { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            var series = new LineSeries() {
                Title = "Barometric Pressure"
            };

            var data = LoadData();
            var values = new ChartValues<WeatherDataRecord>();
            values.AddRange(data);

            series.Values = values;

            var wdrXY = Mappers.Xy<WeatherDataRecord>();
            wdrXY.X(wdr => wdr.Timestamp.Ticks);
            wdrXY.Y(wdr => (double)wdr.BarometricPressure);

            MySeriesCollection = new SeriesCollection(wdrXY) {
                series
            };

            DataContext = this;

            Coefficient.Content = Utils.GetCoeffectient(data, new DateTime(2013, 01, 01));
        }

        private static IEnumerable<WeatherDataRecord> LoadData() {

            var dataParser = new TextFileWeatherDataParser(@"C:\Users\SoftwareDevelopment\source\repos\CSharpCodeClinic\WeatherDataTests\TestFiles")
            {
                FromDate = new DateTime(2013, 01, 01),
                ToDate = new DateTime(2013, 01, 9)
            };

            return dataParser.GetWeatherData();
        }

        public Func<double, string> X_Label_Formatter => (d) => (new DateTime((long)d).ToShortDateString());
        public Func<double, string> Y_Label_Formatter => (d) => d.ToString("###.00");
    }
}

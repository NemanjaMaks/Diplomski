using Diplomski.DatabaseHelper;
using Diplomski.DataModel;
using LiveCharts;
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

namespace Diplomski.CustomComponents
{
    /// <summary>
    /// Interaction logic for StatisticsPage.xaml
    /// </summary>
    public partial class StatisticsPage : Page
    {
        public SeriesCollection ViekndPieChartSeries { get; set; }
        public SeriesCollection DeoDanaPieChartSeries { get; set; }
        public SeriesCollection PauzaPieChartSeries { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public StatisticsPage()
        {
            InitializeComponent();
            PreferenceStatistika statistika = SqlQueryHelper.GetPreferenceStatistika();

            int i = 0;
            foreach (var item in statistika)
            {
                Label title = new Label();
                title.Content = item.Key;
                title.FontSize = 36;
                title.HorizontalAlignment = HorizontalAlignment.Center;
                Grid.SetColumn(title, i);
                Grid.SetRow(title, 0);
                grid.Children.Add(title);

                PieChart pieChart = new PieChart();
                pieChart.Hoverable = true;
                pieChart.LegendLocation = LegendLocation.Bottom;
                SeriesCollection series = new SeriesCollection();
                foreach (var pair in item.Value)
                {
                    series.Add(new PieSeries { Title = pair.Name, Values = new ChartValues<int> { pair.Value } });
                }
                pieChart.Series = series;

                Grid.SetColumn(pieChart, i++);
                Grid.SetRow(pieChart, 1);
                grid.Children.Add(pieChart);
            }

            DataContext = this;
        }
    }
}

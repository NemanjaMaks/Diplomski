using Diplomski.DatabaseHelper;
using Diplomski.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for SvaDezurstvaPage.xaml
    /// </summary>
    public partial class SvaDezurstvaPage : Page
    {
        private ObservableCollection<Dezurstvo> dezurstva;

        public SvaDezurstvaPage()
        {
            InitializeComponent();

            dezurstva = new ObservableCollection<Dezurstvo>(SqlQueryHelper.GetDezurstva(MainWindow.User.id));
            dataGrid.ItemsSource = dezurstva;
        }

        private void btn_zameni_Click(object sender, RoutedEventArgs e)
        {
            Dezurstvo zamena = dataGrid.SelectedItem as Dezurstvo;
            if (zamena != null)
            {
                ZamenaDialog zamenaDialog = new ZamenaDialog(zamena);
                zamenaDialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("Izaberite dežurstvo sa kojim želite da se zamenite.");
            }
        }
    }
}

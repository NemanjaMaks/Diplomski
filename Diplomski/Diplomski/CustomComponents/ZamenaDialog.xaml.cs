using Diplomski.DatabaseHelper;
using Diplomski.DataModel;
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
using System.Windows.Shapes;

namespace Diplomski.CustomComponents
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ZamenaDialog : Window
    {
        private Dezurstvo zamena;
        public ZamenaDialog(Dezurstvo zamena)
        {
            InitializeComponent();
            this.zamena = zamena;
            LoadData();
        }

        private void LoadData()
        {
            dataGrid.ItemsSource = MainWindow.User.mojaDezurstva;
            dataGrid.Loaded += DataGrid_Loaded;
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var col in dataGrid.Columns)
            {
                if (!(col.Header.ToString() == "Predmet" || col.Header.ToString() == "Vreme"))
                {
                    col.Visibility = Visibility.Hidden;
                }
            }
        }

        private void btn_potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Dezurstvo dezurstvo = dataGrid.SelectedItem as Dezurstvo;
            if(dezurstvo != null)
            {
                SqlQueryHelper.RequestZamena(zamena.id, dezurstvo.id, zamena.id_korisnika, dezurstvo.id_korisnika);
            }
            else
            {
                MessageBox.Show("Izaberite dežurstvo koje želite da zamenite.");
            }
        }

        private void btn_odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

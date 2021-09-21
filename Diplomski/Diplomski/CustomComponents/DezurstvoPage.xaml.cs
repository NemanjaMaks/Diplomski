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
    /// Interaction logic for DezurstvoPage.xaml
    /// </summary>
    public partial class DezurstvoPage : Page
    {
        private ObservableCollection<Dezurstvo> dezurstva;

        public DezurstvoPage()
        {
            InitializeComponent();
            MainWindow.User.LoadDezurstva();
            dezurstva = new ObservableCollection<Dezurstvo>(MainWindow.User.mojaDezurstva);
            dataGrid.ItemsSource = dezurstva;
            
        }

        private void btn_zameni_Click(object sender, RoutedEventArgs e)
        {
            Dezurstvo dezurstvo = dataGrid.SelectedItem as Dezurstvo;
            if(dezurstvo != null)
            {
                ListaDezurstva listaDezurstva = SqlQueryHelper.GetDezurstva(MainWindow.User.Id);
                Dezurstvo zamena = listaDezurstva.GetBestFit(MainWindow.User.preference, MainWindow.User.lokalnePreference);
                if(zamena != null)
                {
                    MessageBoxResult result = MessageBox.Show(zamena.ToString(), "Da li zelis da zamenis dezurstvo sa sledecim dezurstvom?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        SqlQueryHelper.RequestZamena(zamena.id, dezurstvo.id, zamena.id_korisnika, dezurstvo.id_korisnika);
                    }
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Ne postoji zamena za dezurstvo u vasem slobodnom periodu!", "Ne postoji zamena!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
                
        }
    }
}

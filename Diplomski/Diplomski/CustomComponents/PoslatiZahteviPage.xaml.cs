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
    /// Interaction logic for PoslatiZahteviPage.xaml
    /// </summary>
    public partial class PoslatiZahteviPage : Page
    {
        public PoslatiZahteviPage()
        {
            InitializeComponent();
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            MainWindow.User.LoadPoslateZahteve();
            dataGrid.ItemsSource = new ObservableCollection<Zahtev>(MainWindow.User.poslatiZahtevi);
        }

        private void btn_obrisi_Click(object sender, RoutedEventArgs e)
        {
            Zahtev zahtev = dataGrid.SelectedItem as Zahtev;
            if(zahtev != null)
            {
                SqlQueryHelper.ChangeZahtevStatus("zatvoren", zahtev.id);
                UpdateGrid();
            }
        }
    }
}

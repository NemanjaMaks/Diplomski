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
    /// Interaction logic for PrimljeniZahteviPage.xaml
    /// </summary>
    public partial class PrimljeniZahteviPage : Page
    {
        public PrimljeniZahteviPage()
        {
            InitializeComponent();
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            MainWindow.User.LoadPrimljeneZahteve();
            dataGrid.ItemsSource = new ObservableCollection<Zahtev>(MainWindow.User.primljeniZahtevi);
        }

        private void btn_prihvati_Click(object sender, RoutedEventArgs e)
        {
            Zahtev zahtev = dataGrid.SelectedItem as Zahtev;
            if (zahtev != null)
            {
                SqlQueryHelper.ChangeZahtevStatus("prihvacen", zahtev.id);
                SqlQueryHelper.Zameni(zahtev.id_dezurstva_za, zahtev.id_dezurstva_od, zahtev.korisnik_za, zahtev.korisnik_od);
                UpdateGrid();
            }
        }

        private void btn_odbij_Click(object sender, RoutedEventArgs e)
        {
            Zahtev zahtev = dataGrid.SelectedItem as Zahtev;
            if (zahtev != null)
            {
                SqlQueryHelper.ChangeZahtevStatus("odbijen", zahtev.id);
                UpdateGrid();
            }
        }
    }
}

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
            dezurstva = new ObservableCollection<Dezurstvo>(SqlQueryHelper.GetDezurstva(MainWindow.User.Id));
            dataGrid.ItemsSource = dezurstva;
            dataGrid.Loaded += DataGrid_Loaded;
            
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                if (column.Header.ToString() == "Id" || column.Header.ToString() == "Id_korisnika")
                {
                    column.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void btn_preference_Click(object sender, RoutedEventArgs e)
        {
            Global.ChangePageEvent?.Invoke(this, new PreferencePage());
        }
    }
}

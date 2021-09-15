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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Diplomski.CustomComponents
{
    /// <summary>
    /// Interaction logic for PreferencePage.xaml
    /// </summary>
    public partial class PreferencePage : Page
    {

        private Preference preference;

        public PreferencePage()
        {
            InitializeComponent();
            preference = SqlQueryHelper.GetPreference(MainWindow.User.Id);
            cbx_deoDana.SelectedIndex = preference.Uvece ? 1 : 0;
            cbx_pauza.SelectedIndex = preference.BezPauze ? 0 : 1;
            cbx_vikend.SelectedIndex = preference.Vikend ? 0 : 1;
        }

        private void btn_izmeni_Click(object sender, RoutedEventArgs e)
        {
            preference.Uvece = cbx_deoDana.SelectedIndex == 1;
            preference.BezPauze = cbx_pauza.SelectedIndex == 0;
            preference.Vikend = cbx_vikend.SelectedIndex == 0;
            SqlQueryHelper.ChangePreference(preference);
        }
    }
}

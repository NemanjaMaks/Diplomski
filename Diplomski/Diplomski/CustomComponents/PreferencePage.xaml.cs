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
        private LokalnePreference lokalnePreference;

        public PreferencePage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            MainWindow.User.LoadPreference();
            preference = MainWindow.User.preference;
            MainWindow.User.LoadLokalnePreference();
            lokalnePreference = MainWindow.User.lokalnePreference;

            cbx_deoDana.SelectedIndex = preference.Uvece ? 1 : 0;
            cbx_pauza.SelectedIndex = preference.BezPauze ? 0 : 1;
            cbx_vikend.SelectedIndex = preference.Vikend ? 0 : 1;

            nedostupan_od.SelectedDate = lokalnePreference.DatumNedostupan_od;
            nedostupan_do.SelectedDate = lokalnePreference.DatumNedostupan_do;
            vise_od.SelectedDate = lokalnePreference.DatumVise_od;
            vise_do.SelectedDate = lokalnePreference.DatumVise_do;

            cb_nedostupan.IsChecked = lokalnePreference.DatumNedostupan_od != null && lokalnePreference.DatumNedostupan_do != null;
            cb_vise.IsChecked = lokalnePreference.DatumVise_od != null && lokalnePreference.DatumVise_do != null;
        }

        private void btn_izmeni_Click(object sender, RoutedEventArgs e)
        {
            preference.Uvece = cbx_deoDana.SelectedIndex == 1;
            preference.BezPauze = cbx_pauza.SelectedIndex == 0;
            preference.Vikend = cbx_vikend.SelectedIndex == 0;
            SqlQueryHelper.ChangePreference(preference);
        }

        private void btn_izmeni_lokal_Click(object sender, RoutedEventArgs e)
        {
            if(nedostupan_od.SelectedDate == null || nedostupan_do.SelectedDate == null)
            {
                lokalnePreference.DatumNedostupan_do = null;
                lokalnePreference.DatumNedostupan_od = null;
            }
            else
            {
                lokalnePreference.DatumNedostupan_do = nedostupan_do.SelectedDate.Value.Add(new TimeSpan(23, 59, 59));
                lokalnePreference.DatumNedostupan_od = nedostupan_od.SelectedDate;
            }

            if (vise_do.SelectedDate == null || vise_od.SelectedDate == null)
            {
                lokalnePreference.DatumVise_do = null;
                lokalnePreference.DatumVise_od = null;
            }
            else
            {
                lokalnePreference.DatumVise_do = vise_do.SelectedDate.Value.Add(new TimeSpan(23, 59, 59));
                lokalnePreference.DatumVise_od = vise_od.SelectedDate;
            }

            SqlQueryHelper.ChangeLokalnePreference(lokalnePreference);
        }

        private void cbx_nedostupan_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if(cb == cb_nedostupan)
            {
                if (cb.IsChecked == true)
                {
                    nedostupan_do.IsEnabled = true;
                    nedostupan_od.IsEnabled = true;
                }
                else
                {
                    nedostupan_do.IsEnabled = false;
                    nedostupan_do.SelectedDate = null;
                    nedostupan_od.IsEnabled = false;
                    nedostupan_od.SelectedDate = null;
                }
            }
            else if(cb == cb_vise)
            {
                if (cb.IsChecked == true)
                {
                    vise_od.IsEnabled = true;
                    vise_do.IsEnabled = true;
                }
                else
                {
                    vise_od.IsEnabled = false;
                    vise_od.SelectedDate = null;
                    vise_do.IsEnabled = false;
                    vise_do.SelectedDate = null;
                }
            }
        }
    }
}

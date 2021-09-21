using Diplomski.CustomComponents;
using Diplomski.DatabaseHelper;
using Diplomski.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Diplomski
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private static User user;
        public static User User { get => user; set => user = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool expandColumn;
        public bool ExpandColumn 
        {
            get { return expandColumn; }
            set { expandColumn = value; OnPropertyChanged("ExpandColumn"); } 
        }

        public MainWindow()
        {
            this.DataContext = this;
            InitializeComponent();
            RegisterEvents();
            Changepage(new LoginPage());
            ExpandColumn = false;
        }

        private void RegisterEvents()
        {
            Global.LoginEvent += OnLogin;
            this.frame.Navigated += Frame_Navigated;
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            frame.NavigationService.RemoveBackEntry();
        }

        private void OnLogin(object sender, LoginEventArgs args)
        {
            User = args.User;
            User.LoadDezurstva();
            User.LoadPreference();
            User.LoadLokalnePreference();
            User.LoadPoslateZahteve();
            User.LoadPrimljeneZahteve();

            if (user.IsAdmin)
            {
                admin_menu.Visibility = Visibility.Visible;
                basic_menu.Visibility = Visibility.Collapsed;
                Changepage(new AdminExcelImportPage());
            }
            else
            {
                admin_menu.Visibility = Visibility.Collapsed;
                basic_menu.Visibility = Visibility.Visible;
                Changepage(new DezurstvoPage());
            }
            
            ExpandColumn = true;
        }

        private void Changepage(Page page)
        {
            frame.Navigate(page);
        }

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            switch (menuItem.Name)
            {
                case "dezurstva":
                    Changepage(new DezurstvoPage());
                    break;
                case "dezurstva_admin":
                    Changepage(new AdminDezurstvoPage());
                    break;
                case "prference":
                    Changepage(new PreferencePage());
                    break;
                case "poslati_zahtevi":
                    Changepage(new PoslatiZahteviPage());
                    break;
                case "primljeni_zahtevi":
                    Changepage(new PrimljeniZahteviPage());
                    break;
                case "statistika":
                    Changepage(new StatisticsPage());
                    break;
                case "logout":
                case "logout_admin":
                    ExpandColumn = false;
                    Changepage(new LoginPage());
                    break;
            }
        }
    }
}

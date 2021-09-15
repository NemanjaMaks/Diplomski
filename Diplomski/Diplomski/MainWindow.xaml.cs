using Diplomski.CustomComponents;
using Diplomski.DatabaseHelper;
using Diplomski.DataModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Diplomski
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static User user;
        public static User User { get => user; set => user = value; }

        public MainWindow()
        {
            InitializeComponent();
            RegisterEvents();
            frame.NavigationService.Navigate(new LoginPage());
        }

        private void RegisterEvents()
        {
            Global.LoginEvent += OnLogin;
            Global.ChangePageEvent += OnPageChange;
        }

        private void OnLogin(object sender, LoginEventArgs args)
        {
            User = args.User;
            frame.NavigationService.Navigate(new DezurstvoPage());
        }

        private void OnPageChange(object sender, Page page)
        {
            frame.NavigationService.Navigate(page);
        }

    }
}

using Diplomski.DatabaseHelper;
using Diplomski.DataModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    /// Interaction logic for KorisniciPage.xaml
    /// </summary>
    public partial class KorisniciPage : Page
    {
        private DataTable dtExcel;

        public KorisniciPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dataGrid.ItemsSource = new ObservableCollection<Korisnik>(SqlQueryHelper.GetKorisnike());
        }

        private void btn_import_Click(object sender, RoutedEventArgs e)
        {
            string filePath = string.Empty;
            string fileExt = string.Empty;
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == true)
            {
                filePath = file.FileName; 
                fileExt = System.IO.Path.GetExtension(filePath); 
                if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0)
                {
                    try
                    {
                        dtExcel = new DataTable();
                        dtExcel = ExcelReader.ReadExcel(filePath);
                        SqlQueryHelper.DodajKorisnike(dtExcel);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Please choose .xls or .xlsx file only.", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}

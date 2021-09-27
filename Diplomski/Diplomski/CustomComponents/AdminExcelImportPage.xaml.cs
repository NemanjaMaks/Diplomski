using Diplomski.DatabaseHelper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AdminExcelImportPage.xaml
    /// </summary>
    public partial class AdminExcelImportPage : Page
    {
        private DataTable dtExcel;
        private DataTable dtExcelGlavna;
        public AdminExcelImportPage()
        {
            InitializeComponent();
        }

        private void btn_import_Click(object sender, RoutedEventArgs e)
        {
            string filePath = string.Empty;
            string fileExt = string.Empty;
            OpenFileDialog file = new OpenFileDialog(); //open dialog to choose file  
            if (file.ShowDialog() == true) //if there is a file choosen by the user  
            {
                filePath = file.FileName; //get the path of the file  
                fileExt = System.IO.Path.GetExtension(filePath); //get the file extension  
                if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0)
                {
                    try
                    {
                        dtExcel = new DataTable();
                        dtExcelGlavna = new DataTable();
                        this.Cursor = Cursors.Wait;
                        dtExcel = ExcelReader.ReadExcel(filePath, 1); //read excel file 
                        dtExcelGlavna = ExcelReader.ReadExcel(filePath, 2); //read excel file 
                        dataGrid.ItemsSource = dtExcel.DefaultView;
                        dataGridGlavna.ItemsSource = dtExcelGlavna.DefaultView;
                        this.Cursor = Cursors.Arrow;
                    }
                    catch (Exception ex)
                    {
                        this.Cursor = Cursors.Arrow;
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Please choose .xls or .xlsx file only.", "Warning", MessageBoxButton.OK, MessageBoxImage.Error); //custom messageBox to show error  
                }
            }
        }

        private void btn_comfirm_Click(object sender, RoutedEventArgs e)
        {
            if(dtExcel != null)
            {
                SqlQueryHelper.DodajDezurstva(dtExcel);
                SqlQueryHelper.DodajGlavnaDezurstva(dtExcelGlavna);
                dtExcel.Clear();
                dtExcelGlavna.Clear();
                dataGrid.ItemsSource = null;
                dataGridGlavna.ItemsSource = null;
                MessageBox.Show("Uspešno ste dodali dežurstva u bazu");
            }
            else
            {
                MessageBox.Show("Dežurstva nisu importovana! Potvrda nije moguća.");
            } 
        }

        private void rb_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if(rb == rb_obicna)
            {
                dataGrid.Visibility = Visibility.Visible;
                dataGridGlavna.Visibility = Visibility.Hidden;
            }
            else
            {
                dataGrid.Visibility = Visibility.Hidden;
                dataGridGlavna.Visibility = Visibility.Visible;
            }
        }
    }
}

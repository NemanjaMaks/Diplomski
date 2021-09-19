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
                        DataTable dtExcel = new DataTable();
                        //dtExcel = ExcelReader.ReadExcel(filePath, fileExt); //read excel file 
                        dtExcel = ExcelReader.ReadExcel(filePath); //read excel file 
                        dataGrid.ItemsSource = dtExcel.DefaultView;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Please choose .xls or .xlsx file only.", "Warning", MessageBoxButton.OK, MessageBoxImage.Error); //custom messageBox to show error  
                }
            }
        }
    }
}

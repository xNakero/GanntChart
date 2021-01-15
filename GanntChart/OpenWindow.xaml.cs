using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace GanntChart
{
    /// <summary>
    /// Logika interakcji dla klasy OpenWindow.xaml
    /// </summary>
    public partial class OpenWindow : Window
    {
        public OpenWindow()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.FileName = "chart.csv";
            dialog.DefaultExt = "csv files (*.csv)|*.csv";
            dialog.Filter = "json files (*.json)|*.json|csv files (*.csv)|*.csv";

            if (dialog.ShowDialog() == true)
            {
                PathName.Text = dialog.FileName;
            }
        }
    }
}

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private ChartData chartData;
        private ChartParser chartParser = new ChartParser();

        public OpenWindow(ChartData chartData)
        {
            InitializeComponent();
            this.chartData = chartData;
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

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            if(PathName.Text.EndsWith(".csv")){
                chartParser.fromCSV(PathName.Text, chartData);
                chartData.printAllData();
            } 
            else if (PathName.Text.EndsWith(".json"))
            {
                //TODO json open
            }
            else
            {
                Debug.WriteLine("not a correct file type.");
            }
            PathName.Text = "";
        }
    }
}

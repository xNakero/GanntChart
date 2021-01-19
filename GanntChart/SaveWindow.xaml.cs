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
    /// Logika interakcji dla klasy SaveWindow.xaml
    /// </summary>
    public partial class SaveWindow : Window
    {
        private ChartData chartData;
        private ChartParser chartParser = new ChartParser();

        public SaveWindow(ChartData chartData)
        {
            this.chartData = chartData;
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            //"json files (*.json)|*.json|csv files (*.csv)|*.csv|All files (*.*)|*.*";
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = "chart.csv";
            dialog.DefaultExt = "csv files (*.csv)|*.csv";
            dialog.Filter = "csv files (*.csv)|*.csv";

            if (dialog.ShowDialog() == true)
            {
                PathName.Text = dialog.FileName;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (PathName.Text.EndsWith(".csv"))
            {
                chartParser.ToCsv(PathName.Text, chartData);
                chartData.printAllData();
            }
            else
            {
                Debug.WriteLine("not a correct file type.");
            }
            PathName.Text = "";
        }
    }
}

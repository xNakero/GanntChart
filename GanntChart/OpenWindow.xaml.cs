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
        private MainWindow mainWindow;
        private Wpf.CartesianChart.GanttChart.GanttExample gantt;

        public OpenWindow(ChartData chartData, MainWindow mainWindow, Wpf.CartesianChart.GanttChart.GanttExample gantt)
        {
            InitializeComponent();
            this.chartData = chartData;
            this.mainWindow = mainWindow;
            this.gantt = gantt;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.FileName = "chart.csv";
            dialog.DefaultExt = "csv files (*.csv)|*.csv";
            dialog.Filter = "csv files (*.csv)|*.csv";
            if (dialog.ShowDialog() == true)
            {
                PathName.Text = dialog.FileName;
            }
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            if (PathName.Text.EndsWith(".csv"))
            {
                chartParser.FromCsv(PathName.Text, chartData);
                chartData.printAllData();
                gantt.SetValues(chartData, "all");
                mainWindow.FrameWithinGrid.Content = gantt;
                mainWindow.FrameWithinGrid.Visibility = Visibility.Visible;
                this.Close();
            }
            else
            {
                Debug.WriteLine("not a correct file type.");
            }
            PathName.Text = "";
        }
    }
}

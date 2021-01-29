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
using System.Windows.Shapes;

namespace GanntChart
{
    /// <summary>
    /// Logika interakcji dla klasy AskSaveDialog.xaml
    /// </summary>
    public partial class AskSaveWindow : Window
    {
        private SaveWindow saveWindow;
        private ChartData chartData;
        private MainWindow mainWindow;
        private Wpf.CartesianChart.GanttChart.GanttExample gantt;
        public AskSaveWindow(ChartData chartData, MainWindow mainWindow, Wpf.CartesianChart.GanttChart.GanttExample gantt)
        {
            this.mainWindow = mainWindow;
            this.gantt = gantt;
            saveWindow = new SaveWindow(chartData);
            this.chartData = chartData;
            InitializeComponent();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            saveWindow.ShowDialog();
            this.Close();
            chartData.RemoveAllActivity();
            gantt.SetValues(chartData, "all");
            mainWindow.FrameWithinGrid.Visibility = Visibility.Hidden;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            chartData.RemoveAllActivity();
            gantt.SetValues(chartData, "all");
            mainWindow.FrameWithinGrid.Visibility = Visibility.Hidden;
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GanntChart
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ChartData chartData;
        private Wpf.CartesianChart.GanttChart.GanttExample gantt;
        public MainWindow()
        {
            InitializeComponent();
            this.chartData = new ChartData();
            this.gantt = new Wpf.CartesianChart.GanttChart.GanttExample();
            FrameWithinGrid.Navigate(new System.Uri("UserControl.xaml", UriKind.RelativeOrAbsolute));
            FrameWithinGrid.Visibility = Visibility.Hidden;
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            AskSaveWindow window = new AskSaveWindow(chartData);
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditWindow window = new EditWindow(chartData, this, gantt);
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow window = new OpenWindow(chartData, this, gantt);
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveWindow window = new SaveWindow(chartData);
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void ToPngButton_Click(object sender, RoutedEventArgs e)
        {
            SavePngWindow window = new SavePngWindow(gantt);
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }
    }
}

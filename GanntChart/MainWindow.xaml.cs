using System.Diagnostics;
using System.Windows;

namespace GanntChart
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ChartData chartData;

        public MainWindow()
        {
            InitializeComponent();
            this.chartData = new ChartData();
           
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            AskSaveWindow window = new AskSaveWindow(chartData);
            window.Show();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditWindow window = new EditWindow(chartData);
            window.ShowDialog();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow window = new OpenWindow(chartData);
            window.ShowDialog();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveWindow window = new SaveWindow(chartData);
            window.ShowDialog();
        }

        private void ToPngButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

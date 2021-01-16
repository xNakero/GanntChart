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

        public AskSaveWindow(ChartData chartData)
        {
            saveWindow = new SaveWindow(chartData);
            this.chartData = chartData;
            InitializeComponent();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            saveWindow.ShowDialog();
            this.Close();
            chartData.RemoveAllActivity();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            chartData.RemoveAllActivity();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

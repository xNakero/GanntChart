using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Logika interakcji dla klasy SavePngWindow.xaml
    /// </summary>
    public partial class SavePngWindow : Window
    {
        private ChartParser chartParser = new ChartParser();
        private Wpf.CartesianChart.GanttChart.GanttExample gantt;

        public SavePngWindow(Wpf.CartesianChart.GanttChart.GanttExample gantt)
        {
            InitializeComponent();
            this.gantt = gantt;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = "image.png";
            dialog.DefaultExt = "png files (*.png)|*.png";
            dialog.Filter = "png files (*.png)|*.png";

            if (dialog.ShowDialog() == true)
            {
                PathName.Text = dialog.FileName;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (PathName.Text.EndsWith(".png"))
            {
                chartParser.ToPng(PathName.Text, gantt);
            }
            else
            {
                Debug.WriteLine("not a correct file type.");
            }
            PathName.Text = "";
        }

    }
}

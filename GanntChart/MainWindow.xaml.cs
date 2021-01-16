using System.Diagnostics;
using System.Windows;

namespace GanntChart
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("text");
            Browser.Navigate(new System.Uri(@"D:\Studia\SEMESTR 5\dotNet\projekt - gannt chart\GanntChart\GanntChart\image.png"));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditWindow window = new EditWindow();
            window.Show();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow window = new OpenWindow();
            window.Show();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveWindow window = new SaveWindow();
            window.Show();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ToPngButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

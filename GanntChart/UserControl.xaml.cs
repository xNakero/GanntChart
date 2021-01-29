using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Microsoft.Scripting.Utils;

namespace Wpf.CartesianChart.GanttChart
{
    /// <summary>
    /// Interaction logic for GanttExample.xaml
    /// </summary>
    public partial class GanttExample : UserControl, INotifyPropertyChanged
    {
        private double _from;
        private double _to; 
        private ChartValues<GanttPoint> _values;
        private ChartValues<GanttPoint> x;

        public GanttExample()
        {
            InitializeComponent();
            Formatter = value => new DateTime((long)value).ToString("dd/MM HH:mm");
            _values = new ChartValues<GanttPoint>();
        }

        public void SetValues(ChartData chartData, string state)
        {
            this.InitializeComponent();
            _values = new ChartValues<GanttPoint>();
            _values.Clear();
            var labels = new List<string>();
            foreach (Activity activity in chartData.GetActivities())
            {   
                if(activity.State == state || state == "all")
                { 
                    _values.Add(new GanttPoint(activity.StartDate.Ticks, activity.EndDate.Ticks));
                    labels.Add(activity.Name);
                }
            }

            Series = new SeriesCollection
            {
                new RowSeries
                {
                    Values = _values,
                    DataLabels = true,
                    
                }
            };
            

            ResetZoomOnClick(null, null);
            Formatter = value => new DateTime((long)value).ToString("dd/MM HH:mm");
            Labels = labels.ToArray();
            DataContext = new SeriesViewModel();
            DataContext = this;
        }

        public SeriesCollection Series { get; set; }
        public Func<double, string> Formatter { get; set; }
        public string[] Labels { get; set; }

        public double From
        {
            get { return _from; }
            set
            {
                _from = value;
                OnPropertyChanged("From");
            }
        }

        public double To
        {
            get { return _to; }
            set
            {
                _to = value;
                OnPropertyChanged("To");
            }
        }

        public ChartValues<GanttPoint> Values { get; private set; }

        private void ResetZoomOnClick(object sender, RoutedEventArgs e)
        {
            if (_values.Count() > 0)
            {
                From = _values.First().StartPoint;
                To = _values.Last().EndPoint;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}

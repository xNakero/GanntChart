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
using LiveCharts.Defaults;
using LiveCharts.Wpf;

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
        


        public GanttExample()
        {
            InitializeComponent();
            Formatter = value => new DateTime((long)value).ToString("dd/MM HH:mm");
            _values = new ChartValues<GanttPoint>();
            
            /*           var now = DateTime.Now;

                       _values = new ChartValues<GanttPoint>
                       {
                           new GanttPoint(now.Ticks, now.AddDays(2).Ticks),
                           new GanttPoint(now.AddDays(1).Ticks, now.AddDays(3).Ticks),
                           new GanttPoint(now.AddDays(3).Ticks, now.AddDays(5).Ticks),
                           new GanttPoint(now.AddDays(5).Ticks, now.AddDays(8).Ticks),
                           new GanttPoint(now.AddDays(6).Ticks, now.AddDays(10).Ticks),
                           new GanttPoint(now.AddDays(7).Ticks, now.AddDays(14).Ticks),
                           new GanttPoint(now.AddDays(9).Ticks, now.AddDays(12).Ticks),
                           new GanttPoint(now.AddDays(9).Ticks, now.AddDays(14).Ticks),
                           new GanttPoint(now.AddDays(10).Ticks, now.AddDays(11).Ticks),
                           new GanttPoint(now.AddDays(12).Ticks, now.AddDays(16).Ticks),
                           new GanttPoint(now.AddDays(15).Ticks, now.AddDays(17).Ticks),
                           new GanttPoint(now.AddDays(18).Ticks, now.AddDays(19).Ticks)
                       };

                       Series = new SeriesCollection
                       {
                           new RowSeries
                           {
                               Values = _values,
                               DataLabels = true
                           }
                       };
                       Formatter = value => new DateTime((long)value).ToString("dd MMM");

                       var labels = new List<string>();
                       for (var i = 0; i < 12; i++)
                           labels.Add("Task " + i);
                       Labels = labels.ToArray();

                       ResetZoomOnClick(null, null);

                       DataContext = this;*/
        }

        public void SetValues(ChartData chartData)
        {
            this.InitializeComponent();
            _values = new ChartValues<GanttPoint>();
            //_values.Clear();
            var labels = new List<string>();
            foreach (Activity activity in chartData.GetActivities())
            {
                _values.Add(new GanttPoint(activity.StartDate.Ticks, activity.EndDate.Ticks));
                labels.Add(activity.Name);
            }
            Series = new SeriesCollection
            {
                new RowSeries
                {
                    Values = _values,
                    DataLabels = true
                }
            };
            Formatter = value => new DateTime((long)value).ToString("dd/MM HH:mm");
            Labels = labels.ToArray();
            DataContext = new SeriesViewModel();
            DataContext = this;
            ResetZoomOnClick(null, null);
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
            }
        }

        public double To
        {
            get { return _to; }
            set
            {
                _to = value;
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

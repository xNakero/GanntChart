using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LINQtoCSV;


    public class Activity
    {
        [CsvColumn(FieldIndex = 1)]
        public string Name { get; set; }

        [CsvColumn(FieldIndex = 2)]
        public DateTime StartDate { get; set; }

        [CsvColumn(FieldIndex = 3)]
        public DateTime EndDate { get; set; }

        public Activity(string name, DateTime startDate, DateTime endDate)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
        }

        public Activity() { }

        public override string ToString()
        {
            string startDate = StartDate.ToString("yyyy-MM-dd HH':'mm':'ss", DateTimeFormatInfo.InvariantInfo);
            string endDate = EndDate.ToString("yyyy-MM-dd HH':'mm':'ss", DateTimeFormatInfo.InvariantInfo);
            return this.Name + ", " + startDate + ", " + endDate + ", ";
        }

    }

    public class ChartData
    {
        public ObservableCollection<Activity> activities { get; set; }

        public ChartData()
        {
            activities = new ObservableCollection<Activity>();
        }

        public ObservableCollection<Activity> GetActivities()
        {
            return activities;
        }
        public void AddActivity(Activity activity)
        {
            activities.Add(activity);
        }

        public void addActivitiesList(ObservableCollection<Activity> list)
        {
            activities = list;
        }
        public void RemoveActivity(Activity activity)
        {
            activities.RemoveAt(activities.IndexOf(activity));
        }

        public void RemoveAllActivity()
        {
            activities.Clear();
        }

        public void printAllData()
        {
            foreach (Activity a in activities)
            {
                Debug.WriteLine(a.Name + " " + a.StartDate + " " + a.EndDate);
            }
        }

    }

    public class ChartParser
    {
        public ChartParser() { }

        public void ToCsv(string path, ChartData chartData)
        {
            CsvFileDescription outputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ',',
                FirstLineHasColumnNames = true
            };
            CsvContext cc = new CsvContext();
            cc.Write(chartData.GetActivities(), path, outputFileDescription);
        }

        public void FromCsv(string path, ChartData chartData)
        {
            if (File.Exists(path))
            {
                chartData.GetActivities().Clear();
                var list = File.ReadAllLines(path)
                    .Skip(1)
                    .Where(row => row.Length > 0)
                    .Select(ParseRow).ToList();
                ObservableCollection<Activity> oc = new ObservableCollection<Activity>(list);
                chartData.addActivitiesList(oc);
            }
            else
            {
                Debug.WriteLine("No such path");
            }
        }

        public void ToPng(string path, Wpf.CartesianChart.GanttChart.GanttExample gantt)
        {
            var encoder = new PngBitmapEncoder();
            EncodeVisual(gantt, path, encoder);
        }

        private static void EncodeVisual(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            var bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            var frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);
            using (var stream = File.Create(fileName)) encoder.Save(stream);
        }

        private Activity ParseRow(string row)
        {
            var columns = row.Split(',');
            return new Activity(
                columns[0],
                DateTime.Parse(columns[1]),
                DateTime.Parse(columns[2]));
        }

    }


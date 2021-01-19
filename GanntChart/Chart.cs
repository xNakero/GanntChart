using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using LINQtoCSV;

    public class Activity
    {
        [CsvColumn(FieldIndex = 1)]
        public string Name { get; set; }

        [CsvColumn(FieldIndex = 2)]
        public DateTime StartDate { get; set; }

        [CsvColumn(FieldIndex = 3)]
        public DateTime EndDate { get; set; }

        [CsvColumn(FieldIndex = 4)]
        public string State { get; set; }

        public Activity(string name, DateTime startDate, DateTime endDate, string state) 
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            State = state;
        }

        public Activity() { }

        public override string ToString()
        {
            string startDate = StartDate.ToString("yyyy-MM-dd HH':'mm':'ss", DateTimeFormatInfo.InvariantInfo);
            string endDate = EndDate.ToString("yyyy-MM-dd HH':'mm':'ss", DateTimeFormatInfo.InvariantInfo);
            return this.Name + ", " + startDate + ", " + endDate + ", " + this.State;
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
            foreach(Activity a in activities)
            {
                Debug.WriteLine(a.Name + " " + a.StartDate + " " + a.EndDate + " " + a.State);
            }
        }
        
    }

    public class ChartParser
    {
        public ChartParser() { }

        public void toCsv(string path, ChartData chartData)
        {
            //TO DO 
            CsvFileDescription outputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ',',
                FirstLineHasColumnNames = true
            };
            CsvContext cc = new CsvContext();
            cc.Write(chartData.GetActivities(), path, outputFileDescription);
        }

        public void fromCSV(string path, ChartData chartData)
        {
            if(File.Exists(path))
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

        private Activity ParseRow(string row)
        {
            var columns = row.Split(',');
            return new Activity(
                columns[0],
                DateTime.Parse(columns[1]),
                DateTime.Parse(columns[2]),
                columns[3]);
        }
       
    }


using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LINQtoCSV;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

namespace GanntChart
{
    class Activity
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
       
    }

    class ChartData
    {
        private List<Activity> Activities { get; set; }

        public ChartData()
        {
            Activities = new List<Activity>();
        }

        public List<Activity> GetActivities()
        {
            return Activities;
        }
        public void AddActivity(Activity activity)
        {
            Activities.Add(activity);
        }

        public void addActivitiesList(List<Activity> list)
        {
            Activities = list;
        }
        public void RemoveActivity(Activity activity)
        {
            Activities.RemoveAt(Activities.IndexOf(activity));
        }

        public void UpdateActivity(Activity activity, Activity updatedActivity)
        {
            activity.Name = updatedActivity.Name;
            activity.StartDate = updatedActivity.StartDate;
            activity.EndDate = updatedActivity.EndDate;
            activity.State = updatedActivity.State;
        }

    }

    class ChartParser
    {
        private ChartData chartData;

        public ChartParser(ChartData chartData) {
            this.chartData = chartData;
        }

        public void toCsv(string path)
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

        public void fromCSV(string path)
        {
            var list = File.ReadAllLines(path)
                .Skip(1)
                .Where(row => row.Length > 0)
                .Select(ParseRow).ToList();
            chartData.addActivitiesList(list);
        }
        private Activity ParseRow(string row)
        {
            var columns = row.Split(',');
            return new Activity(
                columns[0],
                DateTime.ParseExact(columns[1], "yyyy-MM-dd HH':'mm':'ss", CultureInfo.InvariantCulture),
                DateTime.ParseExact(columns[1], "yyyy-MM-dd HH':'mm':'ss", CultureInfo.InvariantCulture),
                columns[3]);
        }

       
    }
}

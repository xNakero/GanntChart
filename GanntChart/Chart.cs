using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

namespace GanntChart
{
    class Activity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
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
        private ArrayList Activities { get; set; }

        public ChartData()
        {
            Activities = new ArrayList();
        }

        public void AddActivity(Activity activity)
        {
            Activities.Add(activity);
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
        public string PathCSV { get; set; }
        public string PathJson { get; set; }
        public string PathExcel { get; set; }
        public string imagePath { get; set; }

        

    }
}

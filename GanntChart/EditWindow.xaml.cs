using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
    /// Logika interakcji dla klasy EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private ChartData chartData;
        private MainWindow mainWindow;
        private Wpf.CartesianChart.GanttChart.GanttExample gantt;
        public EditWindow(ChartData chartData, MainWindow mainWindow, Wpf.CartesianChart.GanttChart.GanttExample gantt)
        {
            this.chartData = chartData;
            this.mainWindow = mainWindow;
            this.gantt = gantt;
            InitializeComponent();
            FillComboBoxHours();
            setButtonsAtStart();
            Activities.ItemsSource = chartData.GetActivities();
        }

        private void FillComboBoxHours()
        {
            for (int i = 0; i <= 23; i++)
            {
                HourStart.Items.Add(i);
                HourEnd.Items.Add(i);
            }
            for(int i = 0; i < 60; i += 5)
            {
                MinuteStart.Items.Add(i);
                MinuteEnd.Items.Add(i);
            }
            List<string> states = new List<string>(new string[] { "Not started", "Started", "Completed" });
            foreach(string state in states)
            {
                States.Items.Add(state);
            }
        }

        private void setButtonsAtStart()
        {
            AddButton.IsEnabled = false;
            RemoveButton.IsEnabled = false;
            EditButton.IsEnabled = false;
        }

        private void canStartBeUsed()
        {
            string split = Name.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            if (StartCalendar.SelectedDate > EndCalendar.SelectedDate || split == "" || split == null || StartCalendar.SelectedDate ==  null 
                || EndCalendar.SelectedDate == null)
            {
                AddButton.IsEnabled = false;
            } 
            else if(StartCalendar.SelectedDate == EndCalendar.SelectedDate)
            {
                if (Convert.ToInt32(HourStart.SelectedItem) > Convert.ToInt32(HourEnd.SelectedItem))
                {
                    AddButton.IsEnabled = false;
                    
                }
                else if (Convert.ToInt32(HourStart.SelectedItem) == Convert.ToInt32(HourEnd.SelectedItem))
                {
                    if (Convert.ToInt32(MinuteStart.SelectedItem) >= Convert.ToInt32(MinuteEnd.SelectedItem))
                    {
                        AddButton.IsEnabled = false;
                    }
                    else
                    {
                        AddButton.IsEnabled = true;
                    }
                }
                else
                {
                    AddButton.IsEnabled = true;
                }
            }
            else
            {
                AddButton.IsEnabled = true;
            }
        }

        private void CanEditBeUsed()
        {
            Activity activity = (Activity)Activities.SelectedItem;
            string split = Name.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            if (Activities.SelectedItems.Count > 0)
            {
                if (StartCalendar.SelectedDate != null && HourStart.SelectedItem != null && MinuteStart.SelectedItem != null 
                    && EndCalendar.SelectedDate != null && HourEnd.SelectedItem != null && MinuteEnd.SelectedItem != null)
                {
                    if (StartCalendar.SelectedDate < EndCalendar.SelectedDate)
                    {
                        EditButton.IsEnabled = true;
                    }
                    else if (StartCalendar.SelectedDate == EndCalendar.SelectedDate)
                    {
                        if (Convert.ToInt32(HourStart.SelectedItem) > Convert.ToInt32(HourEnd.SelectedItem))
                        {
                            EditButton.IsEnabled = false;
                        }
                        else if (Convert.ToInt32(HourStart.SelectedItem) == Convert.ToInt32(HourEnd.SelectedItem))
                        {
                            if (Convert.ToInt32(MinuteStart.SelectedItem) >= Convert.ToInt32(MinuteEnd.SelectedItem))
                            {
                                EditButton.IsEnabled = false;
                            }
                            else
                            {
                                EditButton.IsEnabled = true;
                            }
                        }
                        else
                        {
                            EditButton.IsEnabled = true;
                        }
                       
                    }
                    else
                    {
                        EditButton.IsEnabled = true;
                    }
                }
                else if ((StartCalendar.SelectedDate != null && HourStart.SelectedItem != null && MinuteStart.SelectedItem != null)
                    && (EndCalendar.SelectedDate == null || HourEnd.SelectedItem == null || MinuteEnd.SelectedItem == null))
                {
                    int startYear = Convert.ToInt32(StartCalendar.SelectedDate.Value.Year.ToString());
                    int startMonth = Convert.ToInt32(StartCalendar.SelectedDate.Value.Month.ToString());
                    int startDay = Convert.ToInt32(StartCalendar.SelectedDate.Value.Day.ToString());
                    int startHour = Convert.ToInt32(HourStart.SelectedItem);
                    int startMinute = Convert.ToInt32(MinuteStart.SelectedItem);
                    DateTime startDate = new DateTime(startYear, startMonth, startDay, startHour, startMinute, 0);
                    if( startDate < activity.EndDate)
                    {
                        EditButton.IsEnabled = true;
                    }
                }
                else if ((StartCalendar.SelectedDate == null || HourStart.SelectedItem == null || MinuteStart.SelectedItem == null)
                    && (EndCalendar.SelectedDate != null && HourEnd.SelectedItem != null && MinuteEnd.SelectedItem != null))
                {
                    int endYear = Convert.ToInt32(EndCalendar.SelectedDate.Value.Year.ToString());
                    int endMonth = Convert.ToInt32(EndCalendar.SelectedDate.Value.Month.ToString());
                    int endDay = Convert.ToInt32(EndCalendar.SelectedDate.Value.Day.ToString());
                    int endHour = Convert.ToInt32(HourEnd.SelectedItem);
                    int endMinute = Convert.ToInt32(MinuteEnd.SelectedItem);
                    DateTime endDate = new DateTime(endYear, endMonth, endDay, endHour, endMinute, 0);
                    if (endDate > activity.StartDate)
                    {
                        EditButton.IsEnabled = true;
                    }
                }
                else if ((split != null && split != "") || States.SelectedItem != null)
                {
                    EditButton.IsEnabled = true;
                }
            }
        }

        private void HourStart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            canStartBeUsed();
            CanEditBeUsed();
        }

        private void MinuteStart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            canStartBeUsed();
            CanEditBeUsed();
        }

        private void HourEnd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            canStartBeUsed();
            CanEditBeUsed();
        }

        private void MinuteEnd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            canStartBeUsed();
            CanEditBeUsed();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            int startYear = Convert.ToInt32(StartCalendar.SelectedDate.Value.Year.ToString());
            int startMonth = Convert.ToInt32(StartCalendar.SelectedDate.Value.Month.ToString());
            int startDay = Convert.ToInt32(StartCalendar.SelectedDate.Value.Day.ToString());
            int startHour = Convert.ToInt32(HourStart.SelectedItem);
            int startMinute = Convert.ToInt32(MinuteStart.SelectedItem);
            int endYear = Convert.ToInt32(EndCalendar.SelectedDate.Value.Year.ToString());
            int endMonth = Convert.ToInt32(EndCalendar.SelectedDate.Value.Month.ToString());
            int endDay = Convert.ToInt32(EndCalendar.SelectedDate.Value.Day.ToString());
            int endHour = Convert.ToInt32(HourEnd.SelectedItem);
            int endMinute = Convert.ToInt32(MinuteEnd.SelectedItem);
            DateTime startDate = new DateTime(startYear, startMonth, startDay, startHour, startMinute, 0);
            DateTime endDate = new DateTime(endYear, endMonth, endDay, endHour, endMinute, 0);
            Activity activity = new Activity();
            if (States.SelectedItem == null)
            {
                activity = new ActivityBuilder()
                    .SetName(Name.Text.Trim())
                    .SetStartDate(startDate)
                    .SetEndDate(endDate)
                    .Build();
            } 
            else
            {
                activity = new ActivityBuilder()
                   .SetName(Name.Text.Trim())
                   .SetStartDate(startDate)
                   .SetEndDate(endDate)
                   .SetState(States.SelectedItem.ToString())
                   .Build();
            }
            activity.Name = Name.Text.Trim();
            activity.StartDate = startDate;
            activity.EndDate = endDate;
            Debug.WriteLine(activity.ToString());
            Debug.WriteLine(activity.Name + activity.StartDate + activity.EndDate);
            chartData.AddActivity(activity);
            StartCalendar.SelectedDates.Clear();
            EndCalendar.SelectedDates.Clear();
            HourStart.SelectedItem = null;
            HourEnd.SelectedItem = null;
            MinuteStart.SelectedItem = null;
            MinuteEnd.SelectedItem = null;
            Name.Text = null;
            Activities.Items.Refresh();
            gantt.SetValues(chartData, "all");
            mainWindow.FrameWithinGrid.Content = gantt;
            mainWindow.FrameWithinGrid.Visibility = Visibility.Visible;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Activity a = (Activity)Activities.SelectedItem;
            Debug.WriteLine(a.ToString());
            chartData.RemoveActivity(a);
            Activities.Items.Refresh();
            gantt.SetValues(chartData, "all");
            mainWindow.FrameWithinGrid.Visibility = Visibility.Visible;
            RemoveButton.IsEnabled = false;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            canStartBeUsed();
            CanEditBeUsed();
        }

        private void EndCalendar_OnSelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            canStartBeUsed();
            CanEditBeUsed();
        }

        private void StartCalendar_OnSelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            canStartBeUsed();
            CanEditBeUsed();
        }

        private void Activities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Activities.SelectedItems.Count > 0)
            {
                RemoveButton.IsEnabled = true;    
            }
            CanEditBeUsed();
        }

        private void States_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CanEditBeUsed();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            ActivityBuilder activityBuilder = new ActivityBuilder((Activity)Activities.SelectedItem);
            if (StartCalendar.SelectedDate != null && HourStart.SelectedItem != null && MinuteStart.SelectedItem != null)
            {
                int startYear = Convert.ToInt32(StartCalendar.SelectedDate.Value.Year.ToString());
                int startMonth = Convert.ToInt32(StartCalendar.SelectedDate.Value.Month.ToString());
                int startDay = Convert.ToInt32(StartCalendar.SelectedDate.Value.Day.ToString());
                int startHour = Convert.ToInt32(HourStart.SelectedItem);
                int startMinute = Convert.ToInt32(MinuteStart.SelectedItem);
                DateTime startDate = new DateTime(startYear, startMonth, startDay, startHour, startMinute, 0);
                activityBuilder.SetStartDate(startDate);
            }
            if (EndCalendar.SelectedDate != null && HourEnd.SelectedItem != null && MinuteEnd.SelectedItem != null)
            {
                int endYear = Convert.ToInt32(EndCalendar.SelectedDate.Value.Year.ToString());
                int endMonth = Convert.ToInt32(EndCalendar.SelectedDate.Value.Month.ToString());
                int endDay = Convert.ToInt32(EndCalendar.SelectedDate.Value.Day.ToString());
                int endHour = Convert.ToInt32(HourEnd.SelectedItem);
                int endMinute = Convert.ToInt32(MinuteEnd.SelectedItem);
                DateTime endDate = new DateTime(endYear, endMonth, endDay, endHour, endMinute, 0);
                activityBuilder.SetStartDate(endDate);
            }
            string split = Name.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            if (split != null && split != "")
            {
                activityBuilder.SetName(Name.Text.Trim());
            }
            if (!string.IsNullOrEmpty(States.Text))
            {
                activityBuilder.SetState(States.SelectedItem.ToString());
            }
            Activity activity = activityBuilder.Build();
            chartData.ReplaceActivity((Activity)Activities.SelectedItem, activity);
            StartCalendar.SelectedDates.Clear();
            EndCalendar.SelectedDates.Clear();
            HourStart.SelectedItem = null;
            HourEnd.SelectedItem = null;
            MinuteStart.SelectedItem = null;
            MinuteEnd.SelectedItem = null;
            Activities.SelectedItem = null;
            States.SelectedItem = null;
            Name.Text = "";
            Activities.Items.Refresh(); 
            gantt.SetValues(chartData, "Started");
            mainWindow.FrameWithinGrid.Content = gantt;
            mainWindow.FrameWithinGrid.Visibility = Visibility.Visible;
            EditButton.IsEnabled = false;
        }
    }
}

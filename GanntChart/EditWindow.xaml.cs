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
    /// Logika interakcji dla klasy EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public EditWindow()
        {
            InitializeComponent();
            fillComboBox();
            setButtonsAtStart();
        }

        private void fillComboBox()
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
            State.Items.Add("Complete");
            State.Items.Add("Incomplete");
            State.Items.Add("Started");
        }

        private void setButtonsAtStart()
        {
            AddButton.IsEnabled = false;
            RemoveButton.IsEnabled = false;
        }

        private void canStartBeUsed()
        {
            if (StartCalendar.SelectedDate > EndCalendar.SelectedDate || Name.Text == "" || Name.Text == null || State.SelectedItem == null)
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

        private void HourStart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            canStartBeUsed();
        }

        private void MinuteStart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            canStartBeUsed();
        }

        private void HourEnd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            canStartBeUsed();

        }

        private void MinuteEnd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            canStartBeUsed();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            int startYear = Convert.ToInt32(StartCalendar.SelectedDate.Value.Year.ToString());
            int startMonth = Convert.ToInt32(StartCalendar.SelectedDate.Value.Month.ToString());
            int startDay = Convert.ToInt32(StartCalendar.SelectedDate.Value.Day.ToString());
            int startHour = Convert.ToInt32(HourStart.SelectedItem);
            int startMinute = Convert.ToInt32(MinuteStart.SelectedItem);
            int endYear = Convert.ToInt32(StartCalendar.SelectedDate.Value.Year.ToString());
            int endMonth = Convert.ToInt32(StartCalendar.SelectedDate.Value.Month.ToString());
            int endDay = Convert.ToInt32(StartCalendar.SelectedDate.Value.Day.ToString());
            int endHour = Convert.ToInt32(HourEnd.SelectedItem);
            int endMinute = Convert.ToInt32(MinuteEnd.SelectedItem);
            DateTime startDate = new DateTime(startYear, startMonth, startDay, startHour, startMinute, 0);
            DateTime endDate = new DateTime(endYear, endMonth, endDay, endHour, endMinute, 0);
            Activity activity = new Activity();
            activity.Name = Name.Text;
            activity.StartDate = startDate;
            activity.EndDate = endDate;
            activity.State = State.SelectedItem.ToString();
            Debug.WriteLine(activity.ToString());
            Debug.WriteLine(activity.Name + activity.State + activity.StartDate + activity.EndDate);

            StartCalendar.SelectedDates.Clear();
            EndCalendar.SelectedDates.Clear();
            HourStart.SelectedItem = null;
            HourEnd.SelectedItem = null;
            MinuteStart.SelectedItem = null;
            MinuteEnd.SelectedItem = null;
            Name.Text = null;
            State.SelectedItem = null;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

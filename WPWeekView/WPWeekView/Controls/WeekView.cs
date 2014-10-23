using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPWeekView.Controls
{
    [TemplatePart(Name = PART_WEEK_CANVAS_NAME, Type = typeof(Canvas))]
    [TemplatePart(Name = PART_DAY_NAME, Type = typeof(Grid))]
    public class WeekView : Control
    {
        /// <summary>
        /// <see cref="StartingDay">See StartingDay Property</see>
        /// </summary>
        private DayOfWeek startingDay;
        private const string PART_WEEK_CANVAS_NAME = "PART_WEEK_CANVAS";
        private const string PART_DAY_NAME = "PART_DAY_NAME";
        /// <summary>
        /// Gets or sets the starting day of the week to be dispalyed
        /// </summary>
        public DayOfWeek StartingDay
        {
            get
            {
                return startingDay;
            }
            set
            {
                startingDay = value;
                UpdateDaysOfWeek();
            }
        }

        /// <summary>
        /// The days of week to be used, starting from the <see cref="StartingDay">StartingDay</see>
        /// </summary>
        private DayOfWeek[] DaysOfWeek;

        public WeekView() : base()
        {
            DefaultStyleKey = typeof(WeekView);
            Loaded += WeekView_Loaded;
        }

        void WeekView_Loaded(object sender, RoutedEventArgs e)
        {
            StartingDay = DayOfWeek.Sunday;
            InitSchedule();
            Loaded -= WeekView_Loaded;
        }

        private void InitSchedule()
        {

        }

        /// <summary>
        /// Sets the days of the week to display in the <see cref="WeeView">WeekView</see> Calendar control to the desired <see cref="DayOfWeek">DayOfWeek(s)</see>.
        /// </summary>
        /// <example>
        /// This example shows how to set the days of the week of this control to Monday and Sunday
        /// <code>
        /// var view = new WeekView();
        /// view.SetDaysOfWeek(DayOfWeek.Monday, DayOfWeek.Sunday);
        /// </code>
        /// </example>
        /// <param name="days"></param>
        public void SetDaysOfWeek(params DayOfWeek[] days)
        {
            if (days == null || days.Length == 0)
                return;
            startingDay = days[0];
            DaysOfWeek = days;
        }

        /// <summary>
        /// Updates the <see cref="DaysOfWeek">DaysOfWeek</see> field.
        /// </summary>
        private void UpdateDaysOfWeek()
        {
            var days = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>();
            int dayIndex = (int)StartingDay;
            DaysOfWeek = days.Skip(dayIndex).Concat(days.Take(dayIndex)).ToArray();            
            GenerateWeekDays();            
        }

        #region Header Methods
        private void GenerateWeekDays()
        {
            Grid daysGrid = (Grid)GetTemplateChild(PART_DAY_NAME);
            if (daysGrid == null)
                return;
            daysGrid.Children.Clear();
            daysGrid.ColumnDefinitions.Clear();
            for(int i = 0; i < DaysOfWeek.Length; i++)
            {
                DayOfWeek day = DaysOfWeek[i];
                daysGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(DaysOfWeek.Length, GridUnitType.Star) });
                TextBlock weekBlock = GenerateWeekHeaderBlock(day);
                Grid.SetColumn(weekBlock, i);
                daysGrid.Children.Add(weekBlock);
            }
        }

        private TextBlock GenerateWeekHeaderBlock(DayOfWeek day)
        {
            TextBlock block = new TextBlock() { VerticalAlignment = System.Windows.VerticalAlignment.Bottom, Foreground = Foreground, FontSize = 18, HorizontalAlignment = System.Windows.HorizontalAlignment.Center };
            block.Text = day.ToString().Substring(0, 3);
            return block;
        }

        #endregion
    }
}

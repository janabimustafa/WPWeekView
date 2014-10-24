﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPWeekView.Controls
{
    [TemplatePart(Name = PART_WEEK_CANVAS_NAME, Type = typeof(Canvas))]
    [TemplatePart(Name = PART_DAY_NAME, Type = typeof(Grid))]
    [TemplatePart(Name = PART_ROOT_NAME, Type = typeof(Grid))]
    [TemplatePart(Name = PART_SCHEDULE_ITEMS_NAME, Type = typeof(Grid))]
    public class WeekView : Control
    {
        private const string PART_SCHEDULE_ITEMS_NAME = "PART_SCHEDULE_ITEMS";
        private const string PART_WEEK_CANVAS_NAME = "PART_WEEK_CANVAS";
        private const string PART_DAY_NAME = "PART_DAY_NAME";
        private const string PART_ROOT_NAME = "PART_ROOT";

        #region Dependency Properties
        /// <summary>
        /// The starting hour in a 24-hour format
        /// A valid value would be an hour between 0 inclusive and 24 exclusive
        /// </summary>
        public int StartingHour
        {
            get { return (int)GetValue(StartingHourProperty); }
            set { SetValue(StartingHourProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartingHour.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartingHourProperty =
            DependencyProperty.Register("StartingHour", typeof(int), typeof(WeekView), new PropertyMetadata(6));


        /// <summary>
        /// The ending hour in a 24-hour format       
        /// A valid value would be an hour between 0 inclusive and 24 exclusive
        /// </summary>
        public int EndingHour
        {
            get { return (int)GetValue(EndingHourProperty); }
            set { SetValue(EndingHourProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EndingHour.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndingHourProperty =
            DependencyProperty.Register("EndingHour", typeof(int), typeof(WeekView), new PropertyMetadata(23));



        /// <summary>
        /// Gets or sets the starting day of the week to be dispalyed
        /// </summary>
        public DayOfWeek StartingDay
        {
            get { return (DayOfWeek)GetValue(StartingDayProperty); }
            set
            {
                SetValue(StartingDayProperty, value);
                UpdateDaysOfWeek();
            }
        }

        // Using a DependencyProperty as the backing store for StartingDay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartingDayProperty =
            DependencyProperty.Register("StartingDay", typeof(DayOfWeek), typeof(WeekView), new PropertyMetadata(DayOfWeek.Sunday));




        /// <summary>
        /// Gets the collection of WeekViewCell items
        /// </summary>
        public IEnumerable<WeekViewCell> Items
        {
            get { return (IEnumerable<WeekViewCell>)GetValue(ItemsProperty); }
            private set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(IEnumerable<WeekViewCell>), typeof(WeekView), new PropertyMetadata(default(IEnumerable<WeekViewCell>)));


        /// <summary>
        /// The currently selected WeekViewCell item
        /// </summary>
        public WeekViewCell SelectedItem
        {
            get { return (WeekViewCell)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(WeekViewCell), typeof(WeekView), new PropertyMetadata(default(WeekViewCell)));


        #endregion
        #region Events
        public event EventHandler<WeekViewCell> SelectionChanged;
        #endregion
        /// <summary>
        /// The days of week to be used, starting from the <see cref="StartingDay">StartingDay</see>
        /// </summary>
        private DayOfWeek[] DaysOfWeek;

        public WeekView()
            : base()
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
            Grid itemsGrid = (Grid)GetTemplateChild(PART_SCHEDULE_ITEMS_NAME);
            FillEmptyGrid(itemsGrid);
        }

        private void FillEmptyGrid(Grid grid)
        {
            grid.Children.Clear();
            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();
            int rowCount = EndingHour - StartingHour;
            int colCount = DaysOfWeek.Length + 1;
            for (int r = 0; r < rowCount; r++)
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });
            for (int c = 0; c < colCount; c++)
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(colCount, GridUnitType.Star) });
            for (int r = 0; r < rowCount; r++)
            {
                for (int c = 0; c < colCount; c++)
                {
                    if (c == 0)
                    {
                        var timeBlock = CreateTimeLabel(r, c);
                        grid.Children.Add(timeBlock);
                    }
                    else
                    {
                        var emptyCell = CreateEmptyCell(r, c);
                        grid.Children.Add(emptyCell);
                    }
                }
            }
        }

        private WeekViewCell CreateEmptyCell(int r, int c)
        {
            var cell = new WeekViewCell();
            Grid.SetRow(cell, r);
            Grid.SetColumn(cell, c);
            cell.StartingTime = StartingHour + r;
            cell.Day = DaysOfWeek[c - 1];
            cell.BorderBrush = BorderBrush;
            cell.HorizontalAlignment = HorizontalAlignment.Stretch;
            cell.VerticalAlignment = VerticalAlignment.Stretch;
            return cell;
        }

        private TextBlock CreateTimeLabel(int r, int c)
        {
            var time = new TextBlock();
            time.Text = String.Format("{0}:00", (StartingHour + r).ToString());
            time.VerticalAlignment = VerticalAlignment.Top;
            time.Margin = new Thickness(0, -8, 0, 0);
            time.Foreground = Foreground;
            time.FontSize = 12;
            time.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetRow(time, r);
            Grid.SetColumn(time, c);
            return time;
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
            DaysOfWeek = days;
            StartingDay = days[0];
            InitSchedule();
        }

        /// <summary>
        /// Updates the <see cref="DaysOfWeek">DaysOfWeek</see> field.
        /// </summary>
        private void UpdateDaysOfWeek()
        {
            var days = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>();
            int dayIndex = (int)StartingDay;
            if (DaysOfWeek == null)
                DaysOfWeek = days.Skip(dayIndex).Concat(days.Take(dayIndex)).ToArray();
            else if (DaysOfWeek[0] != StartingDay)
            {
                dayIndex = Array.IndexOf(DaysOfWeek, StartingDay);
                if (dayIndex == -1)
                    throw new ArgumentOutOfRangeException(String.Format("The day {0} does not exist in the days collection {1}", StartingDay.ToString(), String.Join(", ", DaysOfWeek)));
                DaysOfWeek = DaysOfWeek.Skip(dayIndex).Concat(DaysOfWeek.Take(dayIndex)).ToArray();
            }
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
            for (int i = 0; i < DaysOfWeek.Length + 1; i++)
                daysGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(DaysOfWeek.Length + 1, GridUnitType.Star) });
            for (int i = 0; i < DaysOfWeek.Length; i++)
            {
                DayOfWeek day = DaysOfWeek[i];
                TextBlock weekBlock = GenerateWeekHeaderBlock(day);
                Grid.SetColumn(weekBlock, i + 1);
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

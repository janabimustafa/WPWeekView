using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPWeekView.Controls
{
    [TemplatePart(Name = PART_CONTENT_PRESENTER_NAME, Type = typeof(ContentPresenter))]
    public class WeekViewCell : ContentControl
    {
        private const string PART_CONTENT_PRESENTER_NAME = "PART_CONTENT_PRESENTER";



        public DayOfWeek Day
        {
            get { return (DayOfWeek)GetValue(DayProperty); }
            set { SetValue(DayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Day.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DayProperty =
            DependencyProperty.Register("Day", typeof(DayOfWeek), typeof(WeekViewCell), new PropertyMetadata(default(DayOfWeek)));

        

        public double StartingTime
        {
            get { return (double)GetValue(StartingTimeProperty); }
            set { SetValue(StartingTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartingTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartingTimeProperty =
            DependencyProperty.Register("StartingTime", typeof(double), typeof(WeekViewCell), new PropertyMetadata(default(double)));



        public DateTime EndingTime
        {
            get { return (DateTime)GetValue(EndingTimeProperty); }
            set { SetValue(EndingTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EndingTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndingTimeProperty =
            DependencyProperty.Register("EndingTime", typeof(DateTime), typeof(WeekViewCell), new PropertyMetadata(default(DateTime)));


        public WeekViewCell()
        {
            DefaultStyleKey = typeof(WeekViewCell);
        }
    }
}

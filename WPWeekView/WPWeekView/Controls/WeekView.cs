using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPWeekView.Controls
{
    class WeekView : Control
    {
        /// <summary>
        /// <see cref="StartingDay">See StartingDay Property</see>
        /// </summary>
        private DayOfWeek startingDay;

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
        private IEnumerable<DayOfWeek> DaysOfWeek;

        public WeekView()
        {            
            DefaultStyleKey = typeof(WeekView);
            StartingDay = DayOfWeek.Sunday;
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
            //TODO: Set DaysOfWeek to a new Array of DayOfWeek starting from the given StartingDay.
        }


    }
}

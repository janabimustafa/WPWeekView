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
        /// Gets or sets the starting day of the week to be dispalyed
        /// </summary>
        public DayOfWeek StartingDay { get; set; }

        public WeekView()
        {            
            DefaultStyleKey = typeof(WeekView);
        }

        /// <summary>
        /// Sets the days of the week to display in the <see cref="WeeView">WeekView</see> Calendar control to the desired <see cref="DayOfWeek">DayOfWeek(s)</see>.
        /// </summary>
        /// <example>
        /// This example shows how to set the days of the week of this control to Monday and Sunday
        /// <code>
        /// var view = new WeekView();
        /// view.SetDaysOfWeek(DayOfWeek.Monday | DayOfWeek.Sunday);
        /// </code>
        /// </example>
        /// <param name="days"></param>
        public void SetDaysOfWeek(params DayOfWeek days)
        {

        }


    }
}

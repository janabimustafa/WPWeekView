using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPWeekView.Controls
{
    [TemplatePart(Name = PART_CONTENT_PRESENTER_NAME, Type = typeof(ContentPresenter)]
    public class WeekViewCell : ContentControl
    {
        private const string PART_CONTENT_PRESENTER_NAME = "PART_CONTENT_PRESENTER";
        public WeekViewCell()
        {
            DefaultStyleKey = typeof(WeekViewCell);                   
        }
    }
}

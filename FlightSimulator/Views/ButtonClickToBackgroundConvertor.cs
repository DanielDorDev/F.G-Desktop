using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace FlightSimulator.Views
{
    class ButtonClickToBackgroundConvertor : IValueConverter
    {
        // Converting between the recievd value (which is bollean) into the backgroud color of the TextBox in the AutoPilot view.
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If true : White ; Otherwise : False.
            var color = (value is bool && (bool)value) ? Colors.White : Color.FromRgb(241, 193, 193);
            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

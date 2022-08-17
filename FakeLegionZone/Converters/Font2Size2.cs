using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FakeLegionZone.Converters
{
    public class Font2Size2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null)
                return false;
            int v = 0;
            int.TryParse(value.ToString(), out v);
            if (v == 20)
            {
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return 0;
            bool v = false;
            bool.TryParse(value.ToString(), out v);
            if (v)
            {
                return 20;
            }
            return 0;
        }
    }
}

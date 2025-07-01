using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Carousel.Converters
{
    public class BoolToBrushConverter : IValueConverter
    {
        public Brush SelectedBrush { get; set; } = Brushes.DarkBlue;
        public Brush UnselectedBrush { get; set; } = Brushes.LightGray;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool b && b ? SelectedBrush : UnselectedBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException ();
        }
    }
}
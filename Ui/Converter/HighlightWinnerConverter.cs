using BierPongTurnier.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace BierPongTurnier.Ui.Converter
{
    public class HighlightWinnerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var self = (TeamPosition)parameter;
            var pos = (TeamPosition)value;

            if (self == pos && pos != TeamPosition.NONE)
            {
                Console.WriteLine("HIGHLIGHT WINNER");
                return FontWeights.Bold;
            }
            else
            {
                return FontWeights.Normal;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Globalization;
using System.Windows.Data;

namespace BierPongTurnier.Ui.Converter
{
    internal class SecondsToTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var time = (int)value;
            var minutes = time / 60;
            var seconds = time % 60;

            if (minutes == 0 || minutes >= (Constants.GAMETIME_MINUTES - 1))
            {
                return (minutes < 10 ? "0" + minutes : "" + minutes) + ":" + (seconds < 10 ? "0" + seconds : "" + seconds);
            }
            else
            {
                return minutes + " Min.";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
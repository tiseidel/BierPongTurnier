using System;
using System.Globalization;
using System.Windows.Data;

namespace BierPongTurnier.Ui.Converter
{
    internal class PlayPauseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var state = (State)value;
            if (state == State.Paused)
            {
                return "/Assets/outline_play_arrow_black_36dp.png";
            }
            else if (state == State.Playing)
            {
                return "/Assets/outline_pause_black_36dp.png";
            }
            throw new InvalidOperationException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
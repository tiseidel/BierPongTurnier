using BierPongTurnier.Model;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BierPongTurnier.Ui.Converter
{
    public class HighlightWinnerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            /*
             * winner: The position of the winning team (left/right/none)
             * self: The position this ui element represents
             *
             * If this ui elements represents the winner position -> display bold
             */
            try
            {
                var winner = (TeamPosition)value;
                var self = (TeamPosition)parameter;

                return (self == winner && winner != TeamPosition.NONE) ? FontWeights.Bold : FontWeights.Normal;
            }
            catch (Exception)
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
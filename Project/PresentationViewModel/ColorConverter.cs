using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Project.Presentation.ViewModel;

public class ColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        double? color = (double?)value;
        if (color == null)
        {
            return new BindingNotification(new NullReferenceException(), BindingErrorType.Error);
        }

        byte green = (byte)((1 - color) * 200);
        
        return new SolidColorBrush(Color.FromRgb(220, green, 0));
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return new BindingNotification(new InvalidOperationException(), BindingErrorType.Error);
    }
}

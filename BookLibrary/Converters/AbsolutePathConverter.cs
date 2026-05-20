using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.IO;

namespace BookLibrary.Converters;

public class AbsolutePathConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is string relativePath && !string.IsNullOrWhiteSpace(relativePath))
        {
            var absolutePath = Path.Combine(AppContext.BaseDirectory, relativePath);
            if (File.Exists(absolutePath))
            {
                return new BitmapImage(new Uri(absolutePath));
            }
        }
        
        return null!;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}

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
            if (!File.Exists(absolutePath))
            {
                absolutePath = Path.Combine(AppContext.BaseDirectory, "AppX", relativePath);
            }

            if (File.Exists(absolutePath))
            {
                try
                {
                    var bitmap = new BitmapImage();
                    using var stream = File.OpenRead(absolutePath);
                    bitmap.SetSource(stream.AsRandomAccessStream());
                    return bitmap;
                }
                catch
                {
                    return new BitmapImage(new Uri(absolutePath));
                }
            }
        }
        
        return null!;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}

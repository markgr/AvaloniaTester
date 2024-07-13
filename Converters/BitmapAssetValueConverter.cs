using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Globalization;
using System.Reflection;

namespace AvaloniaTester.Converters
{
    public class BitmapAssetValueConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is null)
            {
                return null;
            }

            if (value is not string rawUri || !targetType.IsAssignableFrom(typeof(Bitmap)))
            {
                throw new NotSupportedException();
            }

            Uri uri;

            if (rawUri.StartsWith("avares://"))
            {
                uri = new Uri(rawUri);
            }
            else
            {
                var assemblyName = Assembly.GetEntryAssembly()?.GetName().Name;
                uri = new Uri($"avares://{assemblyName}/{rawUri.TrimStart('/')}");
            }

            var asset = AssetLoader.Open(uri);

            return new Bitmap(asset);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System.Windows.Media;
using System.Windows.Media.Imaging;
using ImageProcessing.Core;

namespace ImageProcessing.Wpf
{
    public static class Extensions
    {
        public static ImageSource ToImageSource(this IFrame<ArgbPixel> frame) {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = frame.ToBitmapStream();
            bitmap.EndInit();

            return bitmap;
        }
    }
}
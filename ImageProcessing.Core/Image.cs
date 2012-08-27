using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace ImageProcessing.Core
{
    public static class Image
    {
        public static IImage<T> FromFile<T>(string filename) where T : IPixel {
            var bitmap = new Bitmap(filename);
            var pixelType = typeof (T);
            if (pixelType == typeof (RGBPixel)) return new RGBImage(bitmap) as IImage<T>;

            throw new NotImplementedException();
        }

        public static void ToPng<T>(IImage<T> image, string filename) where T : IPixel {
            image.ToBitmap().Save(filename, ImageFormat.Png);
        }

        public static IImage<TOut> Filter<TIn, TOut>(this IImage<TIn> image, IFilter<TIn, TOut> filter) where TIn : IPixel where TOut : IPixel {
            return filter.Process(image);
        }

        public static IImage<T> FromPixels<T>(int width, int height, T[][] pixels) where T : IPixel {
            if (typeof (T) == typeof (GreyscalePixel)) return new GreyscaleImage(width, height, pixels as GreyscalePixel[][]) as IImage<T>;

            if (typeof (T) == typeof (RGBPixel)) return new RGBImage(width, height, pixels as RGBPixel[][]) as IImage<T>;

            if (typeof (T) == typeof (ValuePixel)) return new ValueImage(width, height, pixels as ValuePixel[][]) as IImage<T>;
            throw new NotImplementedException();
        }
    }

    public class ValueImage : ImageBase<ValuePixel>
    {
        private readonly int _minValue;
        private readonly int _maxValue;

        public ValueImage(int width, int height, ValuePixel[][] pixels)
            : base(width, height, pixels) {
            _minValue = pixels.Min(r => r.Min(p => p.Value));
            _maxValue = pixels.Max(r => r.Max(p => p.Value));
        }

        protected override byte GetRed(ValuePixel pixel) {

            return (byte)((pixel.Value - _minValue)*255/(_maxValue - _minValue));
        }

        protected override byte GetGreen(ValuePixel pixel)
        {
            return (byte)((pixel.Value - _minValue) * 255 / (_maxValue - _minValue));
        }

        protected override byte GetBlue(ValuePixel pixel)
        {
            return (byte)((pixel.Value - _minValue) * 255 / (_maxValue - _minValue));
        }

        protected override ValuePixel GetPixel(byte red, byte green, byte blue) {
            return new ValuePixel((red + green + blue)/3);
        }
    }
}
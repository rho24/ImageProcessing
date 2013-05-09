using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessing.Core
{
    public static class ImgProc
    {
        public static IFrame<ArgbPixel> ImageFromFile(string fileName) {
            return new StaticFileImage(fileName);
        }
    }

    public class ArgbPixel
    {
        public byte A { get; private set; }
        public byte R { get; private set; }
        public byte G { get; private set; }
        public byte B { get; private set; }

        public ArgbPixel(byte a, byte r, byte g, byte b) {
            A = a;
            R = r;
            G = g;
            B = b;
        }
    }

    public class StaticFileImage : IFrame<ArgbPixel>
    {
        public StaticFileImage(string fileName) {
            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            var bitmap = new Bitmap(fileName);
            Height = bitmap.Height;
            Width = bitmap.Width;
            BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            var stride = bmData.Stride;
            var scan0 = bmData.Scan0;

            var rows = new List<List<ArgbPixel>>();
            unsafe {
                var p = (byte*) (void*) scan0;
                var nOffset = stride - Width*3;

                for (int y = 0; y < bitmap.Height; ++y) {
                    var currentRow = new List<ArgbPixel>();
                    for (int x = 0; x < bitmap.Width; ++x) {
                        var red = p[2];
                        var green = p[1];
                        var blue = p[0];

                        var pixel = new ArgbPixel(127, red, green, blue);

                        currentRow.Add(pixel);
                        p += 3;
                    }
                    p += nOffset;
                    rows.Add(currentRow);
                }
            }
            bitmap.UnlockBits(bmData);

            Data = rows;
        }

        public IEnumerable<IEnumerable<ArgbPixel>> Data { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }
    }

    public interface IFrame<out TPixelType>
    {
        IEnumerable<IEnumerable<TPixelType>> Data { get; }
        int Height { get; }
        int Width { get; }
    }
}
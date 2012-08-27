using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessing.Core
{
    public abstract class ImageBase<T> : IImage<T> where T : IPixel
    {
        protected ImageBase(int width, int height, T[][] pixels) {
            Width = width;
            Height = height;
            Pixels = pixels;
        }

        protected ImageBase(Bitmap bitmap) {
            Width = bitmap.Width;
            Height = bitmap.Height;
            Pixels = GetPixels(bitmap);
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public T[][] Pixels { get; private set; }

        public Bitmap ToBitmap() {
            var img = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            var stride = bmData.Stride;
            var scan0 = bmData.Scan0;

            unsafe {
                var p = (byte*) (void*) scan0;
                var nOffset = stride - img.Width*3;

                foreach (var row in Pixels) {
                    foreach (var pixel in row) {
                        p[2] = GetRed(pixel);
                        p[1] = GetGreen(pixel);
                        p[0] = GetBlue(pixel);

                        p += 3;
                    }
                    p += nOffset;
                }
            }
            img.UnlockBits(bmData);

            return img;
        }

        protected abstract byte GetRed(T pixel);
        protected abstract byte GetGreen(T pixel);
        protected abstract byte GetBlue(T pixel);

        private T[][] GetPixels(Bitmap bitmap) {
            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            var stride = bmData.Stride;
            var scan0 = bmData.Scan0;

            var rows = new List<T[]>();
            unsafe {
                var p = (byte*) (void*) scan0;
                var nOffset = stride - Width*3;

                for (int y = 0; y < bitmap.Height; ++y) {
                    var currentRow = new List<T>();
                    for (int x = 0; x < bitmap.Width; ++x) {
                        var red = p[2];
                        var green = p[1];
                        var blue = p[0];

                        var pixel = GetPixel(red, green, blue);

                        currentRow.Add(pixel);
                        p += 3;
                    }
                    p += nOffset;
                    rows.Add(currentRow.ToArray());
                }
            }
            bitmap.UnlockBits(bmData);

            return rows.ToArray();
        }

        protected abstract T GetPixel(byte red, byte green, byte blue);
    }
}
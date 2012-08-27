using System.Collections.Generic;
using System.Drawing;

namespace ImageProcessing.Core
{
    internal class RGBImage : ImageBase<RGBPixel>
    {
        public RGBImage(Bitmap bitmap) : base(bitmap) {}

        public RGBImage(int width, int height, RGBPixel[][] pixels)
            : base(width, height, pixels) {}

        protected override byte GetRed(RGBPixel pixel) {
            return (byte) pixel.R;
        }

        protected override byte GetGreen(RGBPixel pixel) {
            return (byte) pixel.G;
        }

        protected override byte GetBlue(RGBPixel pixel) {
            return (byte) pixel.B;
        }

        protected override RGBPixel GetPixel(byte red, byte green, byte blue) {
            return new RGBPixel(red, green, blue);
        }
    }
}
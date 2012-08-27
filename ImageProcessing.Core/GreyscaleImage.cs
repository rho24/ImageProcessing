using System.Collections.Generic;

namespace ImageProcessing.Core
{
    public class GreyscaleImage : ImageBase<GreyscalePixel>
    {
        public GreyscaleImage(int width, int height, GreyscalePixel[][] pixels)
            : base(width, height, pixels) {}

        protected override byte GetRed(GreyscalePixel pixel) {
            return (byte) pixel.Value;
        }

        protected override byte GetGreen(GreyscalePixel pixel) {
            return (byte) pixel.Value;
        }

        protected override byte GetBlue(GreyscalePixel pixel) {
            return (byte) pixel.Value;
        }

        protected override GreyscalePixel GetPixel(byte red, byte green, byte blue) {
            return new GreyscalePixel((red + green + blue)/3);
        }
    }
}
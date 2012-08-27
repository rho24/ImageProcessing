namespace ImageProcessing.Core
{
    public class GreyscaleFilter : OneToOneFilter<RGBPixel, GreyscalePixel>
    {
        public override GreyscalePixel Map(RGBPixel pixel) {
            return new GreyscalePixel((pixel.R + pixel.G + pixel.B)/3);
        }
    }
}
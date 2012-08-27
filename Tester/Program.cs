using ImageProcessing.Core;

namespace Tester
{
    internal class Program
    {
        private static void Main(string[] args) {
            var image = Image.FromFile<RGBPixel>(@"C:\Users\Rich\Pictures\From Rich's Phone\Camera roll\WP_000003.jpg");

            var grey = image.Filter(new GreyscaleFilter());
            var red = image.Filter(new RedFilter());
            var green = image.Filter(new GreenFilter());
            var blue = image.Filter(new BlueFilter());
            var sobelHorizontal = grey.Filter(new SobelHorizontal());
            var sobelVertical = grey.Filter(new SobelVertical());

            Image.ToPng(image, "orig.png");
            Image.ToPng(grey, "grey.png");
            Image.ToPng(red, "red.png");
            Image.ToPng(green, "green.png");
            Image.ToPng(blue, "blue.png");
            Image.ToPng(sobelHorizontal, "sobelH.png");
            Image.ToPng(sobelVertical, "sobelV.png");
        }
    }

    internal class RedFilter : OneToOneFilter<RGBPixel, RGBPixel>
    {
        public override RGBPixel Map(RGBPixel pixel) {
            return new RGBPixel(pixel.R, 0, 0);
        }
    }

    internal class GreenFilter : OneToOneFilter<RGBPixel, RGBPixel>
    {
        public override RGBPixel Map(RGBPixel pixel) {
            return new RGBPixel(0, pixel.G, 0);
        }
    }

    internal class BlueFilter : OneToOneFilter<RGBPixel, RGBPixel>
    {
        public override RGBPixel Map(RGBPixel pixel) {
            return new RGBPixel(0, 0, pixel.B);
        }
    }

    internal class SobelHorizontal : TemplateFilter3<GreyscalePixel, ValuePixel>
    {
        protected override ValuePixel DefaultPixel() {
            return new ValuePixel(0);
        }

        public override ValuePixel Map(Template<GreyscalePixel> pixels) {
            return new ValuePixel(pixels.TopLeft.Value + 2*pixels.CenterLeft.Value + pixels.BottomLeft.Value - pixels.TopRight.Value - 2*pixels.CenterRight.Value - pixels.BottomRight.Value);
        }
    }

    internal class SobelVertical : TemplateFilter3<GreyscalePixel, ValuePixel>
    {
        protected override ValuePixel DefaultPixel()
        {
            return new ValuePixel(0);
        }

        public override ValuePixel Map(Template<GreyscalePixel> pixels)
        {
            return new ValuePixel(pixels.TopLeft.Value + 2 * pixels.TopCenter.Value + pixels.TopRight.Value - pixels.BottomLeft.Value - 2 * pixels.BottomCenter.Value - pixels.BottomRight.Value);
        }
    }
}
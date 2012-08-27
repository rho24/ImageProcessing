namespace ImageProcessing.Core
{
    public class RGBPixel : IPixel
    {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        public RGBPixel(int r, int g, int b) {
            R = r;
            G = g;
            B = b;
        }
    }
}
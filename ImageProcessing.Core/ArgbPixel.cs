namespace ImageProcessing.Core
{
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
}
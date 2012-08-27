namespace ImageProcessing.Core
{
    public class GreyscalePixel : IPixel
    {
        public int Value { get; set; }

        public GreyscalePixel(int value) {
            Value = value;
        }
    }
}
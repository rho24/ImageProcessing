namespace ImageProcessing.Core
{
    public class ValuePixel : IPixel
    {
        public int Value { get; set; }

        public ValuePixel(int value) {
            Value = value;
        }
    }
}
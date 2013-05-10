namespace ImageProcessing.Core
{
    public interface IPixel<out T>
    {
        T Value { get; }
        int X { get; }
        int Y { get; }
        IFrame<T> Frame { get; }
    }
}
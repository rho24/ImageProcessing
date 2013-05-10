using System;

namespace ImageProcessing.Core
{
    public static class Pixel
    {
        public static IPixel<T> Create<T>(T value, int x, int y, Func<IFrame<T>> frame) {
            return new Pixel<T>(value, x, y, frame);
        }
    }

    internal class Pixel<T> : IPixel<T>
    {
        private readonly Func<IFrame<T>> _frame;

        public Pixel(T value, int x, int y, Func<IFrame<T>> frame) {
            _frame = frame;
            Value = value;
            X = x;
            Y = y;
        }

        public T Value { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public IFrame<T> Frame {
            get { return _frame(); }
        }
    }
}
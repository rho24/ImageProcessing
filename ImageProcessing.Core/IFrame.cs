using System.Collections.Generic;

namespace ImageProcessing.Core
{
    public interface IFrame<out TPixelType>
    {
        IIndexedSequence<IIndexedSequence<IPixel<TPixelType>>> Data { get; }
        int Height { get; }
        int Width { get; }
    }

    public class Frame<TPixelType> : IFrame<TPixelType>
    {
        public IIndexedSequence<IIndexedSequence<IPixel<TPixelType>>> Data { get; private set; }

        public Frame(int height, int width, IIndexedSequence<IIndexedSequence<IPixel<TPixelType>>> data) {
            Data = data;
            Height = height;
            Width = width;
        }

        public int Height { get; private set; }
        public int Width { get; private set; }
    }
}
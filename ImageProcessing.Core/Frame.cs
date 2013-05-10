using System.Collections.Generic;

namespace ImageProcessing.Core
{
    public class Frame<TPixelType> : IFrame<TPixelType>
    {
        public IEnumerable<IEnumerable<IPixel<TPixelType>>> Data { get; private set; }

        public Frame(int height, int width, IEnumerable<IEnumerable<IPixel<TPixelType>>> data) {
            Data = data;
            Height = height;
            Width = width;
        }

        public int Height { get; private set; }
        public int Width { get; private set; }
    }
}
using System.Collections.Generic;

namespace ImageProcessing.Core
{
    public interface IFrame<out TPixelType>
    {
        IEnumerable<IEnumerable<IPixel<TPixelType>>> Data { get; }
        int Height { get; }
        int Width { get; }
    }
}
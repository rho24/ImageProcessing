using System.Collections.Generic;

namespace ImageProcessing.Core
{
    public interface IFrame<out TPixelType>
    {
        IEnumerable<IEnumerable<TPixelType>> Data { get; }
        int Height { get; }
        int Width { get; }
    }
}
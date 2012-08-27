using System.Collections.Generic;
using System.Drawing;

namespace ImageProcessing.Core
{
    public interface IImage<out T> where T : IPixel
    {
        int Width { get; }
        int Height { get; }
        T[][] Pixels { get; }
        Bitmap ToBitmap();
    }
}
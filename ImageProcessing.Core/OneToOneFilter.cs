using System.Collections.Generic;
using System.Linq;

namespace ImageProcessing.Core
{
    public abstract class OneToOneFilter<TIn, TOut> : IFilter<TIn, TOut> where TIn : IPixel where TOut : IPixel
    {
        public IImage<TOut> Process(IImage<TIn> image) {
            return Image.FromPixels(image.Width, image.Height, image.Pixels.Select(r => r.Select(Map).ToArray()).ToArray());
        }

        public abstract TOut Map(TIn pixel);
    }
}
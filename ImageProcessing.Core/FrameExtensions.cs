using System.IO;

namespace ImageProcessing.Core
{
    public static class FrameExtensions
    {
        public static Stream ToBitmapStream(this IFrame<ArgbPixel> frame) {
            return new BitmapStream(frame);
        }

        public static IFrame<TOut> Filter<TIn, TOut>(this IFrame<TIn> frame, IFilter<TIn, TOut> filter) {
            return filter.Execute(frame);
        }
    }
}
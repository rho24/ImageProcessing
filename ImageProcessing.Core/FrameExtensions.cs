using System;
using System.IO;
using ImageProcessing.Core.Filters;

namespace ImageProcessing.Core
{
    public static class FrameExtensions
    {
        public static Stream ToBitmapStream(this IFrame<Argb> frame) {
            return new BitmapStream(frame);
        }

        public static IFrame<TOut> Filter<TIn, TOut>(this IFrame<TIn> frame, IFilter<TIn, TOut> filter) {
            return filter.Execute(frame);
        }

        public static IFrame<Argb> ToArgb(this IFrame<int> frame) {
            return frame.Filter(new ArgbFilter());
        }

        public static IFrame<int> ToGreyScale(this IFrame<Argb> frame) {
            return frame.Filter(new GreyscaleFilter());
        }

        public static IFrame<TOut> Zip<TIn1, TIn2, TOut>(this IFrame<TIn1> frame1, IFrame<TIn2> frame2, Func<TIn1, TIn2, TOut> projection) {
            return new FrameZipper<TIn1, TIn2, TOut>(projection).Execute(frame1, frame2);
        }

        public static IFrame<TOut> Select<TIn, TOut>(this IFrame<TIn> frame, Func<TIn, TOut> projection) {
            return frame.Filter(new SelectFilter<TIn, TOut>(projection));
        }

        public static IFrame<int> Range(this IFrame<int> frame, int min, int max) {
            return frame.Filter(new RangeFilter(min, max));
        }
    }
}
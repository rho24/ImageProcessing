using System;
using System.IO;
using System.Linq;
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

    public class RangeFilter : IFilter<int, int>
    {
        private readonly int _max;
        private readonly int _min;
        private readonly int _range;

        public RangeFilter(int min, int max) {
            _min = min;
            _max = max;
            _range = max - min;
        }

        public IFrame<int> Execute(IFrame<int> frame) {
            var frameMin = new Lazy<int>(() => frame.Data.SelectMany(r => r).Min(p => p.Value));
            var frameMax = new Lazy<int>(() => frame.Data.SelectMany(r => r).Max(p => p.Value));
            var frameRange = new Lazy<int>(() => frameMax.Value - frameMin.Value);

            IFrame<int>[] resultFrame = {null};

            resultFrame[0] = new Frame<int>(frame.Height, frame.Width,
                                            frame.Data.Select(r => r.Select(
                                                p => Pixel.Create(((p.Value - frameMin.Value)*_range/frameRange.Value) + _min, p.X, p.Y, () => resultFrame[0]))).ToIndexedSequence());

            return resultFrame[0];
        }
    }
}
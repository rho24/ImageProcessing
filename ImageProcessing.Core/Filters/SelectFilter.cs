using System;
using System.Linq;

namespace ImageProcessing.Core.Filters
{
    public class SelectFilter<TIn, TOut> : IFilter<TIn, TOut>
    {
        private readonly Func<TIn, TOut> _projection;

        public SelectFilter(Func<TIn, TOut> projection) {
            _projection = projection;
        }

        public IFrame<TOut> Execute(IFrame<TIn> frame) {
            IFrame<TOut>[] resultFrame = {null};

            resultFrame[0] = new Frame<TOut>(frame.Height, frame.Width,
                                             frame.Data.Select(r => r.Select(
                                                 p => Pixel.Create(_projection(p.Value), p.X, p.Y, () => resultFrame[0]))));

            return resultFrame[0];
        }
    }
}
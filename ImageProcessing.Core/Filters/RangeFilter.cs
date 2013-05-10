using System;
using System.Linq;

namespace ImageProcessing.Core.Filters
{
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
                                                p => Pixel.Create(((p.Value - frameMin.Value)*_range/frameRange.Value) + _min, p.X, p.Y, () => resultFrame[0]))));

            return resultFrame[0];
        }
    }
}
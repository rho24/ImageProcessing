using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ImageProcessing.Core.Filters
{
    public class HysteresisThresholdingFilter : IFilter<double, bool>
    {
        private readonly double _maxPercentage;
        private readonly double _minPercentage;

        public HysteresisThresholdingFilter(double minPercentage, double maxPercentage) {
            _minPercentage = minPercentage;
            _maxPercentage = maxPercentage;
        }

        public IFrame<bool> Execute(IFrame<double> frame) {
            IFrame<bool>[] resultFrame = {null};

            resultFrame[0] = new Frame<bool>(frame.Height, frame.Width,
                                             new LazyEnumerable<IEnumerable<IPixel<bool>>>(new Lazy<IEnumerable<IEnumerable<IPixel<bool>>>>(() => Filter(frame, () => resultFrame[0]))));

            return resultFrame[0];
        }

        private IEnumerable<IEnumerable<IPixel<bool>>> Filter(IFrame<double> frame, Func<IFrame<bool>> resultFrame) {
            var indexedFrameData = frame.Data.ToIndexedSequence();


            var maxValue = indexedFrameData.Max(p => p.Max(p2 => p2.Value));
            var minThreashold = maxValue*_minPercentage/100;
            var maxThreashold = maxValue*_maxPercentage/100;

            var results = new IPixel<bool>[frame.Height][];
            for (var i = 0; i < frame.Height; i++) results[i] = new IPixel<bool>[frame.Width];

            Action<int, int, double> checkPixel = null;

            checkPixel = (y, x, threshold) => {
                if (results[y][x] == null)
                    results[y][x] = new Pixel<bool>(false, x, y, resultFrame);

                if (results[y][x].Value)
                    return;

                if (indexedFrameData[y][x].Value >= threshold) {
                    results[y][x] = Pixel.Create(true, x, y, resultFrame);
                    checkPixel(y - 1, x - 1, minThreashold);
                    checkPixel(y - 1, x, minThreashold);
                    checkPixel(y - 1, x + 1, minThreashold);
                    checkPixel(y, x - 1, minThreashold);
                    checkPixel(y, x + 1, minThreashold);
                    checkPixel(y + 1, x - 1, minThreashold);
                    checkPixel(y + 1, x, minThreashold);
                    checkPixel(y + 1, x + 1, minThreashold);
                }
            };

            for (int forY = 0; forY < frame.Height; forY++) for (int forX = 0; forX < frame.Width; forX++) checkPixel(forY, forX, maxThreashold);

            return results;
        }
    }

    internal class LazyEnumerable<T> : IEnumerable<T>
    {
        private readonly Lazy<IEnumerable<T>> _innerEnumerable;

        public LazyEnumerable(Lazy<IEnumerable<T>> innerEnumerable) {
            _innerEnumerable = innerEnumerable;
        }

        public IEnumerator<T> GetEnumerator() {
            return _innerEnumerable.Value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
using System;
using System.Linq;

namespace ImageProcessing.Core
{
    public class FrameZipper<TIn1, TIn2, TOut>
    {
        private readonly Func<TIn1, TIn2, TOut> _projection;

        public FrameZipper(Func<TIn1, TIn2, TOut> projection) {
            _projection = projection;
        }

        public IFrame<TOut> Execute(IFrame<TIn1> frame1, IFrame<TIn2> frame2) {
            IFrame<TOut>[] resultFrame = {null};

            resultFrame[0] = new Frame<TOut>(frame1.Height, frame1.Width,
                                             frame1.Data.Zip(frame2.Data,
                                                             (r1, r2) => r1.Zip(r2,
                                                                                (p1, p2) => Pixel.Create(_projection(p1.Value, p2.Value), p1.X, p1.Y, () => resultFrame[0])))
                );

            return resultFrame[0];
        }
    }
}
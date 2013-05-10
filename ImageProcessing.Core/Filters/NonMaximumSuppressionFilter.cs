using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageProcessing.Core.Filters
{
    public class NonMaximumSuppressionFilter : IFilter<Vector2, Vector2>
    {
        public IFrame<Vector2> Execute(IFrame<Vector2> frame) {
            IFrame<Vector2>[] resultFrame = {null};

            resultFrame[0] = new Frame<Vector2>(frame.Height, frame.Width, Filter(frame.Data, () => resultFrame[0]));

            return resultFrame[0];
        }

        private IEnumerable<IEnumerable<IPixel<Vector2>>> Filter(IEnumerable<IEnumerable<IPixel<Vector2>>> frameData, Func<IFrame<Vector2>> resultFrame) {
            var indexedFrameData = new Lazy<IIndexedSequence<IIndexedSequence<IPixel<Vector2>>>>(() => frameData.ToIndexedSequence());

            return frameData.Select(r => r.Select(p => {
                if (Math.Abs(p.Value.Length - 0) < 0.001)
                    return p;

                var octant =
                    Convert.ToInt32(Math.Round((((p.Value.Angle/Math.PI) + 1)*4))) % 8;


                if (octant == 0 || octant == 4) {
                    return p.Value.Length > indexedFrameData.Value[p.Y][p.X - 1].Value.Length && p.Value.Length >= indexedFrameData.Value[p.Y][p.X + 1].Value.Length
                               ? p
                               : Pixel.Create(Vector2.Zero, p.X, p.Y, resultFrame);
                }
                if (octant == 1 || octant == 5) {
                    return p.Value.Length > indexedFrameData.Value[p.Y - 1][p.X - 1].Value.Length && p.Value.Length >= indexedFrameData.Value[p.Y + 1][p.X + 1].Value.Length
                               ? p
                               : Pixel.Create(Vector2.Zero, p.X, p.Y, resultFrame);
                }
                if (octant == 2 || octant == 6) {
                    return p.Value.Length > indexedFrameData.Value[p.Y - 1][p.X].Value.Length && p.Value.Length >= indexedFrameData.Value[p.Y + 1][p.X].Value.Length
                               ? p
                               : Pixel.Create(Vector2.Zero, p.X, p.Y, resultFrame);
                }
                if (octant == 3 || octant == 7) {
                    return p.Value.Length > indexedFrameData.Value[p.Y + 1][p.X - 1].Value.Length && p.Value.Length >= indexedFrameData.Value[p.Y - 1][p.X + 1].Value.Length
                               ? p
                               : Pixel.Create(Vector2.Zero, p.X, p.Y, resultFrame);
                }
                throw new InvalidOperationException("octant should always be between 0 and 7");
            }).Cache()).Cache();
        }
    }
}
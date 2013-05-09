using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageProcessing.Core
{
    public abstract class PixelFilter<TIn, TOut> : IFilter<TIn, TOut>
    {
        protected abstract Func<TIn, TOut> PixelProjection { get; }

        public IFrame<TOut> Execute(IFrame<TIn> frame) {
            return new WrappingFrame<TOut>(frame, PixelProjection);
        }

        #region Nested type: WrappingFrame

        public class WrappingFrame<T> : IFrame<TOut>
        {
            private readonly IFrame<TIn> _frame;
            private readonly Func<TIn, TOut> _pixelProjection;

            public WrappingFrame(IFrame<TIn> frame, Func<TIn, TOut> pixelProjection) {
                _frame = frame;
                _pixelProjection = pixelProjection;
            }

            public IEnumerable<IEnumerable<TOut>> Data {
                get { return _frame.Data.Select(r => r.Select(_pixelProjection)); }
            }

            public int Height {
                get { return _frame.Height; }
            }

            public int Width {
                get { return _frame.Width; }
            }
        }

        #endregion
    }
}
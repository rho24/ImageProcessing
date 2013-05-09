using System;

namespace ImageProcessing.Core
{
    public class DelayingFilter : PixelFilter<ArgbPixel, ArgbPixel>
    {
        private readonly int _delay;

        protected override Func<ArgbPixel, ArgbPixel> PixelProjection {
            get {
                return p => {
                    int y;
                    for (int i = 0; i < 10*_delay; i++) y = i;
                    return p;
                };
            }
        }

        public DelayingFilter(int delay) {
            _delay = delay;
        }
    }
}
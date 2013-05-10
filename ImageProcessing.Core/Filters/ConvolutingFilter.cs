using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageProcessing.Core.Filters
{
    public class ConvolutingFilter : IFilter<int, int>
    {
        private readonly int _halfHeight;
        private readonly int _halfWidth;
        private readonly int _height;
        private readonly int _width;

        public IIndexedSequence<IIndexedSequence<int>> Template { get; private set; }

        public ConvolutingFilter(IEnumerable<IEnumerable<int>> template) {
            if (template == null) throw new ArgumentNullException("template");
            Template = template.ToIndexedSequence();
            _width = template.Count();
            _height = (template.FirstOrDefault() ?? Enumerable.Empty<int>()).Count();

            if (_width%2 == 0)
                throw new ArgumentException("Template's width must be odd", "template");

            if (_height%2 == 0)
                throw new ArgumentException("Template's height must be odd", "template");

            _halfWidth = _width/2;
            _halfHeight = _height/2;
        }

        public IFrame<int> Execute(IFrame<int> frame) {
            IFrame<int>[] resultFrame = {null};

            resultFrame[0] = new Frame<int>(frame.Height, frame.Width, ConvolutePixels(frame.Data, () => resultFrame[0]));

            return resultFrame[0];
        }

        private IIndexedSequence<IIndexedSequence<IPixel<int>>> ConvolutePixels(IIndexedSequence<IIndexedSequence<IPixel<int>>> data, Func<IFrame<int>> resultFrame) {
            return data.Select(r => r.Select(p => Convolute(p, resultFrame)).ToIndexedSequence()).ToIndexedSequence();
        }

        private IPixel<int> Convolute(IPixel<int> pixel, Func<IFrame<int>> resultFrame) {
            int value = 0;
            
            if (!(pixel.Y <= _halfHeight || pixel.Y >= (pixel.Frame.Height - _halfHeight) || pixel.X <= _halfWidth || pixel.X >= (pixel.Frame.Width - _halfWidth))) {
                for (var y = 0; y < _height; y++)
                    for (int x = 0; x < _width; x++) value += pixel.Frame.Data[pixel.Y + y - _halfHeight][pixel.X + x - _halfWidth].Value*Template[y][x];
            }

            return Pixel.Create(value, pixel.X, pixel.Y, resultFrame);
        }
    }
}
namespace ImageProcessing.Core
{
    public abstract class TemplateFilter3<TIn, TOut> : IFilter<TIn, TOut> where TIn : IPixel where TOut : IPixel
    {
        public IImage<TOut> Process(IImage<TIn> image) {
            var pixels = new TOut[image.Height][];
            for (int y = 0; y < image.Height; y++) {
                pixels[y] = new TOut[image.Width];
                for (int x = 0; x < image.Width; x++) {
                    if (y < 1 || y >= image.Height - 1 || x < 1 || x >= image.Width - 1) {
                        pixels[y][x] = DefaultPixel();
                        continue;
                    }

                    pixels[y][x] = Map(
                        new Template<TIn> {
                            TopLeft = image.Pixels[y - 1][x - 1],
                            TopCenter = image.Pixels[y - 1][x],
                            TopRight = image.Pixels[y - 1][x + 1],
                            CenterLeft = image.Pixels[y][x - 1],
                            CenterCenter = image.Pixels[y][x],
                            CenterRight = image.Pixels[y][x + 1],
                            BottomLeft = image.Pixels[y + 1][x - 1],
                            BottomCenter = image.Pixels[y + 1][x],
                            BottomRight = image.Pixels[y + 1][x + 1]
                        });
                }
            }


            return Image.FromPixels(image.Width, image.Height, pixels);
        }

        protected abstract TOut DefaultPixel();
        
        public abstract TOut Map(Template<TIn> pixels);

        #region Nested type: Template

        public class Template<T>
        {
            public T TopLeft { get; set; }
            public T TopCenter { get; set; }
            public T TopRight { get; set; }
            public T CenterLeft { get; set; }
            public T CenterCenter { get; set; }
            public T CenterRight { get; set; }
            public T BottomLeft { get; set; }
            public T BottomCenter { get; set; }
            public T BottomRight { get; set; }
        }

        #endregion
    }
}
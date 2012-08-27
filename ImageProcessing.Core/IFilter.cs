namespace ImageProcessing.Core
{
    public interface IFilter<in TIn, out TOut> where TIn : IPixel where TOut : IPixel
    {
        IImage<TOut> Process(IImage<TIn> image);
    }
}
namespace ImageProcessing.Core
{
    public interface IFilter<in TIn, out TOut>
    {
        IFrame<TOut> Execute(IFrame<TIn> frame);
    }
}
namespace ImageProcessing.Core.Filters
{
    public class ArgbFilter : SelectFilter<int, Argb>
    {
        public ArgbFilter() : base(p => new Argb(0, (byte) p, (byte) p, (byte) p)) {}
    }
}
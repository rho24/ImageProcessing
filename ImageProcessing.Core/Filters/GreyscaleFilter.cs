namespace ImageProcessing.Core.Filters
{
    public class GreyscaleFilter : SelectFilter<Argb, int>
    {
        public GreyscaleFilter() : base(p => (p.R + p.G + p.B)/3) {}
    }
}
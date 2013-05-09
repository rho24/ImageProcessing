namespace ImageProcessing.Core
{
    public static class ImgProc
    {
        public static IFrame<ArgbPixel> ImageFromFile(string fileName) {
            return new StaticFileImage(fileName);
        }
    }
}
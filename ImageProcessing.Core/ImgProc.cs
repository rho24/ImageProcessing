namespace ImageProcessing.Core
{
    public static class ImgProc
    {
        public static IFrame<Argb> ImageFromFile(string fileName) {
            return new StaticFileImage(fileName);
        }
    }
}
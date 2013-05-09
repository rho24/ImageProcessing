using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ImageProcessing.Core;

namespace Tester
{
    internal class Program
    {
        private static void Main(string[] args) {
            using (var b = new Bitmap(@"Penguins.jpg")) b.Save(@"Temp2.bmp", ImageFormat.Bmp);

            var i = ImgProc.ImageFromFile(@"Penguins.jpg");

            using (var fs = File.OpenWrite(@"Temp.bmp")) CopyStream(i.ToBitmapStream(), fs);
        }

        public static void CopyStream(Stream input, Stream output) {
            byte[] buffer = new byte[8*1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
                output.Write(buffer, 0, len);
        }
    }
}
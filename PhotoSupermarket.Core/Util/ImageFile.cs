using System;
using System.IO;

namespace PhotoSupermarket.Core.Util
{
    public class ImageFile
    {
        public static BmpImage LoadBmpImage(string filePath)
        {
            BmpImageParser parser = new BmpImageParser(File.ReadAllBytes(filePath));
            parser.Parse();
            return parser.Image;
        }

        public static void SaveBmpImage(BmpImage image, string filePath)
        {
            BmpImageGenerator generator = new BmpImageGenerator(image);
            generator.Generate();
            var fs = File.OpenWrite(filePath);
            fs.Write(generator.ImageBytes, 0, generator.ImageBytes.Length);
            fs.Close();
        }
    }
}

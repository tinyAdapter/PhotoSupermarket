using System;
using System.IO;

namespace PhotoSupermarket.Core.Util
{
    public class ImageFile
    {
        public static BmpImage LoadBmpImage(string filePath)
        {
            int currentIndex = 0;

            BmpImage image = new BmpImage();
            byte[] allImageBytes = File.ReadAllBytes(filePath);
            if (Bytes.ReadUShort(allImageBytes, ref currentIndex) != 0x4D42)
                throw new NotValidImageFileException();

            return image;
        }

        public static bool SaveBmpImage(BmpImage image, string filePath)
        {
            throw new NotImplementedException();
        }
    }
}

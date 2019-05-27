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

        public static bool SaveBmpImage(BmpImage image, string filePath)
        {
            throw new NotImplementedException();
        }
    }

    public class BmpImageParser
    {
        public BmpImageParser(byte[] imageBytes)
        {
            this.imageBytes = imageBytes;
        }

        private byte[] imageBytes;
        private int currentIndex = 0;
        public BmpImage Image { get; } = new BmpImage();

        public void Parse()
        {
            ParseFileHeader();
            ParseInfoHeader();
            //ParsePalette();
            //ParseBitmapData();
        }
        
        private void ParseFileHeader()
        {
            Image.FileHeader = new BitmapFileHeader();
            if ((Image.FileHeader.Type = Bytes.ReadUShort(imageBytes, ref currentIndex)) != 0x4D42)
            {
                throw new NotValidImageFileException();
            }
            Image.FileHeader.Size = Bytes.ReadUInt(imageBytes, ref currentIndex);
            if ((Image.FileHeader.Reserved1 = Bytes.ReadUShort(imageBytes, ref currentIndex)) != 0)
            {
                throw new NotValidImageFileException();
            }
            if ((Image.FileHeader.Reserved2 = Bytes.ReadUShort(imageBytes, ref currentIndex)) != 0)
            {
                throw new NotValidImageFileException();
            }
            Image.FileHeader.OffBits = Bytes.ReadUInt(imageBytes, ref currentIndex);
        }

        private void ParseInfoHeader()
        {//eli cao
            Image.InfoHeader = new BitmapInfoHeader();
            if((Image.InfoHeader.Size = Bytes.ReadUInt(imageBytes,ref currentIndex)) != 40)
            {
                throw new NotValidImageFileException("biSize is not 40");
            }
            Image.InfoHeader.Width = Bytes.ReadInt(imageBytes, ref currentIndex);
            Image.InfoHeader.Height = Bytes.ReadInt(imageBytes, ref currentIndex);
            if ((Image.InfoHeader.Planes = Bytes.ReadUShort(imageBytes, ref currentIndex)) != 1)
            {
                throw new NotValidImageFileException("biPlanes is not 1");
            }
            Image.InfoHeader.BitCount = Bytes.ReadUShort(imageBytes, ref currentIndex);
            Image.InfoHeader.Compression = Bytes.ReadUInt(imageBytes, ref currentIndex);
            if((Image.InfoHeader.SizeImage = Bytes.ReadUInt(imageBytes, ref currentIndex)) != Image.FileHeader.Size - Image.FileHeader.OffBits)
            {
                throw new NotValidImageFileException();
            }
            Image.InfoHeader.XPelsPerMeter = Bytes.ReadInt(imageBytes, ref currentIndex);
            Image.InfoHeader.YPelsPerMeter = Bytes.ReadInt(imageBytes, ref currentIndex);
            Image.InfoHeader.ClrUsed = Bytes.ReadUInt(imageBytes, ref currentIndex);
            Image.InfoHeader.ClrImportanet = Bytes.ReadUInt(imageBytes, ref currentIndex);
        }

        private void ParsePalette()
        {
            throw new NotImplementedException();
        }

        private void ParseBitmapData()
        {
            throw new NotImplementedException();
        }
    }
}

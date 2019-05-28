using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoSupermarket.Core.Util
{
    public class BmpImageGenerator
    {
        public byte[] ImageBytes { get; private set; }
        private BmpImage image;
        private int currentIndex = 0;

        public BmpImageGenerator(BmpImage image)
        {
            this.image = image;
        }

        public void Generate()
        {
            ImageBytes = new byte[image.FileHeader.Size];
            GenerateFileHeader();
            GenerateInfoHeader();
            GeneratePalette();
            GenerateData();
        }

        private void GenerateFileHeader()
        {
            Bytes.ToBytes(ImageBytes, image.FileHeader.Type, ref currentIndex);
            Bytes.ToBytes(ImageBytes, image.FileHeader.Size, ref currentIndex);
            Bytes.ToBytes(ImageBytes, image.FileHeader.Reserved1, ref currentIndex);
            Bytes.ToBytes(ImageBytes, image.FileHeader.Reserved2, ref currentIndex);
            Bytes.ToBytes(ImageBytes, image.FileHeader.OffBits, ref currentIndex);
        }

        private void GenerateInfoHeader()
        {
            Bytes.ToBytes(ImageBytes, image.InfoHeader.Size, ref currentIndex);
            Bytes.ToBytes(ImageBytes, image.InfoHeader.Width, ref currentIndex);
            Bytes.ToBytes(ImageBytes, image.InfoHeader.Height, ref currentIndex);
            Bytes.ToBytes(ImageBytes, image.InfoHeader.Planes, ref currentIndex);
            Bytes.ToBytes(ImageBytes, image.InfoHeader.BitCount, ref currentIndex);
            Bytes.ToBytes(ImageBytes, image.InfoHeader.Compression, ref currentIndex);
            Bytes.ToBytes(ImageBytes, image.InfoHeader.SizeImage, ref currentIndex);
            Bytes.ToBytes(ImageBytes, image.InfoHeader.XPelsPerMeter, ref currentIndex);
            Bytes.ToBytes(ImageBytes, image.InfoHeader.YPelsPerMeter, ref currentIndex);
            Bytes.ToBytes(ImageBytes, image.InfoHeader.ClrUsed, ref currentIndex);
            Bytes.ToBytes(ImageBytes, image.InfoHeader.ClrImportanet, ref currentIndex);
        }

        private void GeneratePalette()
        {
            if (!image.HasPalette()) return;

            int totalPaletteEntries = image.GetTotalPaletteEntries();
            for (int i = 0; i < totalPaletteEntries; i++)
            {
                Bytes.ToBytes(ImageBytes, image.Palette[i].Blue, ref currentIndex);
                Bytes.ToBytes(ImageBytes, image.Palette[i].Green, ref currentIndex);
                Bytes.ToBytes(ImageBytes, image.Palette[i].Red, ref currentIndex);
                Bytes.ToBytes(ImageBytes, image.Palette[i].Flags, ref currentIndex);
            }
        }

        private void GenerateData()
        {
            for (int i = 0; i < image.InfoHeader.SizeImage; i++)
            {
                Bytes.ToBytes(ImageBytes, image.Data.Data[i], ref currentIndex);
            }
        }
    }
}

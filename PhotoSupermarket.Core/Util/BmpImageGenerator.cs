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
            uint offBits = 14 + 40 + (uint)(image.HasPalette() ? image.Palette.Length * 4 : 0);
            Bytes.ToBytes(ImageBytes, (ushort)0x4D42, ref currentIndex);
            Bytes.ToBytes(ImageBytes, (uint)(offBits + image.Data.GetRealSize()), ref currentIndex);
            Bytes.ToBytes(ImageBytes, (ushort)0, ref currentIndex);
            Bytes.ToBytes(ImageBytes, (ushort)0, ref currentIndex);
            Bytes.ToBytes(ImageBytes, offBits, ref currentIndex);
        }

        private void GenerateInfoHeader()
        {
            Bytes.ToBytes(ImageBytes, (uint)40, ref currentIndex);
            Bytes.ToBytes(ImageBytes, image.Data.Width, ref currentIndex);
            Bytes.ToBytes(ImageBytes, image.Data.Height, ref currentIndex);
            Bytes.ToBytes(ImageBytes, (ushort)1, ref currentIndex);
            Bytes.ToBytes(ImageBytes, (ushort)image.Data.ColorMode, ref currentIndex);
            Bytes.ToBytes(ImageBytes, (uint)0, ref currentIndex);
            Bytes.ToBytes(ImageBytes, (uint)image.Data.GetRealSize(), ref currentIndex);
            Bytes.ToBytes(ImageBytes, 0, ref currentIndex);
            Bytes.ToBytes(ImageBytes, 0, ref currentIndex);
            Bytes.ToBytes(ImageBytes, (uint)0, ref currentIndex);
            Bytes.ToBytes(ImageBytes, (uint)0, ref currentIndex);
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

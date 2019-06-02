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
        private int offBits;

        public BmpImageGenerator(BmpImage image)
        {
            this.image = image;

            int paletteBitCount = 0;
            if (image.Data.ColorMode == BitmapColorMode.MonoChrome) paletteBitCount = 2 * 4;
            else if (image.Data.ColorMode == BitmapColorMode.TwoFiftySixColors) paletteBitCount = 256 * 4;
            offBits = 14 + 40 + paletteBitCount;
        }

        public void Generate()
        {
            ImageBytes = new byte[offBits + image.Data.GetRealSize()];
            GenerateFileHeader();
            GenerateInfoHeader();
            GeneratePalette();
            GenerateData();
        }

        private void GenerateFileHeader()
        {
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
            if (image.Data.ColorMode == BitmapColorMode.TrueColor
                || image.Data.ColorMode == BitmapColorMode.RGBA) return;

            int totalPaletteEntries = (int)Math.Pow(2, (int)image.Data.ColorMode);
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
            int dataSize = image.Data.GetRealSize();
            for (int i = 0; i < dataSize; i++)
            {
                Bytes.ToBytes(ImageBytes, image.Data.Data[i], ref currentIndex);
            }
        }
    }
}

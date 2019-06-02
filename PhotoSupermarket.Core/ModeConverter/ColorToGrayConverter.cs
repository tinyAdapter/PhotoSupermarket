using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoSupermarket.Core.ModeConverter
{
    public abstract class ColorToGrayConverter : BaseConverter
    {
        public ColorToGrayConverter(BmpImage image) : base(image)
        {
        }

        protected override void DoConvert()
        {
            Image.Data.ColorMode = BitmapColorMode.TwoFiftySixColors;
            GeneratePalette();
            ConvertBitmapData();
        }

        private void GeneratePalette()
        {
            Image.Palette = new BitmapPaletteEntry[256];
            for (int i = 0; i < Image.Palette.Length; i++)
            {
                Image.Palette[i] = new BitmapPaletteEntry((byte)i, (byte)i, (byte)i, 0);
            }
        }
        
        private void ConvertBitmapData()
        {
            Image.Data.Data = new byte[Image.Data.GetRealSize()];

            for (int i = 0; i < Image.Data.Width; i++)
            {
                for (int k = 0; k < Image.Data.Height; k++)
                {
                    Image.Data.Set8BitDataAt(i, k, GetGrayValueAt(i, k));
                }
            }
        }

        protected abstract byte GetGrayValueAt(int x, int y);
    }
}

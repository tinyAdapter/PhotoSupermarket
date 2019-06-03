using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoSupermarket.Core.ModeConverter
{
    abstract class GrayToBinaryConverter : BaseConverter
    {
        public GrayToBinaryConverter(BmpImage data) : base(data)
        {
        }

        protected override void DoConvert()
        {
            Image.Data.ColorMode = BitmapColorMode.MonoChrome;
            GeneratePalette();
            ConvertBitmapData();
        }

        private void GeneratePalette()
        {
            Image.Palette = new BitmapPaletteEntry[2];
            Image.Palette[0] = new BitmapPaletteEntry((byte)0, (byte)0, (byte)0, 0);
            Image.Palette[1] = new BitmapPaletteEntry((byte)255, (byte)255, (byte)255, 0);
        }

        protected abstract void ConvertBitmapData();
    }
}

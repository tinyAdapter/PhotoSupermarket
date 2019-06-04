using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoSupermarket.Core.Util
{
    class BmpImageAllocator
    {
        public static BmpImage NewGrayScaleImage(int width, int height, byte[] data = null )
        {
            BmpImage result = new BmpImage()
            {
                Palette = new BitmapPaletteEntry[256]
            };
            for (int i = 0; i < 256; i++)
            {
                result.Palette[i] = new BitmapPaletteEntry((byte)i, (byte)i, (byte)i, 0);
            }
            result.Data = new BitmapData
            {
                ColorMode = BitmapColorMode.TwoFiftySixColors,
                Width = width,
                Height = height
            };

            if (data == null)
                result.Data.Data = new byte[result.Data.GetRealSize()];
            else
                result.Data.Data = data;

            return result;
        }

    }
}

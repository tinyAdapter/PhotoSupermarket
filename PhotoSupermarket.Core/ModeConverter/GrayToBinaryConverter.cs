using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoSupermarket.Core.ModeConverter
{
    public abstract class GrayToBinaryConverter : BaseConverter
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

    public class SingleThresholdConverter : GrayToBinaryConverter
    {
        private int Threshold { get; set; }

        public SingleThresholdConverter(BmpImage data, int threshold) : base(data)
        {
            if (threshold < 0 || threshold > 255)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (threshold == 0)
            {
                Threshold = 128;
            }
            else
            {
                Threshold = threshold;
            }
        }

        protected override void ConvertBitmapData()
        {
            for (int i = 0; i < Image.Data.Width; i++)
            {
                for (int j = 0; j < Image.Data.Height; j++)
                {
                    byte temp = oldData.Get8BitDataAt(i, j);
                    if (temp >= Threshold)
                    {
                        Image.Data.Set1BitDataAt(i, j, true);
                    }
                    else
                    {
                        Image.Data.Set1BitDataAt(i, j, false);
                    }
                }
            }
        }
    }
}

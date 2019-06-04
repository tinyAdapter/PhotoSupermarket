using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoSupermarket.Core.Compression
{
    public class UniformQuantizing
    {
        public BmpImage Image { get; }
        public byte[,] UniformQuantizingData { get; private set; }
        public int TotalIntervals { get; }
        public double QuantizingInterval { get; }

        public UniformQuantizing(BmpImage image, double CompressRatio)
        {
            double intendingBit = 8.0 / CompressRatio;
            double intendingTotalIntervals = Math.Pow(2, intendingBit);
            QuantizingInterval = 256.0 / intendingTotalIntervals;
            TotalIntervals = (int)Math.Ceiling(intendingTotalIntervals);
            Image = image;
            UniformQuantizingCompress();
        }

        // no CR means IGS Uniform Quantizing (CR = 2)
        public UniformQuantizing(BmpImage image)
        {
            QuantizingInterval = 16.0;
            TotalIntervals = 16;
            Image = image;
            IGSUniformQuantizaing();
        }

        // the result bits of this method range from 1-8.
        public void UniformQuantizingCompress()
        {
            if (Image.Data.ColorMode != BitmapColorMode.TwoFiftySixColors)
            {
                throw new NotThisColorModeException();
            }

            UniformQuantizingData = new byte[Image.Data.Width, Image.Data.Height];

            for (int i = 0; i < Image.Data.Width; i++)
            {
                for (int j = 0; j < Image.Data.Height; j++)
                {
                    byte originalColor = Image.Data.Get8BitDataAt(i, j);
                    byte newColor = (byte)(originalColor / QuantizingInterval);
                    UniformQuantizingData[i, j] = newColor;
                }
            }
        }

        public void IGSUniformQuantizaing()
        {
            if (Image.Data.ColorMode != BitmapColorMode.TwoFiftySixColors)
            {
                throw new NotThisColorModeException();
            }

            UniformQuantizingData = new byte[Image.Data.Width, Image.Data.Height];
            byte lastSum = 0;

            for (int i = 0; i < Image.Data.Width; i++)
            {
                for (int j = 0; j < Image.Data.Height; j++)
                {
                    byte originalColor = Image.Data.Get8BitDataAt(i, j);
                    byte newSum = (byte)Math.Min(((lastSum % 16) + originalColor), 255);
                    byte newColor = (byte)(newSum / 16);
                    UniformQuantizingData[i, j] = newColor;
                    lastSum = newSum;
                }
            }
        }

        // this method only returns a bitmap with 1/8 bit(s).
        // this method works for both Uniform Quantizing and IGS.
        public BmpImage InverseUniformQuantizaing()
        {
            if (UniformQuantizingData == null) throw new UnauthorizedAccessException();

            if (TotalIntervals <= 2) return Inverse1BitBitmap();
            else return Inverse8BitsBitmap();
        }

        private BmpImage Inverse8BitsBitmap()
        {
            BmpImage result = NewGrayScaleImage();

            for (int i = 0; i < Image.Data.Width; i++)
            {
                for (int j = 0; j < Image.Data.Height; j++)
                {
                    result.Data.Set8BitDataAt(i, j, 
                        (byte)((UniformQuantizingData[i, j] + 0.5) * QuantizingInterval));
                }
            }
            return result;
        }
        
        private BmpImage Inverse1BitBitmap()
        {
            BmpImage result = new BmpImage()
            {
                Palette = new BitmapPaletteEntry[2]
            };
            result.Palette[0] = new BitmapPaletteEntry((byte)0, (byte)0, (byte)0, 0);
            result.Palette[1] = new BitmapPaletteEntry((byte)255, (byte)255, (byte)255, 0);
            result.Data = new BitmapData
            {
                ColorMode = BitmapColorMode.MonoChrome,
                Width = Image.Data.Width,
                Height = Image.Data.Height
            };
            result.Data.Data = new byte[result.Data.GetRealSize()];
            for (int i = 0; i < Image.Data.Width; i++)
            {
                for (int j = 0; j < Image.Data.Height; j++)
                {
                    if (UniformQuantizingData[i, j] == 0)
                    {
                        result.Data.Set1BitDataAt(i, j, false);
                    }
                    else
                    {
                        result.Data.Set1BitDataAt(i, j, true);
                    }
                }
            }
            return result;
        }

        private BmpImage NewGrayScaleImage()
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
                Width = Image.Data.Width,
                Height = Image.Data.Height
            };
            result.Data.Data = new byte[result.Data.GetRealSize()];

            return result;
        }
    }
}

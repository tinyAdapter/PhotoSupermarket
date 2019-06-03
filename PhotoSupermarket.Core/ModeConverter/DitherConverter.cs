using PhotoSupermarket.Core.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoSupermarket.Core.ModeConverter
{
    public class DitherConverter : GrayToBinaryConverter
    {
        public int Power { get; set; }

        public DitherConverter(BmpImage image, int power) : base(image)
        {
            Power = power;
        }

        protected override void ConvertBitmapData()
        {
            int scale = (int)Math.Pow(2, Power);
            Image.Data.Width = Image.Data.Width * scale;
            Image.Data.Height = Image.Data.Height * scale;
            Image.Data.Data = new byte[Image.Data.GetRealSize()];
            
            int[,] ditherMatrix = Dither.GenerateDitherMatrix(Power);

            for (int i = 0; i < oldData.Width; i++)
            {
                for (int k = 0; k < oldData.Height; k++)
                {
                    byte oldValue = oldData.Get8BitDataAt(i, k);
                    int threshold = (int)((double)oldValue / 255 * scale);
                    for (int m = 0; m < scale; m++)
                    {
                        for (int n = 0; n < scale; n++)
                        {
                            if (threshold >= ditherMatrix[m, n])
                            {
                                Image.Data.Set1BitDataAt(i * scale + m, k * scale + n, true);
                            }
                            else
                            {
                                Image.Data.Set1BitDataAt(i * scale + m, k * scale + n, false);
                            }
                        }
                    }
                }
            }
        }
    }
}

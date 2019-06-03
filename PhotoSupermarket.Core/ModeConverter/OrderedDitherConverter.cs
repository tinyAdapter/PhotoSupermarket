using PhotoSupermarket.Core.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoSupermarket.Core.ModeConverter
{

    class OrderedDitherConverter : GrayToBinaryConverter
    {
        int[,] ditherMatrix;
        public OrderedDitherConverter(BmpImage data) : base(data)
        {
            ditherMatrix = Dither.GenerateDitherMatrix(1);
        }
        public OrderedDitherConverter(BmpImage data, int DitherMatrixPower) : base(data)
        {
            ditherMatrix = Dither.GenerateDitherMatrix(DitherMatrixPower);
        }

        protected override void ConvertBitmapData()
        {
            Image.Data.Data = new byte[Image.Data.GetRealSize()];

            int[,] remapedOldData = new int[oldData.Width, oldData.Height];
            for (int x = 0; x < oldData.Width; x++)
            {
                for (int y = 0; y < oldData.Height; y++)
                {
                    remapedOldData[x, y] = (int)(oldData.Get8BitDataAt(x, y) / (Math.Pow(ditherMatrix.Length, 2) - 1));
                }
            }


            int i, j;
            for (int x = 0; x < oldData.Width; x++)
            {
                i = x % ditherMatrix.Length;
                for (int y = 0; y < oldData.Height; y++)
                {
                    j = y % ditherMatrix.Length;
                    if (remapedOldData[x, y] > ditherMatrix[i, j])
                        Image.Data.Set1BitDataAt(x, y, true);
                    else
                        Image.Data.Set1BitDataAt(x, y, false);
                }
            }
        }
    }
}

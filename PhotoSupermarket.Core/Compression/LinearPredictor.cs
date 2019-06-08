using PhotoSupermarket.Core.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoSupermarket.Core.Compression
{
    struct Position
    {
        public int x;
        public int y;
    }

    public class LinearPredictor
    {
        private int k;
        private float[] cocs;
        private BmpImage image;
        private string savedFilePath;
        private string fileName;

        /** user can ignore the collections of correalations, it will be set first order
         */
        public LinearPredictor(BmpImage image, string savedFilePath, string fileName, float[] cocs = null)
        {
            this.image = image;
            if (cocs != null)
                this.cocs = cocs;
            else
                this.cocs = new float[] { 1 };

            this.k = this.cocs.Length;
            this.savedFilePath = savedFilePath;
            this.fileName = fileName;
        }

        public BmpImage predicate()
        {
            var oldData = this.image.Data;

            byte[] newData = new byte[oldData.GetRealSize()];

            int width = oldData.Width;
            int height = oldData.Height;

            Position pos = new Position();

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    //TODO: to calculate every predicated pixel value
                    pos.x = i;
                    pos.y = j;
                    newData[i * width + j] = 
                        (byte)((oldData.Data[i * width + j] - PredictSinglePixel(pos)) / 2 + 128);
                }
            }

            BmpImage pImage = BmpImageAllocator.NewGrayScaleImage(width, height, newData);

            ImageFile.SaveBmpImage(pImage, savedFilePath+ fileName + ".bmp" );


            return pImage;
        }

        private byte PredictSinglePixel(Position pos)
        {
            float predictResult = 0;

            int width = this.image.Data.Width;

            if (pos.x * width + pos.y < k)
                return this.image.Data.Data[pos.x * width + pos.y];

            for (int i = 1; i <= k; i++)
            {
                predictResult += cocs[i - 1] * (int)this.image.Data.Data[pos.x * width + pos.y - k];
            }
            return (byte)Math.Round(predictResult);
        }
    }
}

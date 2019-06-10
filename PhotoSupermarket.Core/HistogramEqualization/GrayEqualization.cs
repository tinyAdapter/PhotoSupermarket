using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoSupermarket.Core.HistogramEqualization
{
    public class GrayEqualization : BaseHistogram
    {
        public GrayEqualization(BmpImage image) : base(image)
        {
        }

        public int[] hist_acc;
        public int[] hist;
        public int[] S;
        public int level = 256;

        protected override void DoEqualization()
        {
            if (Image.Data.ColorMode != BitmapColorMode.TwoFiftySixColors)
                throw new NotThisColorModeException();
            GetHistogram();
            Transformation();
        }

        public void GetHistogram()
        {
            hist = new int[level];
            hist_acc = new int[level];

            for (int i = 0; i < oldData.Width; i++)
            {
                for (int k = 0; k < oldData.Height; k++)
                {

                    hist[oldData.Get8BitDataAt(i, k)]++;
                }
            }

            for (int i = 0; i < level; i++)
            {
                hist_acc[i] = hist_acc[i];
            }

            for (int i = 1; i < level; i++)
            {
                hist_acc[i] += hist_acc[i-1];
            }
        }


        public void Transformation()
        {

            S = new int[level];
            int size = oldData.Width * oldData.Height;
            for (int i = 0; i < level; i++)
            {
                S[i] = (int)Math.Round((level - 1) * (hist_acc[i] * 1.0) / size);
            }

            for (int i = 0; i < Image.Data.Width; i++)
            {
                for (int k = 0; k < Image.Data.Height; k++)
                {
                    Image.Data.Set8BitDataAt(i, k, (byte)S[oldData.Get8BitDataAt(i, k)]);
                }
            }
        }
    }
}

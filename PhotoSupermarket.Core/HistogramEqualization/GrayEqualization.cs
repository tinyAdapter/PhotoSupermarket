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

        public int[] histogram;
        public int[] S;
        public int Level = 256;

        protected override void DoEqualization()
        {
            if (Image.Data.ColorMode != BitmapColorMode.TwoFiftySixColors)
                throw new NotThisColorModeException();
            GetHistogram();
            Transformation();
        }

        public void GetHistogram()
        {
            histogram = new int[Level];

            for (int i = 0; i < oldData.Width; i++)
            {
                for (int k = 0; k < oldData.Height; k++)
                {
                    histogram[oldData.Get8BitDataAt(i, k)]++;
                }
            }

            for (int i = 1; i < Level; i++)
            {
                histogram[i] += histogram[i-1];
            }
        }


        public void Transformation()
        {

            S = new int[Level];
            int size = Image.Data.GetRealSize();
            for (int i = 0; i < Level; i++)
            {
                S[i] = (int)Math.Round((Level - 1) * (histogram[i] * 1.0) / size);
            }

            for (int i = 0; i < Image.Data.Width; i++)
            {
                for (int k = 0; k < Image.Data.Height; k++)
                {
                    Image.Data.Set8BitDataAt(i, k, S[oldData.Get8BitDataAt(i, k)]);
                }
            }
        }
    }
}

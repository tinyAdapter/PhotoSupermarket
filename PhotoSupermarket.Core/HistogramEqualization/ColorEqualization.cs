using System;
using System.Collections.Generic;
using System.Text;
using PhotoSupermarket.Core.Util;
using PhotoSupermarket.Core.Model;

namespace PhotoSupermarket.Core.HistogramEqualization
{
    public class ColorEqualization : BaseHistogram
    {
        public ColorEqualization(BmpImage image) : base(image)
        {
        }

        public int[] histV;
        public float[] SV;
        public int level = 256; 


        protected override void DoEqualization()
        {
            if (Image.Data.ColorMode != BitmapColorMode.TrueColor)
                throw new NotThisColorModeException();
            GetHistogram();
            Transformation();
        }

        public void GetHistogram()
        {
            histV = new int[level];

            for (int i = 0; i < oldData.Width; i++)
            {
                for (int k = 0; k < oldData.Height; k++)
                {
                    histV[(int)(HSVAndRGB.RGB2HSV(oldData.GetRGBDataAt(i, k)).V *255)]++;
                }
            }

            for (int i = 1; i < level; i++)
            {
                histV[i] += histV[i - 1];
            }
        }


        public void Transformation()
        {

            SV = new float[level];
            int size = oldData.Width * oldData.Height;
            for (int i = 0; i < level; i++)
            {
                SV[i] = (histV[i] * 1.0F) / size;
            }

            for (int i = 0; i < Image.Data.Width; i++)
            {
                for (int k = 0; k < Image.Data.Height; k++)
                {
                    HSV oldHsv= HSVAndRGB.RGB2HSV(oldData.GetRGBDataAt(i,k));
                    oldHsv.V = SV[(int)(oldHsv.V*255)];
                    Image.Data.SetRGBDataAt(i, k, HSVAndRGB.HSV2RGB(oldHsv));
                }
            }
        }

        

    } 
}

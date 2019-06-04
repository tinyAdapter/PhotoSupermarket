using PhotoSupermarket.Core.Compression;
using PhotoSupermarket.Core.Util;
using System;
using System.Collections.Generic;
using System.Text;

using Xunit;

namespace PhotoSupermarket.Core.UnitTest
{
    public class AllocatorUnitTest
    {
        BmpImage image = Util.ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\lenna_gray.bmp");

        [Fact]
        public void testPredict()
        {

            float[] cocs = { 0.5f, 0.5f };
            string savedFilePath = "..\\..\\..\\TestImages\\";
            string fileName = "lenna_gray_predicated";

            LinearPredictor lp = new LinearPredictor(image, savedFilePath, fileName, cocs);
            BmpImage pImage = lp.predicate();


            Assert.Equal(pImage.Data.Width, image.Data.Width);
            Assert.Equal(pImage.Data.Height, image.Data.Height);
            Assert.Equal(BitmapColorMode.TwoFiftySixColors, pImage.Data.ColorMode);

        }

    }
}

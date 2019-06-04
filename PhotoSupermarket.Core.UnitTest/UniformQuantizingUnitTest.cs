using PhotoSupermarket.Core.Compression;
using PhotoSupermarket.Core.Util;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace PhotoSupermarket.Core.UnitTest
{
    public class UniformQuantizingUnitTest
    {
        [Fact]
        public void TestUniformQuantizingCompress2()
        {
            var image = ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\lenna_gray.bmp");
            UniformQuantizing uq = new UniformQuantizing(image, 2.0);
            BmpImage uqImage = uq.InverseUniformQuantizaing();
            ImageFile.SaveBmpImage(uqImage, "..\\..\\..\\TestImages\\lenna_uq2_modified.bmp");
            Assert.Equal(16, uq.TotalIntervals);
        }

        [Fact]
        public void TestUniformQuantizingCompress4()
        {
            var image = ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\lenna_gray.bmp");
            UniformQuantizing uq = new UniformQuantizing(image, 4.0);
            BmpImage uqImage = uq.InverseUniformQuantizaing();
            ImageFile.SaveBmpImage(uqImage, "..\\..\\..\\TestImages\\lenna_uq4_modified.bmp");
            Assert.Equal(4, uq.TotalIntervals);
        }

        [Fact]
        public void TestUniformQuantizingCompress8()
        {
            var image = ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\lenna_gray.bmp");
            UniformQuantizing uq = new UniformQuantizing(image, 8.0);
            BmpImage uqImage = uq.InverseUniformQuantizaing();
            ImageFile.SaveBmpImage(uqImage, "..\\..\\..\\TestImages\\lenna_uq8_modified.bmp");
            Assert.Equal(2, uq.TotalIntervals);
        }

        [Fact]
        public void TestIGSUniformQuantizing()
        {
            var image = ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\lenna_gray.bmp");
            UniformQuantizing igs = new UniformQuantizing(image);
            BmpImage igsImage = igs.InverseUniformQuantizaing();
            ImageFile.SaveBmpImage(igsImage, "..\\..\\..\\TestImages\\lenna_igs_modified.bmp");
            Assert.Equal(16, igs.TotalIntervals);
        }
    }
}

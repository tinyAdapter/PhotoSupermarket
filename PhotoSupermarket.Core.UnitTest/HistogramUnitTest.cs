using System;
using PhotoSupermarket.Core.HistogramEqualization;
using System.Collections.Generic;
using Xunit;
using System.Text;

namespace PhotoSupermarket.Core.UnitTest
{
    public class HistogramUnitTest
    {
        [Fact]
        public void TestGrayEqualization1()
        {
            var image = Util.ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\直方图均衡测试图像gray1.bmp");
            image = new GrayEqualization(image).Equalization().Image;
            Util.ImageFile.SaveBmpImage(image,"..\\..\\..\\TestImages\\直方图均衡测试图像gray1_test.bmp");
        }


        [Fact]
        public void TestGrayEqualization2()
        {
            var image = Util.ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\直方图均衡测试图像gray2.bmp");
            image = new GrayEqualization(image).Equalization().Image;
            Util.ImageFile.SaveBmpImage(image, "..\\..\\..\\TestImages\\直方图均衡测试图像gray2_test.bmp");
        }
    }
}

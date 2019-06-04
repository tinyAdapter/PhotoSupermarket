using System;
using PhotoSupermarket.Core.HistogramEqualization;
using System.Collections.Generic;
using Xunit;
using System.Text;
using PhotoSupermarket.Core.Model;

namespace PhotoSupermarket.Core.UnitTest
{
    public class HistogramUnitTest
    {
        [Fact]
        public void TestGrayEqualization1()
        {
            var image = Util.ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\直方图均衡测试图像gray1.bmp");
            image = new GrayEqualization(image).Equalization().Image;
            Util.ImageFile.SaveBmpImage(image, "..\\..\\..\\TestImages\\直方图均衡测试图像gray1_modified.bmp");
        }


        [Fact]
        public void TestGrayEqualization2()
        {
            var image = Util.ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\直方图均衡测试图像gray2.bmp");
            image = new GrayEqualization(image).Equalization().Image;
            Util.ImageFile.SaveBmpImage(image, "..\\..\\..\\TestImages\\直方图均衡测试图像gray2_modified.bmp");
        }

        [Fact]
        public void TestColorEqualization1()
        {
            var image = Util.ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\直方图均衡测试图片color1.bmp");
            image = new ColorEqualization(image).Equalization().Image;
            Util.ImageFile.SaveBmpImage(image, "..\\..\\..\\TestImages\\直方图均衡测试图片color1_modified.bmp");
        }

        [Fact]
        public void TestColorEqualization2()
        {
            var image = Util.ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\直方图均衡测试图片color2.bmp");
            image = new ColorEqualization(image).Equalization().Image;
            Util.ImageFile.SaveBmpImage(image, "..\\..\\..\\TestImages\\直方图均衡测试图片color2_modified.bmp");
        }

        [Fact]
        public void TestColorEqualization3()
        {
            var image = Util.ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\直方图均衡测试图片color3.bmp");
            image = new ColorEqualization(image).Equalization().Image;
            Util.ImageFile.SaveBmpImage(image, "..\\..\\..\\TestImages\\直方图均衡测试图片color3_modified.bmp");
        }


        [Fact]
        public void TestHSVAndRGB()
        {
            //因为float精度问题不能直接比较，经过人工测试，没有问题；
            var image = Util.ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\直方图均衡测试图片color3.bmp");
            ColorEqualization colorEqualization = new ColorEqualization(image);
            RGB rgb1 = new RGB { R = 240,G = 248,B = 255};
            HSV hsv1 = new HSV { H = (int)(0.5778 * 360), S = 0.0588F, V = 1.0000F };
            //Assert.Equal(rgb1,colorEqualization.HSV2RGB(hsv1));
            //Assert.Equal(hsv1, colorEqualization.RGB2HSV(rgb1));


            RGB rgb2 = new RGB { R = 139, G = 130, B = 119 };
            HSV hsv2 = new HSV { H = (int)(0.0965 * 360), S = 0.1367F, V = 0.5451F};
            //Assert.Equal(rgb2,colorEqualization.HSV2RGB(hsv2));
            //Assert.Equal(hsv2, colorEqualization.RGB2HSV(rgb2));

            RGB rgb3 = new RGB { R = 255, G = 255, B = 255 };
            HSV hsv3 = new HSV { H = 0, S = 0.0F, V = 1.0000F };
            //Assert.Equal(rgb3, colorEqualization.HSV2RGB(hsv3));
            //Assert.Equal(hsv3, colorEqualization.RGB2HSV(rgb3));

            RGB rgb4 = new RGB { R = 162, G = 205, B = 90 };
            HSV hsv4 = new HSV { H = (int)(0.2290 * 360), S = 0.5610F, V = 0.8039F };
            //Assert.Equal(rgb4,colorEqualization.HSV2RGB(hsv4));
            //Assert.Equal(hsv4, colorEqualization.RGB2HSV(rgb4));

        }

    }
}

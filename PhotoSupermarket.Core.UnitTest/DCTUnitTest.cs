using PhotoSupermarket.Core.Compression;
using PhotoSupermarket.Core.Util;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace PhotoSupermarket.Core.UnitTest
{
    public class DCTUnitTest
    {
        [Fact]
        public void TestGenerateDCTBlock()
        {
            int[,] block = new int[4, 4]
            {
                { 42, 66, 68, 66 },
                { 92, 4 , 76, 17 },
                { 79, 85, 74, 71 },
                { 96, 93, 39, 3  }
            };
            var matrix = DCTCompression.GenerateDCTBlock(block);
            var shouldBe = new double[4, 4]
            {
                { 242.7500,  48.4317,  -9.7500,  23.5052 },
                { -12.6428, -54.0659,   7.4278,  22.7950 },
                {  -6.2500,  10.7158, -19.7500, -38.8046 },
                {  40.6852, -38.7050, -11.4653, -45.9341 }
            };
            for (int i = 0; i < 4; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    Assert.Equal(shouldBe[i, k], matrix[i, k], 4);
                }
            }
        }

        [Fact]
        public void TestGenerateDCTReverseBlock()
        {
            double[,] block = new double[4, 4]
            {
                { 242.7500,  48.4317,  -9.7500,  23.5052 },
                { -12.6428, -54.0659,   7.4278,  22.7950 },
                {  -6.2500,  10.7158, -19.7500, -38.8046 },
                {  40.6852, -38.7050, -11.4653, -45.9341 }
            };
            var matrix = DCTCompression.GenerateDCTReverseBlock(block);
            var shouldBe = new int[4, 4]
            {
                { 42, 66, 68, 66 },
                { 92, 4 , 76, 17 },
                { 79, 85, 74, 71 },
                { 96, 93, 39, 3  }
            };
            for (int i = 0; i < 4; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    Assert.Equal(shouldBe[i, k], matrix[i, k]);
                }
            }
        }

        [Fact]
        public void TestGetDCTImage()
        {
            var image = ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\lenna_gray.bmp");
            BmpImage dctImage = new DCTCompression(image, 15).GetDCTImage();
            ImageFile.SaveBmpImage(dctImage, "..\\..\\..\\TestImages\\lenna_dct_modified.bmp");
        }

        [Fact]
        public void TestGetDCTReverseImage()
        {
            var image = ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\lenna_gray.bmp");
            var dct = new DCTCompression(image, 15);
            BmpImage dctImage = dct.GetDCTImage();
            ImageFile.SaveBmpImage(dctImage, "..\\..\\..\\TestImages\\lenna_dct_modified.bmp");
            BmpImage reverseImage = dct.GetDCTReverseImage(1.0);
            ImageFile.SaveBmpImage(reverseImage, "..\\..\\..\\TestImages\\lenna_dct_reverse_modified.bmp");
        }

        [Fact]
        public void TestGetDCTReverseImageWithCompression()
        {
            var image = ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\lenna_gray.bmp");
            var dct = new DCTCompression(image, 15);
            BmpImage dctImage = dct.GetDCTImage();
            ImageFile.SaveBmpImage(dctImage, "..\\..\\..\\TestImages\\lenna_dct_comp_modified.bmp");
            BmpImage reverseImage = dct.GetDCTReverseImage(8.0);
            ImageFile.SaveBmpImage(reverseImage, "..\\..\\..\\TestImages\\lenna_dct_reverse_comp_modified.bmp");
        }
    }
}

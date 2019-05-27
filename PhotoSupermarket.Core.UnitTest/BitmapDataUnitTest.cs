using System;
using Xunit;
using PhotoSupermarket.Core;
using PhotoSupermarket.Core.Model;

namespace PhotoSupermarket.Core.UnitTest
{
    public class BitmapDataUnitTest
    {
        [Fact]
        public void TestGet1BitDataAt()
        {
            BitmapData bitmapData = new BitmapData();
            bitmapData.Data = new byte[] {
                0b00001111, 0b10101010, 0b00001111, 0b11110_000,
                0b00110011, 0b00000000, 0b00000000, 0b00000_000
            };
            bitmapData.ColorMode = BitmapColorMode.MonoChrome;
            bitmapData.Width = 29;
            bitmapData.Height = 2;
            Assert.False(bitmapData.Get1BitDataAt(0, 0));
            Assert.True(bitmapData.Get1BitDataAt(27, 0));
            Assert.False(bitmapData.Get1BitDataAt(4, 1));
            Assert.False(bitmapData.Get1BitDataAt(28, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => bitmapData.Get1BitDataAt(29, 1));
            Assert.Throws<NotThisColorModeException>(() => bitmapData.Get8BitDataAt(1, 0));
        }

        [Fact]
        public void TestGet8BitDataAt()
        {
            BitmapData bitmapData = new BitmapData();
            bitmapData.Data = new byte[] {
                33, 255, 29, 8, 40, 82, 39, 0,
                28, 128, 37, 200, 182, 22, 47, 0,
                89, 45, 221, 120, 23, 64, 11, 0
            };
            bitmapData.ColorMode = BitmapColorMode.TwoFiftySixColors;
            bitmapData.Width = 7;
            bitmapData.Height = 3;
            Assert.Equal(33, bitmapData.Get8BitDataAt(0, 0));
            Assert.Equal(47, bitmapData.Get8BitDataAt(6, 1));
            Assert.Equal(45, bitmapData.Get8BitDataAt(1, 2));
            Assert.Equal(11, bitmapData.Get8BitDataAt(6, 2));
            Assert.Throws<ArgumentOutOfRangeException>(() => bitmapData.Get8BitDataAt(8, 1));
            Assert.Throws<NotThisColorModeException>(() => bitmapData.Get1BitDataAt(1, 0));
        }

        [Fact]
        public void TestGetRGBDataAt()
        {
            BitmapData bitmapData = new BitmapData();
            bitmapData.Data = new byte[] {
                33, 255, 29,    8, 40, 82,     39, 88, 12,     0, 0, 0,
                28, 128, 37,    200, 182, 22,  47, 19, 192,    0, 0, 0,
                89, 45, 221,    120, 23, 64,   11, 33, 191,    0, 0, 0
            };
            bitmapData.ColorMode = BitmapColorMode.TrueColor;
            bitmapData.Width = 3;
            bitmapData.Height = 3;
            Assert.Equal(new RGB()
            {
                R = 29,
                G = 255,
                B = 33
            }, bitmapData.GetRGBDataAt(0, 0));
            Assert.Equal(new RGB()
            {
                R = 12,
                G = 88,
                B = 39
            }, bitmapData.GetRGBDataAt(2, 0));
            Assert.Equal(new RGB()
            {
                R = 191,
                G = 33,
                B = 11
            }, bitmapData.GetRGBDataAt(2, 2));
            Assert.Throws<ArgumentOutOfRangeException>(() => bitmapData.GetRGBDataAt(3, 1));
            Assert.Throws<NotThisColorModeException>(() => bitmapData.Get8BitDataAt(1, 0));
        }

        [Fact]
        public void TestGetRGBADataAt()
        {
            BitmapData bitmapData = new BitmapData();
            bitmapData.Data = new byte[] {
                33, 255, 29, 8,     40, 82, 39, 88,     12, 0, 0, 0,
                28, 128, 37, 200,   182, 22, 47, 19,    192, 0, 0, 0,
                89, 45, 221, 120,   23, 64, 11, 33,     191, 0, 0, 0
            };
            bitmapData.ColorMode = BitmapColorMode.RGBA;
            bitmapData.Width = 3;
            bitmapData.Height = 3;
            Assert.Equal(new RGBA()
            {
                R = 8,
                G = 29,
                B = 255,
                A = 33
            }, bitmapData.GetRGBADataAt(0, 0));
            Assert.Equal(new RGBA()
            {
                R = 0,
                G = 0,
                B = 0,
                A = 12
            }, bitmapData.GetRGBADataAt(2, 0));
            Assert.Equal(new RGBA()
            {
                R = 0,
                G = 0,
                B = 0,
                A = 191
            }, bitmapData.GetRGBADataAt(2, 2));
            Assert.Throws<ArgumentOutOfRangeException>(() => bitmapData.GetRGBADataAt(3, 1));
            Assert.Throws<NotThisColorModeException>(() => bitmapData.GetRGBDataAt(1, 0));
        }
    }
}

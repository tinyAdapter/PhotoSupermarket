using System;
using Xunit;

namespace PhotoSupermarket.Core.UnitTest
{
    public class PaletteUnitTest
    {
        [Fact]
        public void TestReadPalette()
        {
            var image = Util.ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\lenna_gray.bmp");
            Assert.Equal(8, image.InfoHeader.BitCount);
            Assert.Equal(0u, image.Palette[0].Blue);
            Assert.Equal(0u, image.Palette[0].Green);
            Assert.Equal(0u, image.Palette[0].Red);
            Assert.Equal(1u, image.Palette[1].Blue);
            var image2 = Util.ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\lenna.bmp");
            Assert.Equal(24, image2.InfoHeader.BitCount);
            Assert.False(image2.HasPalette());

        }
    }
}

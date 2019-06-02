using PhotoSupermarket.Core.ModeConverter;
using PhotoSupermarket.Core.Model;
using System;
using System.IO;
using Xunit;

namespace PhotoSupermarket.Core.UnitTest
{
    public class ModeConverterUnitTest
    {
        [Fact]
        public void TestHSIConverter()
        {
            var image = Util.ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\lenna.bmp");
            image = new HSIConverter(image).Convert().Image;
            Util.ImageFile.SaveBmpImage(image, "..\\..\\..\\TestImages\\lenna_gray_modified.bmp");
            image = Util.ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\lenna_gray_modified.bmp");
            // File header
            Assert.Equal(0x4D42, image.FileHeader.Type);
            Assert.Equal((uint)(14 + 40 + 256 * 4 + 512 * 512), image.FileHeader.Size);
            Assert.Equal(0, image.FileHeader.Reserved1);
            Assert.Equal(0, image.FileHeader.Reserved2);
            Assert.Equal((uint)(14 + 40 + 256 * 4), image.FileHeader.OffBits);
            // Info header
            Assert.Equal(40u, image.InfoHeader.Size);
            Assert.Equal(512, image.InfoHeader.Width);
            Assert.Equal(512, image.InfoHeader.Height);
            Assert.Equal(1u, image.InfoHeader.Planes);
            Assert.Equal(8u, image.InfoHeader.BitCount);
            Assert.Equal(0u, image.InfoHeader.Compression);
            Assert.Equal((uint)(512 * 512), image.InfoHeader.SizeImage);
            Assert.Equal(0, image.InfoHeader.XPelsPerMeter);
            Assert.Equal(0, image.InfoHeader.YPelsPerMeter);
            Assert.Equal(0u, image.InfoHeader.ClrUsed);
            Assert.Equal(0u, image.InfoHeader.ClrImportanet);
            // Palette
            Assert.Equal(256, image.Palette.Length);
            for (int i = 0; i < image.Palette.Length; i++)
            {
                Assert.Equal(i, image.Palette[i].Red);
                Assert.Equal(i, image.Palette[i].Green);
                Assert.Equal(i, image.Palette[i].Blue);
                Assert.Equal(0, image.Palette[i].Flags);
            }
        }
    }
}

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

        [Fact]
        public void TestSingleThresholdConverter()
        {
            var image = Util.ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\lenna_gray.bmp");
            image = new SingleThresholdConverter(image).Convert().Image;
            Util.ImageFile.SaveBmpImage(image, "..\\..\\..\\TestImages\\lenna_single_threshold.bmp");
            image = Util.ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\lenna_single_threshold.bmp");
            // File header
            Assert.Equal(0x4D42, image.FileHeader.Type);
            Assert.Equal((uint)(14 + 40 + 2 * 4 + 320 * 300 / 8), image.FileHeader.Size);
            Assert.Equal(0, image.FileHeader.Reserved1);
            Assert.Equal(0, image.FileHeader.Reserved2);
            Assert.Equal((uint)(14 + 40 + 2 * 4), image.FileHeader.OffBits);
            // Info header
            Assert.Equal(40u, image.InfoHeader.Size);
            Assert.Equal(300, image.InfoHeader.Width);
            Assert.Equal(300, image.InfoHeader.Height);
            Assert.Equal(1u, image.InfoHeader.Planes);
            Assert.Equal(1u, image.InfoHeader.BitCount);
            Assert.Equal(0u, image.InfoHeader.Compression);
            Assert.Equal((uint)(320 * 300 / 8), image.InfoHeader.SizeImage);
            Assert.Equal(0, image.InfoHeader.XPelsPerMeter);
            Assert.Equal(0, image.InfoHeader.YPelsPerMeter);
            Assert.Equal(0u, image.InfoHeader.ClrUsed);
            Assert.Equal(0u, image.InfoHeader.ClrImportanet);
            // Palette
            Assert.Equal(2, image.Palette.Length);
            Assert.Equal(0, image.Palette[0].Red);
            Assert.Equal(0, image.Palette[0].Green);
            Assert.Equal(0, image.Palette[0].Blue);
            Assert.Equal(255, image.Palette[1].Red);
            Assert.Equal(255, image.Palette[1].Green);
            Assert.Equal(255, image.Palette[1].Blue);
            Assert.Equal(0, image.Palette[0].Flags);
        }

        [Fact]
        public void TestDitherConverter()
        {
            var image = Util.ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\lenna_gray.bmp");
            image = new DitherConverter(image, 1).Convert().Image;
            Util.ImageFile.SaveBmpImage(image, "..\\..\\..\\TestImages\\lenna_dither_modified.bmp");
            image = Util.ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\lenna_dither_modified.bmp");
            // File header
            Assert.Equal(0x4D42, image.FileHeader.Type);
            Assert.Equal((uint)(14 + 40 + 2 * 4 + image.Data.GetRealSize()), image.FileHeader.Size);
            Assert.Equal(0, image.FileHeader.Reserved1);
            Assert.Equal(0, image.FileHeader.Reserved2);
            Assert.Equal((uint)(14 + 40 + 2 * 4), image.FileHeader.OffBits);
            // Info header
            Assert.Equal(40u, image.InfoHeader.Size);
            Assert.Equal(600, image.InfoHeader.Width);
            Assert.Equal(600, image.InfoHeader.Height);
            Assert.Equal(1u, image.InfoHeader.Planes);
            Assert.Equal(1u, image.InfoHeader.BitCount);
            Assert.Equal(0u, image.InfoHeader.Compression);
            Assert.Equal((uint)image.Data.GetRealSize(), image.InfoHeader.SizeImage);
            Assert.Equal(0, image.InfoHeader.XPelsPerMeter);
            Assert.Equal(0, image.InfoHeader.YPelsPerMeter);
            Assert.Equal(0u, image.InfoHeader.ClrUsed);
            Assert.Equal(0u, image.InfoHeader.ClrImportanet);
            // Palette
            Assert.Equal(0, image.Palette[0].Red);
            Assert.Equal(0, image.Palette[0].Green);
            Assert.Equal(0, image.Palette[0].Blue);
            Assert.Equal(0, image.Palette[0].Flags);
            Assert.Equal(255, image.Palette[1].Red);
            Assert.Equal(255, image.Palette[1].Green);
            Assert.Equal(255, image.Palette[1].Blue);
            Assert.Equal(0, image.Palette[1].Flags);
        }
    }
}

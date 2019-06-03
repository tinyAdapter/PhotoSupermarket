using PhotoSupermarket.Core.Model;
using System;
using System.IO;
using Xunit;

namespace PhotoSupermarket.Core.UnitTest
{
    public class ImageFileUnitTest
    {
        [Fact]
        public void TestLoadRGBBmpImage()
        {
            var image = Util.ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\lenna.bmp");
            //ParseFileHeader
            Assert.Equal(0x4D42, image.FileHeader.Type);
            Assert.Equal(786486u, image.FileHeader.Size);
            Assert.Equal(0, image.FileHeader.Reserved1);
            Assert.Equal(0, image.FileHeader.Reserved2);
            Assert.Equal(54u, image.FileHeader.OffBits);

            //ParseInfoHeader
            Assert.Equal(40u, image.InfoHeader.Size);
            Assert.Equal(512, image.InfoHeader.Width);
            Assert.Equal(512, image.InfoHeader.Height);
            Assert.Equal(1u, image.InfoHeader.Planes);
            Assert.Equal(24u, image.InfoHeader.BitCount);
            Assert.Equal(0u, image.InfoHeader.Compression);
            Assert.Equal(786432u, image.InfoHeader.SizeImage);
            Assert.Equal(0, image.InfoHeader.XPelsPerMeter);
            Assert.Equal(0, image.InfoHeader.YPelsPerMeter);
            Assert.Equal(0u, image.InfoHeader.ClrUsed);
            Assert.Equal(0u, image.InfoHeader.ClrImportanet);
        }

        [Fact]
        public void TestLoadGrayBmpImage()
        {
            var image = Util.ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\lenna_gray.bmp");
            //ParseFileHeader
            Assert.Equal(0x4D42, image.FileHeader.Type);
            Assert.Equal(91080u, image.FileHeader.Size);
            Assert.Equal(0, image.FileHeader.Reserved1);
            Assert.Equal(0, image.FileHeader.Reserved2);
            Assert.Equal(1078u, image.FileHeader.OffBits);

            //ParseInfoHeader
            Assert.Equal(40u, image.InfoHeader.Size);
            Assert.Equal(300, image.InfoHeader.Width);
            Assert.Equal(300, image.InfoHeader.Height);
            Assert.Equal(1u, image.InfoHeader.Planes);
            Assert.Equal(8u, image.InfoHeader.BitCount);
            Assert.Equal(0u, image.InfoHeader.Compression);
            Assert.Equal(90002u, image.InfoHeader.SizeImage);
            Assert.Equal(2834, image.InfoHeader.XPelsPerMeter);
            Assert.Equal(2834, image.InfoHeader.YPelsPerMeter);
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
        public void TestSaveBmpImage()
        {
            var image = Util.ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\lenna.bmp");
            for (int i = 0; i < image.InfoHeader.Height; i++)
            {
                image.Data.SetRGBDataAt(i, i, new RGB(0, 0, 0));
                image.Data.SetRGBDataAt(
                    image.InfoHeader.Height - i - 1, 
                    i, 
                    new RGB(0, 0, 0)
                );
            }
            Util.ImageFile.SaveBmpImage(image, "..\\..\\..\\TestImages\\lenna_modified.bmp");
            image = Util.ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\lenna_modified.bmp");
            // Unmodified file header
            Assert.Equal(0x4D42, image.FileHeader.Type);
            Assert.Equal(786486u, image.FileHeader.Size);
            Assert.Equal(0, image.FileHeader.Reserved1);
            Assert.Equal(0, image.FileHeader.Reserved2);
            Assert.Equal(54u, image.FileHeader.OffBits);
            // Unmodified info header
            Assert.Equal(40u, image.InfoHeader.Size);
            Assert.Equal(512, image.InfoHeader.Width);
            Assert.Equal(512, image.InfoHeader.Height);
            Assert.Equal(1u, image.InfoHeader.Planes);
            Assert.Equal(24u, image.InfoHeader.BitCount);
            Assert.Equal(0u, image.InfoHeader.Compression);
            Assert.Equal(786432u, image.InfoHeader.SizeImage);
            Assert.Equal(0, image.InfoHeader.XPelsPerMeter);
            Assert.Equal(0, image.InfoHeader.YPelsPerMeter);
            Assert.Equal(0u, image.InfoHeader.ClrUsed);
            Assert.Equal(0u, image.InfoHeader.ClrImportanet);
            // Modified (0, 0) pixel
            for (int i = 0; i < image.InfoHeader.Height; i++)
            {
                Assert.Equal(new RGB(0, 0, 0), image.Data.GetRGBDataAt(i, i));
                Assert.Equal(new RGB(0, 0, 0), image.Data.GetRGBDataAt(
                    image.InfoHeader.Height - i - 1, 
                    i)
                );
            }
        }

        [Fact]
        public void TestThrowsWhenSavingDirectoryNotFound()
        {
            var image = Util.ImageFile.LoadBmpImage("..\\..\\..\\TestImages\\lenna.bmp");
            Assert.Throws<DirectoryNotFoundException>(() => 
                Util.ImageFile.SaveBmpImage(image, "..\\..\\..\\InvalidDirectory\\lenna_modified.bmp"));
        }
    }
}

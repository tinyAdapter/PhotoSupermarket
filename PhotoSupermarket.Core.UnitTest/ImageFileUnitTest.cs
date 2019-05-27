using System;
using Xunit;

namespace PhotoSupermarket.Core.UnitTest
{
    public class ImageFileUnitTest
    {
        [Fact]
        public void TestLoadBmpImage()
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
    }
}

using System;
using Xunit;

namespace PhotoSupermarket.Core.UnitTest
{
    public class ImageFileUnitTest
    {
        [Fact]
        public void TestLoadBmpImage()
        {
            var image = Util.ImageFile.LoadBmpImage(".\\TestImages\\lenna.bmp");
            Assert.Equal(0x4D42, image.FileHeader.Type);
            Assert.Equal(786486u, image.FileHeader.Size);
            Assert.Equal(0, image.FileHeader.Reserved1);
            Assert.Equal(0, image.FileHeader.Reserved2);
            Assert.Equal(54u, image.FileHeader.OffBits);
        }
    }
}

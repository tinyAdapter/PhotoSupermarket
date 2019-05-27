using System;
using Xunit;

namespace PhotoSupermarket.Core.UnitTest
{
    public class BytesUnitTest
    {
        [Fact]
        public void TestReadUShort()
        {
            byte[] testBytes = { 0, 1, 2, 3 };
            int index = 1;
            Assert.Equal(0x0201, Util.Bytes.ReadUShort(testBytes, ref index));
            Assert.Equal(3, index);
        }

        [Fact]
        public void TestReadUInt()
        {
            byte[] testBytes = { 0, 1, 2, 3 };
            int index = 0;
            Assert.Equal(0x03020100u, Util.Bytes.ReadUInt(testBytes, ref index));
            Assert.Equal(4, index);
        }

        [Fact]
        public void TestReadInt()
        {
            byte[] testBytes = { 0, 1, 2, 3 };
            int index = 0;
            Assert.Equal((int)((testBytes[index] & 0xFF)
          | ((testBytes[index + 1] & 0xFF) << 8)
          | ((testBytes[index + 2] & 0xFF) << 16)
          | ((testBytes[index + 3] & 0xFF) << 24)), Util.Bytes.ReadInt(testBytes, ref index));
            Assert.Equal(4, index);
        }
    }
}

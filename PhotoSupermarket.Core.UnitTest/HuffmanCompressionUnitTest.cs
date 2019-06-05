using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PhotoSupermarket.Core.Compression;
using PhotoSupermarket.Core.Util;

using Xunit;

namespace PhotoSupermarket.Core.UnitTest
{
    public class HuffmanCompressionUnitTest
    {
        [Fact]
        public void TestStoreDictionary()
        {
            byte[] bytes = new byte[] {1,2,3,4,5,6,7,8};
            HuffmanCompression huff = new HuffmanCompression(bytes);
            huff.Compress("..\\..\\..\\TestImages\\huff_dictionary_test.bi");
            //Assert.Equal(bytes.Length, File.ReadAllBytes("..\\..\\..\\TestImages\\huff_dictionary_test.bi")[0]);
        }
    }
}

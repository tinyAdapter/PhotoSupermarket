using System;
using System.Collections.Generic;
using System.IO;
using PhotoSupermarket.Core.Compression;

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

        [Fact]
        public  void TestDecompression()
        {
            byte[] bytes = new byte[] { 1, 1, 1, 2, 3, 23, 4, 65, 3, 5, 3, 6, 4, 78, 5, 9, 56, 5, 8, 5, 8, 4, 8, 43, 78, 4, 87 };
            HuffmanCompression huff = new HuffmanCompression(bytes);
            huff.Compress("F:\\CourseFile\\MultimediaTechnology\\PhotoSupermarket\\PhotoSupermarket.Core.UnitTest\\TestImages\\huffman_compressed_data.bi");
            byte[] storedBytes = File.ReadAllBytes("F:\\CourseFile\\MultimediaTechnology\\PhotoSupermarket\\PhotoSupermarket.Core.UnitTest\\TestImages\\huffman_compressed_data.bi");
            HuffmanDecompression decompression = new HuffmanDecompression("F:\\CourseFile\\MultimediaTechnology\\PhotoSupermarket\\PhotoSupermarket.Core.UnitTest\\TestImages\\huffman_compressed_data.bi");
            byte[] decompressedBytes = decompression.Decompress();

            Assert.Equal(bytes.Length, decompressedBytes.Length);
            for(int i = 0; i < decompressedBytes.Length; i++)
            {
                Assert.Equal(bytes[i], decompressedBytes[i]);
            }
        }

    }
}

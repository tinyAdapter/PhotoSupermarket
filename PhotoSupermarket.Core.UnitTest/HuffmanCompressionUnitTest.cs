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
            huff.Compress("F:\\CourseFile\\MultimediaTechnology\\PhotoSupermarket\\PhotoSupermarket.Core.UnitTest\\TestImages\\huff_dictionary_test.bi");
            byte[] storedBytes = File.ReadAllBytes("F:\\CourseFile\\MultimediaTechnology\\PhotoSupermarket\\PhotoSupermarket.Core.UnitTest\\TestImages\\huff_dictionary_test.bi");
            int iii = 0
            Dictionary<char, string> storedDictionary = HuffmanDecompression.ReadDictionary(storedBytes, ref iii);

            foreach (var v in storedDictionary)
            {
                Console.WriteLine("key:" + Convert.ToString((byte)v.Key, 2) + ",value:" + v.Value);
            }
            Console.WriteLine("--------------------------------------");
            foreach (var v in huff.zipCode)
            {
                Console.WriteLine("key:" + Convert.ToString((byte)v.Key, 2) + ",value:" + v.Value);
            }

            Console.WriteLine("--------------------------------------");

            HuffmanDecompression decomp = new HuffmanDecompression("F:\\CourseFile\\MultimediaTechnology\\PhotoSupermarket\\PhotoSupermarket.Core.UnitTest\\TestImages\\huff_dictionary_test.bi");
            char[] rddd = decomp.Decompress();
            for (int i = 0; i < rddd.Length; i++)
            {
                Console.Write(rddd[i] == (char)bytes[i]);
            }
        }

    }
}

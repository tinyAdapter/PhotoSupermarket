using PhotoSupermarket.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PhotoSupermarket.Core.Compression
{
    class HuffmanDecompression
    {
        private string filePath;
        private Dictionary<char, string> dictionary;
        private string data = "";
        private int currentIndex = 0;

        public HuffmanDecompression(String filePath)
        {
            this.filePath = filePath;
        }

        public char[] Decompress()
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);
            dictionary = ReadDictionary(fileBytes,ref currentIndex);

            return data.ToCharArray();
        }


        public static Dictionary<char, string> ReadDictionary(byte[] fileBytes, ref int currentIndex)
        {
            //字典大小
            int dictionarySize = fileBytes[currentIndex++]+1;

            Dictionary<char, string> dictionary = new Dictionary<char, string>();
            char key;
            int codeLength;
            string value;
            for(int i = 0; i < dictionarySize; i++)
            {
                key = (char)fileBytes[currentIndex++];
                codeLength = fileBytes[currentIndex++];
                value = Convert.ToString(fileBytes[currentIndex++], 2).PadLeft(codeLength,'0');
                dictionary.Add(key, value);
            }

            return dictionary;
        }
        //private void RebuildTree(Dictionary<char, string>)
        //{

        //}
    }
}

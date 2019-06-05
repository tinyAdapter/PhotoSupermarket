using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PhotoSupermarket.Core.Compression
{
    class HuffmanDecompression
    {
        private string filePath;
        private string data = "";
        private int currentIndex;

        public HuffmanDecompression(String filePath)
        {
            this.filePath = filePath;
        }

        public char[] Decompress()
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);

            return data.ToCharArray();
        }

        private rebuildTree() {}
    }
}

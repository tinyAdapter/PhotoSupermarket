using PhotoSupermarket.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace PhotoSupermarket.Core.Compression
{
    public class HuffmanDecompression
    {
        private string filePath;
        private Dictionary<char, string> dictionary;
        private int currentIndex = 0;

        public HuffmanDecompression(String filePath)
        {
            this.filePath = filePath;
        }

        public byte[] Decompress()
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);

            dictionary = ReadDictionary(fileBytes, ref currentIndex);

            HuffmanTree huffTree = RebuildTree(dictionary);

            char[] bitArray = ReadBitArray(fileBytes, ref currentIndex);

            int lastByteOffset = fileBytes[currentIndex];

            byte[] data = ReadData(huffTree, bitArray, lastByteOffset);

            return data;
        }

        private byte[] ReadData(HuffmanTree huffTree, char[] bitArray, int lastByteOffset)
        {
            string data = "";

            for (int i = 0; i < bitArray.Length - lastByteOffset;)
            {
                HuffNode currentNode = huffTree.root;

                while (true)
                {
                    if (bitArray[i] == '0')
                    {
                        currentNode = ((InternalNode)currentNode).leftChild;
                    }
                    else if (bitArray[i] == '1')
                        currentNode = ((InternalNode)currentNode).rightChild;

                    i++;
                    if (currentNode is LeafNode)
                    {
                        data += ((LeafNode)currentNode).E;
                        Console.Write(Convert.ToInt32(((LeafNode)currentNode).E) + " ");
                        break;
                    }
                }
            }

            char[] ca = data.ToCharArray();
            byte[] result = new byte[data.Length];
            for (int i = 0; i < result.Length; i++)
                result[i] = (byte)ca[i];

            return result;
        }

        private char[] ReadBitArray(byte[] fileBytes, ref int currentIndex)
        {
            string data = "";
            byte[] compressedData = new byte[fileBytes.Length - currentIndex - 1];
            for (int i = 0; i < compressedData.Length; i++)
            {
                data += Convert.ToString(fileBytes[currentIndex++], 2).PadLeft(8, '0');
                //compressedData[i] = fileBytes[currentIndex++];
                //Console.WriteLine(Convert.ToString(compressedData[i], 2));
            }

            Console.WriteLine(data);
            return data.ToCharArray();
        }

        public static Dictionary<char, string> ReadDictionary(byte[] fileBytes, ref int currentIndex)
        {
            //字典大小
            int dictionarySize = fileBytes[currentIndex++] + 1;

            Dictionary<char, string> dictionary = new Dictionary<char, string>();
            char key;
            int codeLength;
            string value = "";
            for (int i = 0; i < dictionarySize; i++)
            {
                key = (char)fileBytes[currentIndex++];
                codeLength = fileBytes[currentIndex++];
                for (int j = 0; i < codeLength / 8; j++)
                {
                    value += Convert.ToString(fileBytes[currentIndex++], 2).PadLeft(8, '0');
                }
                value += Convert.ToString(fileBytes[currentIndex++], 2).PadLeft(codeLength % 8, '0');
                dictionary.Add(key, value);
                value = "";
            }

            return dictionary;
        }

        private HuffmanTree RebuildTree(Dictionary<char, string> dictionary)
        {
            HuffmanTree tree = new HuffmanTree(new InternalNode());
            foreach (var v in dictionary)
            {
                char[] huffcode = v.Value.ToCharArray();
                InternalNode currentInternalNode = (InternalNode)tree.root;
                for (int i = 0; i < huffcode.Length - 1; i++)
                {
                    if (huffcode[i] == '0')
                    {
                        if (currentInternalNode.leftChild == null)
                            currentInternalNode.leftChild = new InternalNode();
                        currentInternalNode = (InternalNode)currentInternalNode.leftChild;
                    }
                    else if (huffcode[i] == '1')
                    {
                        if (currentInternalNode.rightChild == null)
                            currentInternalNode.rightChild = new InternalNode();
                        currentInternalNode = (InternalNode)currentInternalNode.rightChild;
                    }
                }
                if (huffcode[huffcode.Length - 1] == '0')
                {
                    currentInternalNode.leftChild = new LeafNode(v.Key, 0);
                }
                else if (huffcode[huffcode.Length - 1] == '1')
                {
                    currentInternalNode.rightChild = new LeafNode(v.Key, 0);
                }
            }

            testTree(tree.root);
            Console.WriteLine();

            return tree;
        }

        private void testTree(HuffNode node)
        {
            if (node is LeafNode)
            {
                Console.Write(Convert.ToInt16(((LeafNode)node).E));
                Console.Write("//");
                return;
            }
            if (node is InternalNode)
            {
                Console.Write("_");
                testTree(((InternalNode)node).leftChild);
                testTree(((InternalNode)node).rightChild);
            }
        }
    }
}
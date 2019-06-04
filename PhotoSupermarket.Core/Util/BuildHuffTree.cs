using PhotoSupermarket.Core.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PhotoSupermarket.Core.Util
{

    public class BuildHuffTree
    {
        private char[] data;
        private Dictionary<char, int> freqs;
        public List<HuffNode> nodes;
        public Dictionary<char, string> dictionaryOfZipcodes;

        public BuildHuffTree(char[] data)
        {
            this.data = data;
            this.nodes = new List<HuffNode>();
            this.dictionaryOfZipcodes = new Dictionary<char, string>();

            CalFrequency();
            InitialTrees();
        }

        //生成Huffman树
        private HuffmanTree Build()
        {
            while (nodes.Count > 1)
            {
                HuffNode firstNode = this.RemoveFirst();
                HuffNode secondNode = this.RemoveFirst();

                int newWeight = firstNode.weight + secondNode.weight;

                nodes.Add(new InternalNode(firstNode, secondNode, newWeight));
            }

            return new HuffmanTree(nodes[0]);

        }

        //生成zipcode
        public Dictionary<char,string> generateZipcode()
        {
            Build();

            if (!nodes[0].isLeaf)
            {
                IterativeTraverse((InternalNode)nodes[0], "");
            }

            return this.dictionaryOfZipcodes;

        }

        private void IterativeTraverse(InternalNode node, string zipcode)
        {
            if (node.leftChild.isLeaf)
                dictionaryOfZipcodes.Add(((LeafNode)node.leftChild).E, zipcode + "0");
            else
                IterativeTraverse((InternalNode)node.leftChild, zipcode + "0");


            if (node.rightChild.isLeaf)           
                dictionaryOfZipcodes.Add(((LeafNode)node.rightChild).E, zipcode + "1");
            else
                IterativeTraverse((InternalNode)node.rightChild, zipcode += "1");

        }

        //计算每个值
        private void CalFrequency()
        {
            freqs = new Dictionary<char, int>();

            foreach (var item in data)
            {
                if (freqs.ContainsKey(item))
                {
                    freqs[item]++;
                }
                else
                    freqs.Add(item, 0);
            }
        }

        //将统计结果转变单节点树放入数组中
        private void InitialTrees()
        {
            int index = 0;
            foreach (KeyValuePair<char, int> item in freqs)
            {
                nodes.Add(new LeafNode(item.Key, item.Value));
                index++;
            }
        }

        //private void QuickSortTrees(int left, int right)
        //{
        //    if (left >= right)
        //        return;

        //    //快排实现
        //    HuffmanTree privot = nodes[left];
        //    int curr_left = left++;
        //    int curr_right = right;

        //    while(curr_left <= curr_right)
        //    {
        //        if (nodes[curr_left].Weight() < privot.Weight())
        //            curr_left++;
        //        if (nodes[curr_right].Weight() > privot.Weight())
        //            curr_right--;
        //    }

        //    if(curr_left < curr_right)
        //        Swap(ref nodes[curr_left], ref nodes[curr_right]);
        //    if(privot.Weight() < nodes[curr_left].Weight())
        //        Swap(ref nodes[curr_left], ref privot);

        //    QuickSortTrees(left, (left + right) / 2);
        //    QuickSortTrees((left + right) / 2+1, right);
        //}

        private HuffNode RemoveFirst()
        {
            if (nodes.Count > 0)
            {
                nodes.Sort(Compare);
                HuffNode temp = nodes[0];
                nodes.RemoveAt(0);

                return temp;
            }

            else
                return null;
        }

        //private void Swap(ref HuffmanTree l, ref HuffmanTree r)
        //{
        //    HuffmanTree temp = l;
        //    l = r;
        //    r = temp;
        //}

        public int Compare(HuffNode l, HuffNode r)
        {
            return l.weight.CompareTo(r.weight);
        }

    }
}

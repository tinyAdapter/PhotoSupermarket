using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoSupermarket.Core.Model
{
    public class HuffmanTree
    {
        public HuffNode root;

        public HuffmanTree(HuffNode root)
        {
            this.root = root;
        }

        public HuffmanTree(char E, int weight)
        {
            root = new LeafNode(E, weight);
        }

        public HuffmanTree(HuffNode l ,HuffNode r, int weight)
        {
            root = new InternalNode(l, r, weight);
        }

        public int Weight()
        {
            return root.weight;
        }
    }

    public class HuffNode
    {
        public int weight { get; set; }
        public bool isLeaf { get; set; }

        public HuffNode(int weight, bool isLeaf)
        {
            this.weight = weight;
            this.isLeaf = isLeaf;
        }
      
    }

    public class LeafNode : HuffNode
    {
        public char E { get; set; }
         
        public LeafNode(char E, int weight): base(weight, true)
        {
            this.E = E;
        }

    }

    public class InternalNode: HuffNode
    {
        public HuffNode leftChild;
        public HuffNode rightChild;

        public InternalNode(HuffNode l, HuffNode r, int weight): base(weight, false)
        {
            leftChild = l;
            rightChild = r;
        }
        
    }


}

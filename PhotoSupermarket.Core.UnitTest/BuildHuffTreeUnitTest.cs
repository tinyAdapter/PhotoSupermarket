
using PhotoSupermarket.Core.Model;
using PhotoSupermarket.Core.Util;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace PhotoSupermarket.Core.UnitTest
{
    public class BuildHuffTreeUnitTest
    {
        [Fact]
        public void quickSortTest()
        {
            string str = "dffdsfdsfdsfdsfdsfsdfdsfdsfsdsds";
            char[] temp = str.ToCharArray();

            BuildHuffTree btf = new BuildHuffTree(temp);

       
            Dictionary<char, string> dic = btf.generateZipcode();

            Assert.False(btf.nodes.Count==0);
            Assert.True(dic.Count == 3);
        }
    }
}

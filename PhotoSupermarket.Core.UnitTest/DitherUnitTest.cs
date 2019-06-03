using PhotoSupermarket.Core.Util;
using System;
using System.IO;
using Xunit;

namespace PhotoSupermarket.Core.UnitTest
{
    public class DitherUnitTest
    {
        [Fact]
        public void TestGenerateDitherMatrix()
        {
            var matrix = Dither.GenerateDitherMatrix(1);
            Assert.Equal(new int[2, 2] 
            { 
                { 0, 2 }, 
                { 3, 1 }
            }, matrix);
            matrix = Dither.GenerateDitherMatrix(2);
            Assert.Equal(new int[4, 4] 
            { 
                { 0 , 8 , 2 , 10 },
                { 12, 4 , 14, 6  },
                { 3 , 11, 1 , 9  },
                { 15, 7 , 13, 5  }
            }, matrix);
        }

        [Fact]
        public void TestThrowsWhenArgumentInvalid()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Dither.GenerateDitherMatrix(0));
        }
    }
}

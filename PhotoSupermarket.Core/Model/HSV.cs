using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoSupermarket.Core.Model
{
    public class HSV
    {
        public HSV() { }
        public HSV(int H, float S, float V)
        {
            this.H = H;
            this.S = S;
            this.V = V;
        }
        public int H { get; set; }   //[0,360)
        public float S { get; set; }  //[0,1]
        public float V { get; set; }  //[0,1]
    }
}

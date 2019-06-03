using PhotoSupermarket.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoSupermarket.Core.ModeConverter
{
    public class YCbCrConverter : ColorToGrayConverter
    {
        public YCbCrConverter(BmpImage data) : base(data)
        {
        }

        protected override byte GetGrayValueAt(int x, int y)
        {
            RGB oldValue = oldData.GetRGBDataAt(x, y);
            //Y = 0.257*R+0.564*G+0.098*B+16
            return (byte)((oldValue.R * 0.257) + (oldValue.G * 0.564) + (oldValue.B * 0.098) + 16);
        }
    }
}

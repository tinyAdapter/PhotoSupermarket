using PhotoSupermarket.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoSupermarket.Core.ModeConverter
{
    public class HSIConverter : ColorToGrayConverter
    {
        public HSIConverter(BmpImage data) : base(data)
        {
        }

        protected override byte GetGrayValueAt(int x, int y)
        {
            RGB oldValue = oldData.GetRGBDataAt(x, y);
            return (byte)((oldValue.R + oldValue.G + oldValue.B)/ 3);
        }
    }
}

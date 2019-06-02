using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoSupermarket.Core.ModeConverter
{
    public abstract class BaseConverter
    {
        public BaseConverter Convert()
        {
            DoConvert();
            return this;
        }

        protected abstract void DoConvert();

        public BmpImage Image { get; private set; }
        protected BitmapData oldData;

        public BaseConverter(BmpImage image)
        {
            Image = image;
            oldData = (BitmapData)Image.Data.Clone();
        }
    }
}

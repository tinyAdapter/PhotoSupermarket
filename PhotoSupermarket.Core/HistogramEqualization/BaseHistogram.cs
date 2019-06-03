using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoSupermarket.Core.HistogramEqualization
{
    public abstract class BaseHistogram
    {
        public BaseHistogram Equalization()
        {
            DoEqualization();
            return this;
        }

        protected abstract void DoEqualization();

        public BmpImage Image { get; private set; }
        protected BitmapData oldData;

        public BaseHistogram(BmpImage image)
        {
            Image = image;
            oldData = (BitmapData)Image.Data.Clone();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoSupermarket.Core.Compression
{
    class UniformQuantizing
    {
        public BmpImage Image { get; private set; }
        protected BitmapData oldData;
        protected double QuantizedInterval;

        public double[,] UniformQuantizingData { get; private set; }

        public UniformQuantizing(BmpImage image, double CompressRatio)
        {
            QuantizedInterval = 256.0 / CompressRatio;
            Image = image;
            oldData = (BitmapData)Image.Data.Clone();
        }
    }
}

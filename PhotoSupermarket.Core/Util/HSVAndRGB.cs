using System;
using System.Collections.Generic;
using System.Text;
using PhotoSupermarket.Core.Model;

namespace PhotoSupermarket.Core.Util
{
    public class HSVAndRGB
    {
        public static HSV RGB2HSV(RGB rgb)
        {
            byte max = Math.Max(Math.Max(rgb.R, rgb.G), rgb.B);
            byte min = Math.Min(Math.Min(rgb.R, rgb.G), rgb.B);
            HSV hsv = new HSV();
            hsv.V = max / 255.0F;

            if (max == 0)
                hsv.S = 0;
            else
                hsv.S = (max - min) * 1.0F / max;

            if (max == min)
                hsv.H = 0;
            else if (rgb.R == max)
                hsv.H = (int)((rgb.G - rgb.B) * 1.0 / (max - min) * 60);
            else if (rgb.G == max)
                hsv.H = (int)(120 + (rgb.B - rgb.R) * 1.0 / (max - min) * 60);
            else
                hsv.H = (int)(240 + (rgb.R - rgb.G) * 1.0 / (max - min) * 60);

            if (hsv.H < 0)
            {
                hsv.H += 360;
            }

            return hsv;
        }

        public static RGB HSV2RGB(HSV hsv)
        {
            int h = hsv.H;
            float s = hsv.S, v = hsv.V;

            if (s == 0)
                return new RGB
                {
                    R = (byte)(v * 255),
                    G = (byte)(v * 255),
                    B = (byte)(v * 255)
                };

            int i = h / 60;
            float f = h * 1.0F / 60 - i;

            float p = v * (1 - s);
            float q = v * (1 - s * f);
            float t = v * (1 - s * (1 - f));

            RGB rgb = new RGB();

            switch (i)
            {
                case 0:
                    rgb.R = (byte)(v * 255);
                    rgb.G = (byte)(t * 255);
                    rgb.B = (byte)(p * 255);
                    break;

                case 1:
                    rgb.R = (byte)(q * 255);
                    rgb.G = (byte)(v * 255);
                    rgb.B = (byte)(p * 255);
                    break;

                case 2:
                    rgb.R = (byte)(p * 255);
                    rgb.G = (byte)(v * 255);
                    rgb.B = (byte)(t * 255);
                    break;

                case 3:
                    rgb.R = (byte)(p * 255);
                    rgb.G = (byte)(q * 255);
                    rgb.B = (byte)(v * 255);
                    break;

                case 4:
                    rgb.R = (byte)(t * 255);
                    rgb.G = (byte)(p * 255);
                    rgb.B = (byte)(v * 255);
                    break;

                case 5:
                    rgb.R = (byte)(v * 255);
                    rgb.G = (byte)(p * 255);
                    rgb.B = (byte)(q * 255);
                    break;
            }

            return rgb;
        }
    }
}

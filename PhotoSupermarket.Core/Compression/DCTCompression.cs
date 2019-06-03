using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoSupermarket.Core.Compression
{
    public class DCTCompression
    {
        public BmpImage Image { get; private set; }
        public int BlockSize { get; private set; }
        protected BitmapData oldData;

        public double[,] DCTData { get; private set; }

        public DCTCompression(BmpImage image, int blockSize)
        {
            Image = image;
            oldData = (BitmapData)Image.Data.Clone();
            BlockSize = blockSize;
        }

        public BmpImage GetDCTImage()
        {
            if (Image.Data.ColorMode != BitmapColorMode.TwoFiftySixColors)
            {
                throw new NotThisColorModeException();
            }

            BmpImage result = NewGrayScaleImage(Image.Data.Width, Image.Data.Height);
            DCTData = new double[Image.Data.Width, Image.Data.Height];

            for (int i = 0; i < Image.Data.Width / BlockSize; i++)
            {
                for (int k = 0; k < Image.Data.Height / BlockSize; k++)
                {
                    int[,] block = new int[BlockSize, BlockSize];
                    for (int m = 0; m < BlockSize; m++)
                    {
                        for (int n = 0; n < BlockSize; n++)
                        {
                            block[m, n] = Image.Data.Get8BitDataAt(BlockSize * i + m, BlockSize * k + n);
                        }
                    }
                    double[,] dctBlock = GenerateDCTBlock(block);
                    for (int m = 0; m < BlockSize; m++)
                    {
                        for (int n = 0; n < BlockSize; n++)
                        {
                            result.Data.Set8BitDataAt(
                                BlockSize * i + m, BlockSize * k + n,
                                (byte)((dctBlock[m, n] + 128) / 2));
                            DCTData[BlockSize * i + m, BlockSize * k + n] = dctBlock[m, n];
                        }
                    }
                }
            }
            
            return result;
        }

        public BmpImage GetDCTReverseImage(double compressRatio)
        {
            if (Image.Data.ColorMode != BitmapColorMode.TwoFiftySixColors)
            {
                throw new NotThisColorModeException();
            }
            if (DCTData == null) throw new UnauthorizedAccessException();
            if (compressRatio < 1.0) throw new ArgumentOutOfRangeException();

            BmpImage result = NewGrayScaleImage(Image.Data.Width, Image.Data.Height);

            for (int i = 0; i < Image.Data.Width / BlockSize; i++)
            {
                for (int k = 0; k < Image.Data.Height / BlockSize; k++)
                {
                    double[,] block = new double[BlockSize, BlockSize];
                    for (int m = 0; m < BlockSize; m++)
                    {
                        for (int n = 0; n < BlockSize; n++)
                        {
                            if ((m + n > BlockSize * BlockSize / (compressRatio * compressRatio)))
                            {
                                block[m, n] = 0;
                            }
                            else
                            {
                                block[m, n] = DCTData[BlockSize * i + m, BlockSize * k + n];
                            }
                        }
                    }
                    int[,] dctReverseBlock = GenerateDCTReverseBlock(block);
                    for (int m = 0; m < BlockSize; m++)
                    {
                        for (int n = 0; n < BlockSize; n++)
                        {
                            result.Data.Set8BitDataAt(
                                BlockSize * i + m, BlockSize * k + n,
                                (byte)dctReverseBlock[m, n]);
                        }
                    }
                }
            }

            return result;
        }

        private BmpImage NewGrayScaleImage(int width, int height)
        {
            BmpImage result = new BmpImage()
            {
                Palette = new BitmapPaletteEntry[256]
            };
            for (int i = 0; i < 256; i++)
            {
                result.Palette[i] = new BitmapPaletteEntry((byte)i, (byte)i, (byte)i, 0);
            }
            result.Data = new BitmapData
            {
                ColorMode = BitmapColorMode.TwoFiftySixColors,
                Width = width,
                Height = height
            };
            result.Data.Data = new byte[result.Data.GetRealSize()];

            return result;
        }


        public static double[,] GenerateDCTBlock(int[,] originalBlock)
        {
            int M = originalBlock.GetLength(0);
            int N = originalBlock.GetLength(1);
            double[,] result = new double[M, N];
            
            for (int u = 0; u < M; u++)
            {
                for (int v = 0; v < N; v++)
                {
                    result[u, v] = Alpha(u) * Alpha(v) / Math.Sqrt(M * N);
                    double aggregate = 0;
                    for (int x = 0; x < M; x++)
                    {
                        for (int y = 0; y < N; y++)
                        {
                            aggregate += 
                                Math.Cos((2 * x + 1) * u * Math.PI / (2 * N)) *
                                Math.Cos((2 * y + 1) * v * Math.PI / (2 * M)) *
                                originalBlock[x, y];
                        }
                    }
                    result[u, v] *= aggregate;
                }
            }

            return result;
        }

        public static int[,] GenerateDCTReverseBlock(double[,] originalBlock)
        {
            int M = originalBlock.GetLength(0);
            int N = originalBlock.GetLength(1);
            int[,] result = new int[M, N];

            double tempResult;
            for (int x = 0; x < M; x++)
            {
                for (int y = 0; y < N; y++)
                {
                    tempResult = 1 / Math.Sqrt(M * N);
                    double aggregate = 0;
                    for (int u = 0; u < M; u++)
                    {
                        for (int v = 0; v < N; v++)
                        {
                            aggregate +=
                                Alpha(u) * Alpha(v) *
                                Math.Cos((2 * x + 1) * u * Math.PI / (2 * N)) *
                                Math.Cos((2 * y + 1) * v * Math.PI / (2 * M)) *
                                originalBlock[u, v];
                        }
                    }
                    tempResult *= aggregate;
                    result[x, y] = (int)Math.Round(tempResult);
                }
            }

            return result;
        }

        private static double Alpha(int u)
        {
            if (u == 0) return 1;
            else return Math.Sqrt(2);
        }
    }
}

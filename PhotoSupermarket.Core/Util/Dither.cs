using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoSupermarket.Core.Util
{
    public class Dither
    {
        public static int[,] GenerateDitherMatrix(int power)
        {
            int[,] ditherMatrix = new int[2, 2];

            if (power <= 0) throw new ArgumentOutOfRangeException();

            ditherMatrix[0, 0] = 0; ditherMatrix[0, 1] = 2;
            ditherMatrix[1, 0] = 3; ditherMatrix[1, 1] = 1;
            for (int i = 0; i < power - 1; i++)
            {
                ditherMatrix = EnlargeDitherMatrix(ditherMatrix);
            }

            return ditherMatrix;
        }

        private static int[,] EnlargeDitherMatrix(int[,] matrix)
        {
            int xLength = matrix.GetLength(0);
            int yLength = matrix.GetLength(1);
            int[,] newMatrix = new int[2 * xLength, 2 * yLength];
            for (int i = 0; i < xLength; i++)
            {
                for (int k = 0; k < yLength; k++)
                {
                    newMatrix[i, k] = 4 * matrix[i, k];
                    newMatrix[i, yLength + k] = 4 * matrix[i, k] + 2;
                    newMatrix[xLength + i, k] = 4 * matrix[i, k] + 3;
                    newMatrix[xLength + i, yLength + k] = 4 * matrix[i, k] + 1;
                }
            }
            return newMatrix;
        }
    }
}

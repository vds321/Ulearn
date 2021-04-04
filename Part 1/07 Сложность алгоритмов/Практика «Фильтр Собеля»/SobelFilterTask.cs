using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        private static double[,] TransposedMatrix(double[,] sx)
        {
            var sy = new double[sx.GetLength(1), sx.GetLength(0)];
            var width = sx.GetLength(0);
            var height = sx.GetLength(1);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    sy[i, j] = sx[j, i];
                }
            }
            return sy;
        }

        private static double MultiplicationMatrixElement(double[,] g, int delta, double[,] sobel, int x, int y)
        {
            double result = 0;
            var width = sobel.GetLength(0);
            var height = sobel.GetLength(1);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    result += sobel[i, j] * g[x - delta + i, y - delta + j];
                }
            }
            return result;
        }

        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var sy = TransposedMatrix(sx);
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var delta = sx.GetLength(0) / 2;
            var result = new double[width, height];

            for (int x = delta; x < width - delta; x++)
                for (int y = delta; y < height - delta; y++)
                {
                    var gx = MultiplicationMatrixElement(g, delta, sx, x, y);
                    var gy = MultiplicationMatrixElement(g, delta, sy, x, y);
                    result[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }
            return result;
        }
    }
}
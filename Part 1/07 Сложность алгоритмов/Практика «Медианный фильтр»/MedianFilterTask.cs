using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        private static bool CheckIndexOutOfRange(int x, int y, double[,] original)
        {
            return x >= 0 && x <= original.GetLength(0) - 1 && y >= 0 && y <= original.GetLength(1) - 1;
        }

        private static double CountMedianGray(List<double> window)
        {
            double grayFiltred;
            window.Sort();
            if (window.Count() % 2 == 0)
            {
                grayFiltred = (window[window.Count() / 2] + window[window.Count() / 2 - 1]) / 2.0;
            }
            else grayFiltred = window[window.Count() / 2];
            return grayFiltred;
        }

        private static List<double> MakeWindow(int coordX, int coordY, double[,] original, List<double> window)
        {
            for (int i = coordX - 1; i <= coordX + 1; i++)
            {
                for (int j = coordY - 1; j <= coordY + 1; j++)
                {
                    if (CheckIndexOutOfRange(i, j, original)) window.Add(original[i, j]);
                }
            }
            return window;
        }

        public static double[,] MedianFilter(double[,] original)
        {
            var filtred = new double[original.GetLength(0), original.GetLength(1)];
            var maxX = filtred.GetLength(0);
            var maxY = filtred.GetLength(1);
            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    var window = new List<double>();
                    MakeWindow(i, j, original, window);
                    filtred[i, j] = CountMedianGray(window);
                }
            }
            return filtred;
        }
    }
}
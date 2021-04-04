using System.Collections.Generic;

namespace Recognizer
{
    public static class ThresholdFilterTask
    {
        private static double FindGrayT(double fraction, double[,] original)
        {
            double grayT;
            var grayList = new List<double>();
            foreach (var pixel in original)
            {
                grayList.Add(pixel);
            }
            grayList.Sort();
            if (fraction == 0.0 || fraction * grayList.Count < 1) grayT = grayList[grayList.Count - 1] + 1;
            else if (fraction == 1.0) grayT = 0.0;
            else grayT = grayList[grayList.Count - (int)(grayList.Count * fraction)];
            return grayT;
        }

        public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {
            var grayTPixel = FindGrayT(whitePixelsFraction, original);
            var blackWhite = new double[original.GetLength(0), original.GetLength(1)];
            var maxX = blackWhite.GetLength(0);
            var maxY = blackWhite.GetLength(1);
            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    if (original[i, j] >= grayTPixel) blackWhite[i, j] = 1.0;
                    else blackWhite[i, j] = 0.0;
                }
            }
            return blackWhite;
        }
    }
}
using System;

namespace Fractals
{
    internal static class DragonFractalTask
    {
        public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
        {
            double x = 1;
            double y = 0;
            var random = new Random(seed);

            for (var i = 0; i < iterationsCount; i++)
            {
                int temp = random.Next(2);
                double x1 = FindPosition(x, y, temp, Math.PI / 4)[0];
                double y1 = FindPosition(x, y, temp, Math.PI / 4)[1];
                x = x1;
                y = y1;
                pixels.SetPixel(x, y);
            }
        }

        private static double[] FindPosition(double x, double y, int temp, double angle)
        {
            if (temp == 1) return new double[] {
                                                TakeCoordinats(-y, x, angle),
                                                TakeCoordinats(x, y, angle)
                                                };
            else return new double[] {
                                                TakeCoordinats(-y, x, angle, 3) + 1,
                                                TakeCoordinats(x, y, angle, 3)
                                                };
        }

        private static double TakeCoordinats(double coord1, double coord2, double angle, int angleFactor = 1)
        {
            return (coord1 * Math.Sin(angleFactor * angle) + coord2 * Math.Cos(angleFactor * angle)) / Math.Sqrt(2);
        }
    }
}
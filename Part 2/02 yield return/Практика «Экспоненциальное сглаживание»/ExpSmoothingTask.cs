using System.Collections.Generic;

namespace yield
{
    public static class ExpSmoothingTask
    {
        public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
        {
            double previousExpSmoothedY = 0.0;
            bool firstPass = true;
            foreach (var dataPoint in data)
            {
                double currentExpSmoothedY;
                if (firstPass)
                {
                    currentExpSmoothedY = dataPoint.OriginalY;
                    previousExpSmoothedY = dataPoint.OriginalY;
                    firstPass = false;
                }
                else
                {
                    currentExpSmoothedY = previousExpSmoothedY + alpha * (dataPoint.OriginalY - previousExpSmoothedY);
                    previousExpSmoothedY = currentExpSmoothedY;
                }
                yield return new DataPoint { X = dataPoint.X, OriginalY = dataPoint.OriginalY, ExpSmoothedY = currentExpSmoothedY };
            }
        }
    }
}
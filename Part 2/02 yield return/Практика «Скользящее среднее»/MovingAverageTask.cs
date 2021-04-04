using System.Collections.Generic;
using System.Linq;

namespace yield
{
	public static class MovingAverageTask
	{
		public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
		{
			Queue<double> queue = new Queue<double>();
			double summOfPointY = 0.0;
			foreach (var dataPoint in data)
			{
				queue.Enqueue(dataPoint.OriginalY);
				summOfPointY += dataPoint.OriginalY;
				if (queue.Count > windowWidth)
				{
					summOfPointY -= queue.Dequeue();
				}
				yield return new DataPoint { X = dataPoint.X, OriginalY = dataPoint.OriginalY, AvgSmoothedY = summOfPointY / queue.Count(), ExpSmoothedY = dataPoint.ExpSmoothedY };
			}
		}
	}
}
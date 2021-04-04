using System.Collections.Generic;

namespace yield
{
    public static class MovingMaxTask
    {
        public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
        {
            Queue<double> queue = new Queue<double>();
            LinkedList<double> maxQueue = new LinkedList<double>();
            foreach (var dataPoint in data)
            {
                queue.Enqueue(dataPoint.OriginalY);
                if (queue.Count > windowWidth)
                {
                    if (queue.Dequeue() == maxQueue.First.Value)
                    {
                        maxQueue.RemoveFirst();
                    }
                }
                while (maxQueue.Count != 0 && dataPoint.OriginalY > maxQueue.Last.Value)
                {
                    maxQueue.RemoveLast();
                }
                maxQueue.AddLast(dataPoint.OriginalY);
                yield return new DataPoint { X = dataPoint.X, OriginalY = dataPoint.OriginalY, MaxY = maxQueue.First.Value, AvgSmoothedY = dataPoint.AvgSmoothedY, ExpSmoothedY = dataPoint.ExpSmoothedY };
            }
        }
    }
}
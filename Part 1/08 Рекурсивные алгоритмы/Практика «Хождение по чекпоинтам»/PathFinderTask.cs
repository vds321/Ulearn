using System;
using System.Drawing;

namespace RoutePlanning
{
    public static class PathFinderTask
    {
        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
        {
            var bestpath = new int[checkpoints.Length];
            var path = new int[checkpoints.Length];
            var bestDistance = Double.MaxValue;
            MakeTrivialPermutation(checkpoints, checkpoints.Length, path, bestpath, bestDistance);

            return bestpath;
        }
		
        private static double MakeTrivialPermutation(Point[] checkpoints, int size, int[] path, int[] bestpath, double bestDistance, int position = 1)
        {
            if (position == path.Length && bestDistance > checkpoints.GetPathLength(path))
            {
                bestDistance = checkpoints.GetPathLength(path);
                Array.Copy(path, bestpath, checkpoints.Length);
                return bestDistance;
            }
            for (int i = 0; i < size; i++)
            {
                bool found = false;
                for (int j = 0; j < position; j++)
                {
                    if (path[j] == i)
                    {
                        found = true;
                        break;
                    }
                }
                if (found) continue;
                path[position] = i;
                if (ToClipPermutation(checkpoints, path, position, bestDistance)) return bestDistance;
                bestDistance = MakeTrivialPermutation(checkpoints, size, path, bestpath, bestDistance, position + 1);
            }
            return bestDistance;
        }
		
        public static bool ToClipPermutation(Point[] checkpoints, int[] path, int position, double bestDistance)
        {
            var tempPath = new int[position + 1];
            Array.Copy(path, tempPath, position + 1);
            var tempLen = checkpoints.GetPathLength(tempPath);
            return tempLen >= bestDistance;
        }
    }
}

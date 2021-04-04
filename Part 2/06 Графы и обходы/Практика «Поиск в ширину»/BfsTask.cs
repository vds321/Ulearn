using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Dungeon
{
    public class BfsTask
    {
        public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
        {
            var chestsPoint = chests.ToHashSet();
            var queue = new Queue<SinglyLinkedList<Point>>();
            var visited = new HashSet<Point>();

            queue.Enqueue(new SinglyLinkedList<Point>(start));
            visited.Add(start);
            while (queue.Count != 0)
            {
                var wayToPoint = queue.Dequeue();
                var point = wayToPoint.First();
                if (point.X < 0 || point.X >= map.Dungeon.GetLength(0) || point.Y < 0 || point.Y >= map.Dungeon.GetLength(1) ||
                    map.Dungeon[point.X, point.Y] != MapCell.Empty) continue;
                 if (chestsPoint.Contains(point)) yield return wayToPoint;
                foreach (var pospoint in Walker.PossibleDirections)
                {
                    var nextPoint = new Point { X = point.X + pospoint.Width, Y = point.Y + pospoint.Height };
                    if (visited.Contains(nextPoint)) continue;
                    queue.Enqueue(new SinglyLinkedList<Point>(nextPoint, wayToPoint));
                    visited.Add(nextPoint);
                }
            }
        }
    }
}
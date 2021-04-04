using System.Collections.Generic;
using System.Drawing;

namespace Rivals
{
    public class RivalsTask
    {
        public static IEnumerable<OwnedLocation> AssignOwners(Map map)
        {
            var queue = new Queue<OwnedLocation>();
            var visited = new HashSet<Point>();
            for (int owner = 0; owner < map.Players.Length; owner++)
            {
                var startOwner = new OwnedLocation(owner, map.Players[owner], 0);
                queue.Enqueue(startOwner);
                visited.Add(startOwner.Location);
            }
            while (queue.Count != 0)
            {
                var ownerPoint = queue.Dequeue();
                yield return ownerPoint;
                FindNextPoint(map, ownerPoint, visited, queue);
            }
        }
        private static void FindNextPoint(Map map, OwnedLocation ownerPoint, HashSet<Point> visited, Queue<OwnedLocation> queue)
        {
            for (var dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    var nextPoint = new Point(ownerPoint.Location.X + dx, ownerPoint.Location.Y + dy);
                    if (dx != 0 && dy != 0 || visited.Contains(nextPoint) || !map.InBounds(nextPoint) ||
                        map.Maze[nextPoint.X, nextPoint.Y] != MapCell.Empty) continue;
                    queue.Enqueue(new OwnedLocation(ownerPoint.Owner, nextPoint, ownerPoint.Distance + 1));
                    visited.Add(nextPoint);
                }
            }
        }
    }
}
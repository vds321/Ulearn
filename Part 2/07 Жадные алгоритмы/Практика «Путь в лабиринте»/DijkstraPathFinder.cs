using Greedy.Architecture;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Greedy
{
    public class DijkstraPathFinder
    {
        public IEnumerable<PathWithCost> GetPathsByDijkstra(State state, Point start,
            IEnumerable<Point> targets)
        {
            var chestsList = targets.ToHashSet();
            var visited = new HashSet<Point>();
            var track = new Dictionary<Point, DijkstraData>
            {
                [start] = new DijkstraData { Price = 0, Previous = new Point(-1, -1) }
            };

            while (chestsList.Count() != 0)
            {
                var toOpen = new Point(-1, -1);
                var bestPrice = double.PositiveInfinity;
                foreach (var e in track.Keys.Where(point => !visited.Contains(point) && track[point].Price < bestPrice))
                {
                    toOpen = e;
                    bestPrice = track[e].Price;
                }
                if (toOpen.X == -1) break;
                if (chestsList.Contains(toOpen))
                {
                    yield return GetPathWithCost(track, toOpen);
                    chestsList.Remove(toOpen);
                }
                GetNextPoint(state, track, toOpen);
                visited.Add(toOpen);
            }

        }
        private PathWithCost GetPathWithCost(Dictionary<Point, DijkstraData> track, Point target)
        {
            var result = new List<Point>();
            var cost = track[target].Price;
            while (target.X != -1)
            {
                result.Add(target);
                target = track[target].Previous;
            }
            result.Reverse();
            return new PathWithCost(cost, result.ToArray());
        }
        private void GetNextPoint(State state, Dictionary<Point, DijkstraData> track, Point toOpen)
        {
            List<int> delta = new List<int>() { -1, 0, 1 };
            var nextPointList = (from x in delta
                                 from y in delta
                                 where Math.Abs(x) != Math.Abs(y)
                                 select new Point(toOpen.X + x, toOpen.Y + y))
                                .Where(point => state.InsideMap(point) && !state.IsWallAt(point)).ToList();
            foreach (var point in nextPointList)
            {
                var currentPrice = track[toOpen].Price + state.CellCost[point.X, point.Y];
                if (!track.ContainsKey(point) || track[point].Price > currentPrice)
                {
                    track[point] = new DijkstraData { Previous = toOpen, Price = currentPrice };
                }
            }
        }
    }
    class DijkstraData
    {
        public int Price { get; set; }
        public Point Previous { get; set; }
    }
}
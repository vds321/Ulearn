using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Dungeon
{
    public class DungeonTask
    {
        public static MoveDirection[] FindShortestPath(Map map)
        {
            var start = map.InitialPosition;
            var exit = map.Exit;
            var chests = map.Chests;
            var pathFromStartToChest = BfsTask.FindPaths(map, start, chests);
            var pathFromExitToChest = BfsTask.FindPaths(map, exit, chests);
            var pathFromSatrtToExitThrowChest = pathFromStartToChest.Join(pathFromExitToChest,
                path1 => path1.Value, path2 => path2.Value, (path1, path2) => Tuple.Create(path1, path2));
            if (!pathFromSatrtToExitThrowChest.Any())
            {
                var pathFromStartToExit = BfsTask.FindPaths(map, start, new[] { map.Exit }).FirstOrDefault();
                return (pathFromStartToExit == null) ? new MoveDirection[0] : ConvertPathToDirection(pathFromStartToExit.Reverse().ToList());
            }
            var shortestPath = pathFromSatrtToExitThrowChest.OrderBy(path => path.Item1.Count() + path.Item2.Count()).First();
            var shortestPathList = shortestPath.Item1.Reverse().Concat(shortestPath.Item2.Skip(1)).ToList();
            return ConvertPathToDirection(shortestPathList);

        }
        private static MoveDirection[] ConvertPathToDirection(List<Point> pathList)
        {
            return pathList.Zip(pathList.Skip(1), (current, next) => Walker.ConvertOffsetToDirection(new Size(next) - new Size(current))).ToArray();
        }
    }
}
using Greedy.Architecture;
using Greedy.Architecture.Drawing;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Greedy
{
    public class GreedyPathFinder : IPathFinder
    {
        public List<Point> FindPathToCompleteGoal(State state)
        {
            var finalFindedPath = new List<Point>();
            var startPoint = state.Position;
            var currentEnegry = state.InitialEnergy;
            var targets = state.Chests.ToHashSet();
            if (targets.Count() < state.Goal) return new List<Point>();
            if (targets.Contains(startPoint))
            {
                state.Scores++;
                targets.Remove(startPoint);
            }
            while (state.Scores < state.Goal)
            {
                var finderPathClass = new DijkstraPathFinder();
                var allPathsToTargets = finderPathClass.GetPathsByDijkstra(state, startPoint, targets);
                if (allPathsToTargets.FirstOrDefault() == null) return new List<Point>();
                var easyPathWithCost = allPathsToTargets.First();
                var energyToEasyPath = easyPathWithCost.Cost;
                var easyPath = easyPathWithCost.Path.Skip(1);
                if (currentEnegry < energyToEasyPath) return new List<Point>();
                foreach (var point in easyPath)
                {
                    finalFindedPath.Add(point);
                }
                var lastPoint = easyPath.Last();
                startPoint = lastPoint;
                currentEnegry -= energyToEasyPath;
                targets.Remove(lastPoint);
                state.Scores++;
            }
            return finalFindedPath;
        }
    }
}
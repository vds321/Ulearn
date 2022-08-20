using System;
using System.Collections.Generic;
using System.Linq;

namespace Delegates.PairsAnalysis
{
    public static class Analysis
    {
        public static int FindMaxPeriodIndex(params DateTime[] data) =>
            data.Pairs().Select(x => (x.Item2 - x.Item1).TotalSeconds).MaxIndex();

        public static double FindAverageRelativeDifference(params double[] data) =>
            data.Pairs().Select(x => (x.Item2 - x.Item1) / x.Item1).Average();
    }

    public static class Extensions
    {
        public static IEnumerable<Tuple<T, T>> Pairs<T>(this IEnumerable<T> data) where T : struct
        {
            T? next = null;
            foreach (var current in data)
            {
                if (!next.Equals(null))
                {
                    if (next != null) yield return new Tuple<T, T>((T)next, current);
                }
                next = current;
            }
        }

        public static int MaxIndex<T>(this IEnumerable<T> data)
        {
            var array = data.ToArray();
            var maxElement = array.Max();
            return Array.IndexOf(array, maxElement);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
    public static class ExtensionsTask
    {
        public static double Median(this IEnumerable<double> items)
        {
            var count = 0;
            var enumerable = items.ToArray();
            foreach (var item in enumerable) count++;
            if (count == 0) throw new InvalidOperationException();
            return count % 2 != 0 ? enumerable.OrderBy(x => x).ElementAt(count / 2) : enumerable.OrderBy(x => x).Skip((count / 2) - 1).Take(2).Aggregate((x, y) => (x + y) / 2);
        }

        public static IEnumerable<Tuple<T, T>> Bigrams<T>(this IEnumerable<T> items)
        {
            var previous = default(T);
            bool isfirstElement = true;
            foreach (var item in items)
            {
                if (isfirstElement)
                {
                    previous = item;
                    isfirstElement = false;
                    continue;
                }
                yield return Tuple.Create<T, T>(previous, item);
                previous = item;
            }
        }
    }
}
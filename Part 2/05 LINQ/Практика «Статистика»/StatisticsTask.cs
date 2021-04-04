using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
    public class StatisticsTask
    {
        public static double TimeFromSameSlide = 0.0;
        public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
        {
            if (visits.Count == 0) return 0;
            var a = visits.OrderBy(visit => visit.UserId)
                          .ThenBy(x => x.DateTime)
                          .Bigrams();
            var b = a.Where(bigramm => bigramm.Item1.SlideType.Equals(slideType))
                          .Select(bigramm => GetTime(bigramm));
            var timeAll = b.Where(time => time <= 120 && time >= 1);

            if (timeAll.Count() == 0) return 0;
            return timeAll.Median();
        }
        private static double GetTime(Tuple<VisitRecord, VisitRecord> tuple)
        {
            if (tuple.Item1.UserId.Equals(tuple.Item2.UserId))
            {
                if (!tuple.Item1.SlideId.Equals(tuple.Item2.SlideId))
                {
                    var tempTime = TimeFromSameSlide;
                    TimeFromSameSlide = 0.0;
                    return tuple.Item2.DateTime.Subtract(tuple.Item1.DateTime).TotalMinutes + tempTime;
                }
                else
                {
                    TimeFromSameSlide += tuple.Item2.DateTime.Subtract(tuple.Item1.DateTime).TotalMinutes;
                }
                return 0;
            }
            else
            {
                TimeFromSameSlide = 0.0;
                return 0;
            }
        }
    }
}
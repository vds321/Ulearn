using System;

namespace DistanceTask
{
    public static class DistanceTask
    {
        public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
        {
            double[] dotA = { ax, ay };
            double[] dotB = { bx, by };
            double[] dotX = { x, y };
            if (Distance(dotA, dotB) == 0) return Distance(dotA, dotX);
            else if (ScalyarMulti(DotsToVector(dotX, dotA), DotsToVector(dotX, dotB)) <= 0 && ObliqueMulti(DotsToVector(dotA, dotB), DotsToVector(dotA, dotX)) == 0) return 0.0;
            else if (ScalyarMulti(DotsToVector(dotA, dotX), DotsToVector(dotA, dotB)) >= 0 && ScalyarMulti(DotsToVector(dotB, dotX), DotsToVector(dotB, dotA)) >= 0)
            {
                return Math.Round(Math.Abs((ObliqueMulti(DotsToVector(dotA, dotB),
                    DotsToVector(dotA, dotX)))) / Math.Sqrt(Math.Pow((bx - ax), 2) + Math.Pow((by - ay), 2.0)), 4);
            }
            else
            {
                return Math.Round(Math.Min(Distance(dotA, dotX), Distance(dotB, dotX)), 4);
            }
        }
		
        private static double Distance(double[] dotOne, double[] dotTwo)
        {
            return Math.Sqrt((Math.Pow((dotOne[0] - dotTwo[0]), 2) + Math.Pow((dotOne[1] - dotTwo[1]), 2)));
        }
		
        private static double[] DotsToVector(double[] dotOne, double[] dotTwo)
        {
            double[] vectorCoordinats = { dotTwo[0] - dotOne[0], dotTwo[1] - dotOne[1] };
            return vectorCoordinats;
        }
		
        private static double ScalyarMulti(double[] vectorOne, double[] vectorTwo)
        {
            return (vectorOne[0] * vectorTwo[0] + vectorOne[1] * vectorTwo[1]);
        }
		
        private static double ObliqueMulti(double[] vectorOne, double[] vectorTwo)
        {
            return (vectorOne[0] * vectorTwo[1] - vectorOne[1] * vectorTwo[0]);
        }
    }
}
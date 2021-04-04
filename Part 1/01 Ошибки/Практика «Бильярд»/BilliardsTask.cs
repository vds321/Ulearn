namespace Billiards
{
    public static class BilliardsTask
    {
        public static double BounceWall(double directionRadians, double wallInclinationRadians)
        {
            double angle = 2.0 * wallInclinationRadians - directionRadians;
            return angle;
        }
    }
}
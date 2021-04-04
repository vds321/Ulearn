using System;

namespace AngryBirds
{
	public static class AngryBirdsTask
	{
		public static double FindSightAngle(double v, double distance)
		{
			double G = 9.8;
			double sinAngle = (distance * G) / (v * v);
			return Math.Asin(sinAngle) / 2.0;
		}
	}
}
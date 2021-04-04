using System;

namespace GeometryTasks
{
    class Vector
    {
        public double X;
        public double Y;
    }
	
    class Segment
    {
        public Vector Begin;
        public Vector End;
    }
	
    class Geometry
    {
        public static double GetLength(Vector vector)
        {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }
		
        public static Vector Add(Vector vector1, Vector vector2)
        {
            return new Vector
            {
                X = vector1.X + vector2.X,
                Y = vector1.Y + vector2.Y
            };
        }
		
        public static double GetLength(Segment segment)
        {
            return Math.Sqrt((segment.End.X - segment.Begin.X) * (segment.End.X - segment.Begin.X) + (segment.End.Y - segment.Begin.Y) * (segment.End.Y - segment.Begin.Y));
        }
		
        public static Vector Difference(Vector vector1, Vector vector2)
        {
            return new Vector
            {
                X = vector2.X - vector1.X,
                Y = vector2.Y - vector1.Y
            };
        }
		
        public static bool IsVectorInSegment(Vector vector, Segment segment)
        {
            var xToBegin = Difference(vector, segment.Begin);
            var xToEnd = Difference(vector, segment.End);
            var beginToEnd = Difference(segment.Begin, segment.End); ;
            var beginToX = Difference(segment.Begin, vector);
            var scalyar = xToBegin.X * xToEnd.X + xToBegin.Y * xToEnd.Y;
            var oblique = beginToEnd.X * beginToX.Y - beginToEnd.Y * beginToX.X;
            return (scalyar <= 0 && oblique == 0);
        }
    }
}
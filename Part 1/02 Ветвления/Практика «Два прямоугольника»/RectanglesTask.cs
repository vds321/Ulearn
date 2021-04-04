using System;

namespace Rectangles
{
    public static class RectanglesTask
    {
        public static bool AreIntersected(Rectangle r1, Rectangle r2)
        {
            return !(r1.Top > r2.Bottom || r1.Bottom < r2.Top || r1.Right < r2.Left ||
                     r1.Left > r2.Right);
        }

        public static int IntersectionSquare(Rectangle r1, Rectangle r2)
        {
            if (!AreIntersected(r1, r2)) return 0;
            return (Math.Min(r1.Right, r2.Right) - Math.Max(r1.Left, r2.Left)) *
                   (Math.Min(r1.Bottom, r2.Bottom) - Math.Max(r1.Top, r2.Top));
        }

        public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
        {
            if (CompareSizeRectangle(r1, r2)) return 1;
            if (CompareSizeRectangle(r2, r1)) return 0;
            return -1;
        }
		
        private static bool CompareSizeRectangle(Rectangle r1, Rectangle r2)
        {
            return (r1.Bottom >= r2.Bottom && r2.Top >= r1.Top && r1.Right >= r2.Right &&
                r2.Left >= r1.Left);
        }
    }
}
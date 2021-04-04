using GeometryTasks;
using System.Collections.Generic;
using System.Drawing;

namespace GeometryPainting
{
    static public class Helper
    {
        static Dictionary<Segment, Color> colorDict = new Dictionary<Segment, Color>();
        public static void SetColor(this Segment segment, Color color)
        {
             colorDict[segment] = color;
        }
        public static Color GetColor(this Segment segment)
        {
            if (colorDict.ContainsKey(segment))
            {
                return colorDict[segment];
            }
            else return Color.Black;
        }
    }
}
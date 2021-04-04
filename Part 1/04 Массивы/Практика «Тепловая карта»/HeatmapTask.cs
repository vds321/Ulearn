using System.Linq;

namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var daysX = new string[30];
            var monthsY = new string[12];
            var birthCount = new double[30, 12];

            var selectedNames = names.Where(n => n.BirthDate.Day != 1);
            foreach (var name in selectedNames)
            {
                birthCount[name.BirthDate.Day - 2,name.BirthDate.Month - 1]++;
            }
            return new HeatmapData("Heat Map", birthCount, MakeCoordinatMassive(daysX, 2), MakeCoordinatMassive(monthsY, 1));
        }
		
        private static string[] MakeCoordinatMassive(string[] coordinat, int delta)
        {
            for (int i = 0; i < coordinat.Length; i++) coordinat[i] = (i + delta).ToString();
            return coordinat;
        }
    }
}
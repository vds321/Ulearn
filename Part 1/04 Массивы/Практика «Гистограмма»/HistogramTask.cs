namespace Names
{
	internal static class HistogramTask
	{
		public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            var minDay = 1;
            var maxday = 31;
            var days = new string[maxday - minDay + 1];
            for (int i = 0; i < days.Length; i++)
            {
                days[i] = (i + 1).ToString();
            }
            var birthCounter = new double[maxday - minDay + 1];
            foreach (var item in names)
            {
                if (item.BirthDate.Day == 1) birthCounter[0] = 0.0;
                if (item.Name == name) birthCounter[item.BirthDate.Day - 1]++;
            }
            return new HistogramData(string.Format("Рождаемость людей с именем '{0}'", name),
                days,
                birthCounter);
        }
    }
}
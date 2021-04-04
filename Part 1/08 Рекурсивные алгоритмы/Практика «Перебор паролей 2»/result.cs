public class CaseAlternatorTask
    {
        public static List<string> AlternateCharCases(string lowercaseWord)
        {
            var result = new List<string>();
            AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
            var passwords = result.OrderByDescending(item => item, StringComparer.Ordinal);
            var sortedResult = new List<string>();
            foreach (var item in passwords)
            {
                sortedResult.Add(item);
            }
            return sortedResult;
        }
	
        static void AlternateCharCases(char[] word, int startIndex, List<string> result)
        {
            while (startIndex < word.Length && (!char.IsLetter(word[startIndex]) || char.ToUpper(word[startIndex]) == char.ToLower(word[startIndex])))
            {
                startIndex++;
            }
            if (startIndex == word.Length)
            {
                 result.Add(new string(word));
            }
            else
            {
                word[startIndex] = char.ToUpper(word[startIndex]);
                AlternateCharCases(word, startIndex + 1, result);
                word[startIndex] = char.ToLower(word[startIndex]);
                AlternateCharCases(word, startIndex + 1, result);
            }
        }
    }   
using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    internal static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var unSortedtDict = new Dictionary<string, Dictionary<string, int>>();
            for (int index = 0; index < text.Count; index++)
            {
                if (text[index].Count == 1) text.RemoveAt(index);
            }
            unSortedtDict = AddingBeTriGramm(unSortedtDict, text);
            var sortedDictionary = ToSortDictionary(unSortedtDict);
            return sortedDictionary;
        }

        private static Dictionary<string, Dictionary<string, int>> AddingBeTriGramm(Dictionary<string, Dictionary<string, int>> unsorteddict, List<List<string>> text)
        {
            foreach (var line in text)
            {
                for (int count = 1; count <= 2; count++)
                {
                    for (int word = 0; word < line.Count - count; word++)
                    {
                        string keyWord, valueWord;
                        if (count == 1) { keyWord = line[word]; valueWord = line[word + 1]; }
                        else { keyWord = line[word] + " " + line[word + 1]; valueWord = line[word + 2]; }
                        if (!unsorteddict.ContainsKey(keyWord)) unsorteddict[keyWord] = new Dictionary<string, int> { { valueWord, 1 } };
                        else
                        {
                            if (unsorteddict[keyWord].ContainsKey(valueWord)) unsorteddict[keyWord][valueWord]++;
                            else unsorteddict[keyWord][valueWord] = 1;
                        }
                    }
                }
            }
            return unsorteddict;
        }

        public static Dictionary<string, string> ToSortDictionary(Dictionary<string, Dictionary<string, int>> dictionary)
        {
            var result = new Dictionary<string, string>();
            foreach (var item in dictionary)
            {
                var sortedWord = item.Value.OrderByDescending(val => val.Value)
                                           .ThenBy(key => key.Key, StringComparer.Ordinal);
                result.Add(item.Key, sortedWord.First().Key);
            }
            return result;
        }
    }
}
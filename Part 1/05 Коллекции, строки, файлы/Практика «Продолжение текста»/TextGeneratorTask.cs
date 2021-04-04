using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(Dictionary<string, string> nextWords, string phraseBeginning,
            int wordsCount)
        {
            if (nextWords.Count == 0 || phraseBeginning == "" || wordsCount == 0) return phraseBeginning;
            for (int i = 0; i < wordsCount; i++)
            {
                var startWord = phraseBeginning.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                string wordToSearch;
                if (startWord.Count >= 2)
                {
                    startWord = new List<string> { startWord[startWord.Count - 2], startWord[startWord.Count - 1] };
                    wordToSearch = String.Join(" ", startWord);
                }
                else
                {
                    startWord = new List<string> { startWord[startWord.Count - 1] };
                    wordToSearch = startWord[0].ToString();
                }
                if (nextWords.Keys.Contains(wordToSearch)) phraseBeginning += " " + nextWords[wordToSearch];
                else
                {
                    wordToSearch = startWord[startWord.Count - 1].ToString();
                    if (nextWords.Keys.Contains(wordToSearch)) phraseBeginning += " " + nextWords[wordToSearch];
                    else break;
                }
            }
            return phraseBeginning;
        }
    }
}
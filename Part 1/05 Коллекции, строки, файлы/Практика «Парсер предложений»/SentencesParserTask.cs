using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            var lineBorder = new char[] { '.', '!', '?', ';', ':', '(', ')' };
            List<string> listLines = text.Split(lineBorder, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var line in listLines)
            {
                var word = "";
                var wordList = new List<string>();
                
                foreach (var itemChar in line)
                {
                    if (char.IsLetter(itemChar) || itemChar == '\'') word += itemChar;
                    else
                    {
                        if (word != "") wordList.Add(word.ToLower());
                        word = "";
                    }
                }
                if (word != "") wordList.Add(word.ToLower());
                if (wordList.Count != 0) sentencesList.Add(wordList);
            }
            return sentencesList;
        }
    }
}
using NUnit.Framework;
using System.Collections.Generic;

namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        public static void Test(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i].Value);
            }
        }
        [TestCase(@"'a'", new[] { "a" })]
        [TestCase(@"'a' ", new[] { "a" })]
        [TestCase(@"'a'  ", new[] { "a" })]
        [TestCase(@"'a''b'", new[] { "a", "b" })]
        [TestCase(@"''", new[] { "" })]
        [TestCase(@"b'a'", new[] { "b", "a" })]
        [TestCase(@"'a\\'", new[] { "a\\" })]
        [TestCase(@"'\\'", new[] { "\\" })]
        [TestCase("\"'\"", new[] { "'" })]
        [TestCase("'\"'", new[] { "\"" })]
        [TestCase("'a", new[] { "a" })]
        [TestCase("'a'b", new[] { "a", "b" })]
        [TestCase(@" 'a'", new[] { "a" })]
        [TestCase(@"' '", new[] { " " })]
        [TestCase(@"'a ", new[] { "a " })]
        [TestCase(@"'\''", new[] { "\'" })]
        [TestCase("\"\\\"\\\"\"", new[] { "\"\"" })]
        [TestCase("", new string[0])]
        public static void RunTests(string input, string[] expectedOutput)
        {
            Test(input, expectedOutput);
        }
    }

    public class FieldsParserTask
    {
        public static List<Token> ParseLine(string line)
        {
            var fields = new List<Token> { };
            int nextPosition = 0;
            for (int i = 0; i < line.Length;)
            {
                if (line[i] == ' ')
                {
                    i++;
                }
                else if (line[i] == '"' || line[i] == '\'')
                {
                    var token = ReadQuotedField(line, i);
                    nextPosition = token.GetIndexNextToToken();
                    fields.Add(token);
                    i = nextPosition;
                }
                else
                {
                    var token = ReadField(line, i);
                    nextPosition = token.GetIndexNextToToken();
                    fields.Add(token);
                    i = nextPosition;
                }
            }
            return fields;
        }

        private static Token ReadField(string line, int startIndex)
        {
            var subLine = "";
            for (var j = startIndex; j < line.Length; j++)
            {
                if (line[j] != '"' && line[j] != '\'' && line[j] != ' ')
                {
                    subLine += line[j];
                }
                else break;
            }
            return new Token(subLine, startIndex, subLine.Length);
        }
		
        public static Token ReadQuotedField(string line, int startIndex)
        {
            return QuotedFieldTask.ReadQuotedField(line, startIndex);
        }
    }
}
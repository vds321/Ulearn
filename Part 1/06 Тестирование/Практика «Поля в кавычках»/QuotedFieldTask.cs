using NUnit.Framework;
using System.Linq;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("", 0, "", 1)]
        [TestCase("'a''b'", 0, "a", 3)]
        [TestCase("'a' 'b'", 0, "a", 3)]
        [TestCase("'a'  'b'", 0, "a", 3)]
        [TestCase("'a'", 0, "a", 3)]
        [TestCase(" 'a'", 1, "a", 3)]
        [TestCase(" ", 0, "", 1)]
        [TestCase("a'b'", 1, "b", 3)]
        [TestCase("'\"'", 0, "\"", 3)]
        [TestCase("'a", 0, "a", 2)]
        [TestCase("'a'b", 0, "a", 3)]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }
    }
	
    internal class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            var simbolArray = line.ToList();
            var newLine = "";
            var newLineLength = 1;
            var index = startIndex + 1;
            while (index < simbolArray.Count)
            {
                if (simbolArray[index] == simbolArray[startIndex])
                {
                    newLineLength++;
                    break;
                }
                else if (simbolArray[index] == '\\')
                {
                    newLine += simbolArray[index + 1];
                    index++;
                    newLineLength += 2;
                }
                else
                {
                    newLine += simbolArray[index];
                    newLineLength++;
                }
				index++;
            }
            return new Token(newLine, startIndex, newLineLength);
        }
    }
}
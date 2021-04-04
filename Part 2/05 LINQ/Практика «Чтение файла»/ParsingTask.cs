using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
    public class ParsingTask
    {
        public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
        {
            IDictionary<int, SlideRecord> result = lines.Skip(1).Select(x => x.Split(';'))
                               .Where(x => GetParsedLine(x) != null)
                               .Select(x => GetParsedLine(x))
                               .ToDictionary(x => x.SlideId, x => x);
            return result;
        }
        static private SlideRecord GetParsedLine(string[] line)
        {
            if (line.Length == 3 && int.TryParse(line[0], out int slideId) && Enum.TryParse(line[1], true, out SlideType slideType))
            {
                return new SlideRecord(slideId, slideType, line[2]);
            }
            else return null;
        }

        public static IEnumerable<VisitRecord> ParseVisitRecords(
            IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
        {

            IEnumerable<VisitRecord> visitRecords = lines.Skip(1).Select(x => x.Split(';'))
                           .Select(x => GetParsedVisitor(x, slides)).ToList();
            return visitRecords;
        }
        static private VisitRecord GetParsedVisitor(string[] line, IDictionary<int, SlideRecord> slideRecord)
        {
            if (line.Length == 4 && int.TryParse(line[0], out int userID) &&
                int.TryParse(line[1], out int slideId) && slideRecord.ContainsKey(slideId)
                && DateTime.TryParse($"{line[2]} {line[3]}", out DateTime dateTime))
            {
                return new VisitRecord(userID, slideId, dateTime, slideRecord[slideId].SlideType);
            }
            else throw new FormatException($"Wrong line [{line.Aggregate((a, b) => a + ';' + b)}]");
        }
    }
}
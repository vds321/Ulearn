using System;
using System.Collections.Generic;

namespace Autocomplete
{
    public class RightBorderTask
    {
        public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            left++;
            right--;
			if (prefix == "") return phrases.Count;
			if (phrases.Count == 0) return 0;
            while (left < right)
            {
                var middle = left + (right - left) / 2;
                if (string.Compare(prefix, phrases[middle], StringComparison.OrdinalIgnoreCase) > 0
                      || phrases[middle].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    left = middle + 1;
                else right = middle;
            }
            if (string.Compare(prefix, phrases[left], StringComparison.OrdinalIgnoreCase) >= 0
			   || phrases[left].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return right + 1;
            return left;
        }
    }
}
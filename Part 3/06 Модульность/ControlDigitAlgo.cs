using System;
using System.Linq;

namespace SRP.ControlDigit
{
    public static class Extensions
    {
        public static int[] GetDigitsFromNumber(this long number)
        {
            var length = number == 0 ? 1 : (int)Math.Floor(Math.Log10(number)) + 1;
            var digits = new int[length];
            for (int i = 0; i < length; i++)
            {
                var digit = (int)(number % 10);
                number /= 10;
                digits[i] = digit;
            }
            return digits;
        }

        public static int SumDigitsFromOddPosition(this int[] array)
        {
            return array.Where((t, i) => (i + 1) % 2 != 0).Sum();
        }

        public static int SumDigitsFromEvenPosition(this int[] array)
        {
            return array.Where((t, i) => (i + 1) % 2 == 0).Sum();
        }
    }

    public static class ControlDigitAlgo
    {
        public static int Upc(long number)
        {
            var digits = number.GetDigitsFromNumber();
            int sumEven = digits.SumDigitsFromEvenPosition();
            int sumOdd = digits.SumDigitsFromOddPosition();
            int result = (sumEven + sumOdd * 3) % 10;
            if (result != 0) result = 10 - result;
            return result;
        }

        public static char Isbn10(long number)
        {
            var digits = number.GetDigitsFromNumber();
            int sum = digits.Select((t, i) => t * (i + 2)).Sum();
            var result = (11 - sum % 11) % 11;
            return result == 10 ? 'X' : result.ToString()[0];
        }

        public static int Luhn(long number)
        {
            var digitsArray = number.GetDigitsFromNumber();
            var sum = SumDigitsLuhnAlgoritm(digitsArray);
            return (sum * 9) % 10;
        }

        private static int SumDigitsLuhnAlgoritm(int[] array)
        {
            var sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                var digit = array[i];
                if ((i - 1) % 2 != 0)
                {
                    var check_number = digit * 2;
                    digit = check_number > 9 ? check_number - 9 : check_number;
                }
                sum += digit;
            }
            return sum;
        }
    }
}
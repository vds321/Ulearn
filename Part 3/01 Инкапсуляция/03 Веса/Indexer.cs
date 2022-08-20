using System;

namespace Incapsulation.Weights
{
    public class Indexer
    {
        private readonly double[] _array;
        private readonly int _start;

        public Indexer(double[] array, int start, int length)
        {
            if (array == null)
            {
                throw new ArgumentException(nameof(array));
            }
            if (start < 0 || start > array.Length)
            {
                throw new ArgumentException(nameof(start));
            }
            if (length < 0 || length > array.Length - start)
            {
                throw new ArgumentException(nameof(length));
            }

            _array = array;
            _start = start;
            Length = length;
        }

        public int Length { get; }

        public double this[int index]
        {
            get
            {
                if (index < 0 || index >= Length) throw new IndexOutOfRangeException();
                return _array[_start + index];
            }
            set
            {
                if (index < 0 || index >= Length) throw new IndexOutOfRangeException();
                _array[_start + index] = value;
            }
        }
    }
}

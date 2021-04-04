using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace hashes
{
    public class ReadonlyBytes : IEnumerable<byte>
    {
        private byte[] Array;
        private int Hash;

        public ReadonlyBytes(params byte[] args)
        {
            if (args == null) throw new ArgumentNullException();
            else
            {
                Array = args;
                Hash = GetHash();
            }
        }
        public int Length => Array.Length;

        private int GetHash()
        {
            int hash = 1;
            foreach (var item in Array)
            {
                unchecked
                {
                    hash *= 139081;
                }
                hash ^= item.GetHashCode();
            }
            return hash;
        }

        public byte this[int index]
        {
            get
            {
                if (index >= Array.Length || index < 0) throw new IndexOutOfRangeException();
                return this.ElementAt(index);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || (obj.GetType() != GetType()) || GetHashCode() != obj.GetHashCode()) return false;
            for (int i = 0; i < Length; i++)
            {
                if (this[i] != (obj as ReadonlyBytes)[i]) return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return Hash;
        }

        public override string ToString()
        {
            string result = string.Join(", ", Array.Select(item => item.ToString()));
            return $"[{result}]";
        }

        public IEnumerator<byte> GetEnumerator()
        {
            for (int i = 0; i < Array.Length; i++)
            {
                yield return Array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
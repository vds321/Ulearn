using System;
using System.Collections.Generic;
using System.Linq;

namespace TodoApplication
{
    public class LimitedSizeStack<T>
    {
        private int size;
        LinkedList<T> itemsList = new LinkedList<T>();
        public LimitedSizeStack(int limit)
        {
            size = limit;
        }

        public void Push(T item)
        {
            if (itemsList.Count == size)
            {
                itemsList.RemoveFirst();
            }
            itemsList.AddLast(item);
        }

        public T Pop()
        {
            if (itemsList.Count == 0) throw new InvalidOperationException();
            var rezult = itemsList.Last.Value;
            itemsList.RemoveLast();
            return rezult;
        }

        public int Count
        {
            get
            {
                return itemsList.Count();
            }
        }
    }
}

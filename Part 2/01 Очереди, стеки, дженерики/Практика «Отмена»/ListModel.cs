using System.Collections.Generic;

namespace TodoApplication
{
    public class ListModel<TItem>
    {
        enum ActionEx
        {
            Add,
            Remove
        }
        private struct ExecuteList
        {
            public ActionEx action;
            public TItem Item;
            public int Index;
        }
        public List<TItem> Items { get; }
        public int Limit;
        private LimitedSizeStack<ExecuteList> executeList;

        public ListModel(int limit)
        {
            Items = new List<TItem>();
            Limit = limit;
            executeList = new LimitedSizeStack<ExecuteList>(limit);
        }

        public void AddItem(TItem item)
        {
            Items.Add(item);
            executeList.Push(new ExecuteList { Index = Items.Count - 1, action = ActionEx.Add, Item = item });
        }

        public void RemoveItem(int index)
        {
            executeList.Push(new ExecuteList { Index = index, action = ActionEx.Remove, Item = Items[index] });
            Items.RemoveAt(index);
        }

        public bool CanUndo()
        {
            return executeList.Count != 0;
        }

        public void Undo()
        {
            if (executeList.Count != 0)
            {
                var result = executeList.Pop();
                if (result.action == ActionEx.Add)
                {
                    Items.RemoveAt(result.Index);
                }
                else if (result.action == ActionEx.Remove)
                {
                    Items.Insert(result.Index, result.Item);
                }
            }
        }
    }
}
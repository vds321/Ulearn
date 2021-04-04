using System;
using System.Collections.Generic;

namespace Clones
{
	public class MyNode<T>
	{
		public MyNode<T> Previous;
		public T Value;
		public MyNode(MyNode<T> previous, T value)
		{
			Previous = previous;
			Value = value;
		}
	}
	public class MyStack<T>
	{
		private MyNode<T> last;
		public int Count { get; private set; }
		public MyNode<T> Last
		{
			get
			{
				if (last != null) return last;
				else return null;
			}
		}
		public MyStack()
		{
			Count = 0;
			last = null;
		}
		public MyStack(int size, MyNode<T> last)
		{
			Count = size;
			this.last = last;
		}
		public T Pop()
		{
			MyNode<T> result = last;
			last = last.Previous;
			Count--;
			return result.Value;
		}
		public void Push(T item)
		{
			MyNode<T> myNode = new MyNode<T>(last, item);
			myNode.Previous = last;
			last = myNode;
			Count++;
		}
		public void Clear()
		{
			last = null;
		}
		public T Peek()
		{
			if (last == null) throw new InvalidOperationException();
			else return last.Value;
		}
	}
	public class Clone
	{
		private MyStack<string> learningPrograms;
		private MyStack<string> history;
		public Clone()
		{
			learningPrograms = new MyStack<string>();
			history = new MyStack<string>();
		}
		public Clone(Clone clone)
		{
			learningPrograms = new MyStack<string>(clone.learningPrograms.Count, clone.learningPrograms.Last);
			history = new MyStack<string>(clone.history.Count, clone.history.Last);
		}
		public void Learn(string program)
		{
			learningPrograms.Push(program);
			history.Clear();
		}
		public void Rollback()
		{
			var result = learningPrograms.Pop();
			history.Push(result);
		}
		public void Relearn()
		{
			var result = history.Pop();
			learningPrograms.Push(result);
		}
		public string Check()
		{
			if (learningPrograms.Last == null) return "basic";
			return learningPrograms.Last.Value;
		}
	}
	public class CloneVersionSystem : ICloneVersionSystem
	{
		List<Clone> clones = new List<Clone>() { new Clone() };

		public string Execute(string query)
		{
			string[] commandList = query.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			string commandClone = commandList[0];
			int numberClone = Int32.Parse(commandList[1]) - 1;
			if (commandClone == "learn")
			{
				clones[numberClone].Learn(commandList[2]);
				return null;
			}
			else if (commandClone == "rollback")
			{
				clones[numberClone].Rollback();
				return null;
			}
			else if (commandClone == "relearn")
			{
				clones[numberClone].Relearn();
				return null;
			}
			else if (commandClone == "check")
			{
				return clones[numberClone].Check();
			}
			else if (commandClone == "clone")
			{
				Clone newClone = new Clone(clones[numberClone]);
				clones.Add(newClone);
				return null;
			}
			return null;
		}
	}
}
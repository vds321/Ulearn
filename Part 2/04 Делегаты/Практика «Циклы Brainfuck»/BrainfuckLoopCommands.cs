using System.Collections.Generic;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
		public static void RegisterTo(IVirtualMachine vm)
		{
			var indexOfSquareBrackets = new Stack<int>();
			var squareBracketsDict = new Dictionary<int, int>();
			for (int i = 0; i < vm.Instructions.Length; i++)
			{
				if (vm.Instructions[i] == '[') indexOfSquareBrackets.Push(i);
				if (vm.Instructions[i] == ']')
				{
					var tempIndex = indexOfSquareBrackets.Pop();
					squareBracketsDict.Add(tempIndex, i);
					squareBracketsDict.Add(i, tempIndex);
				}
			}
			vm.RegisterCommand('[', virtMachine =>
			{
				if (vm.Memory[vm.MemoryPointer] == 0) vm.InstructionPointer = squareBracketsDict[vm.InstructionPointer];
			});
			vm.RegisterCommand(']', virtMachine =>
			{
				if (vm.Memory[vm.MemoryPointer] != 0) vm.InstructionPointer = squareBracketsDict[vm.InstructionPointer];
			});
		}
	}
}
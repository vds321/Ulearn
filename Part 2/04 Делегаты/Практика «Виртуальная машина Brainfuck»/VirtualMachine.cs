using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public VirtualMachine(string program, int memorySize)
		{
			Instructions = program;
			Memory = new byte[memorySize];
			InstructionPointer = 0;
			MemoryPointer = 0;
			RegCommand = new Dictionary<char, Action<IVirtualMachine>>();
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			RegCommand.Add(symbol, execute);
		}

		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }
		public Dictionary<char, Action<IVirtualMachine>> RegCommand;// = new Dictionary<char, Action<IVirtualMachine>>();
		public void Run()
		{
			for (int i = InstructionPointer; InstructionPointer < Instructions.Length; InstructionPointer++)
			{
				if (RegCommand.ContainsKey(Instructions[InstructionPointer])) RegCommand[Instructions[InstructionPointer]].Invoke(this);
			}
		}
	}
}
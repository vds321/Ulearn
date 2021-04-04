using System;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
            string symbolCollection = String.Empty;
            for (char i = 'A'; i <= 'Z'; i++)
            {
                symbolCollection += i;
            }
            for (char i = 'a'; i <= 'z'; i++)
            {
                symbolCollection += i;
            }
            for (char i = '0'; i <= '9'; i++)
            {
                symbolCollection += i;
            }

            foreach (var symbol in symbolCollection.ToCharArray())
			{
				vm.RegisterCommand(symbol, virtMachine => virtMachine.Memory[virtMachine.MemoryPointer] = (byte)symbol);
			}

			vm.RegisterCommand('.', virtMachine => write((char)virtMachine.Memory[virtMachine.MemoryPointer]));

			vm.RegisterCommand('+', virtMachine =>
			{
				if (virtMachine.Memory[virtMachine.MemoryPointer] == 255) virtMachine.Memory[virtMachine.MemoryPointer] = 0;
				else virtMachine.Memory[virtMachine.MemoryPointer]++;

			});
			vm.RegisterCommand('-', virtMachine =>
			{
				if (virtMachine.Memory[virtMachine.MemoryPointer] == 0) virtMachine.Memory[virtMachine.MemoryPointer] = 255;
				else virtMachine.Memory[virtMachine.MemoryPointer]--;

			});

			vm.RegisterCommand(',', virtMachine => virtMachine.Memory[virtMachine.MemoryPointer] = (byte)read());

			vm.RegisterCommand('>', virtMachine =>
			{
				if (virtMachine.MemoryPointer < virtMachine.Memory.Length - 1) virtMachine.MemoryPointer++;
				else virtMachine.MemoryPointer = 0;

			});

			vm.RegisterCommand('<', virtMachine =>
			{
				if (virtMachine.MemoryPointer != 0) virtMachine.MemoryPointer--;
				else virtMachine.MemoryPointer = virtMachine.Memory.Length - 1;
			});
		}
	}
}
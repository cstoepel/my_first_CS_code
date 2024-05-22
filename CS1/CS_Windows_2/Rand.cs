using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win32_Console;

namespace CS_Windows_2
{
	internal static class Rand
	{
		internal static void RandomPattern1(object data)
		{
			Window w = (Window)data;
			Random random = new Random();
			while (true)
			{
				switch (random.Next(1, 5))
				{
					case 1: w.PutChar('█', 0x00f); break;
					case 2: w.PutChar('█', 0x00e); break;
					case 3: w.PutChar('█', 0x00d); break;
					case 4: w.PutChar('█', 0x00c); break;
					case 5: w.PutChar('█', 0x00b); break;
				}
				w.CopyAppBuffer();
				Thread.Sleep(2);
			}
		}

		internal static void RandomPattern3(object data)
		{
			Window w = (Window)data;
			Random random = new Random();
			int index = 0;
			Win32.CHAR_INFO Char = new();
			Char.UnicodeChar = '█';
			short attr = 0;
			while (true)
			{
				for (int i = 0; i < w.AppBufferSize; i++)
				{
					switch (random.Next(1, 5))
					{
						case 1: attr = 0x00f; break;
						case 2: attr = 0x00e; break;
						case 3: attr = 0x00d; break;
						case 4: attr = 0x00c; break;
						case 5: attr = 0x00b; break;
					}
					Char.Attributes = attr;
					w.AppBuffer[i] = Char;
				}
				w.CopyAppBuffer();
				Thread.Sleep(500);
			}
		}

		internal static void RandomPattern2(object data)
		{
			Window w = (Window)data;
			Random random = new Random();
			while (true)
			{
				switch (random.Next(1, 6))
				{
					case 1: w.PutChar('╠', 0x004); break;
					case 2: w.PutChar('╣', 0x004); break;
					case 3: w.PutChar('╩', 0x004); break;
					case 4: w.PutChar('╦', 0x004); break;
					case 5: w.PutChar('║', 0x004); break;
					case 6: w.PutChar('═', 0x004); break;
				}
				w.CopyAppBuffer();
				Thread.Sleep(2);
			}
		}
	}
}

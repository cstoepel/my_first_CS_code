using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Win32_Console;

internal class WinConsole
{
	public WinConsole()
	{
		Width = Console.WindowWidth;
		Height = Console.WindowHeight;
		Console.ResetColor();
	}

	public WinConsole(int width, int height, ConsoleColor fg = ConsoleColor.DarkGray, ConsoleColor bg = ConsoleColor.Black)
	{
		Width = width;
		Height = height;
		Console.SetWindowSize(width, height);
		Console.ForegroundColor = fg;
		Console.BackgroundColor = bg;
	}

	public int Width { get; }
	public int Height { get; }
	public void SetFGColor(ConsoleColor c) { Console.ForegroundColor = c; }
	public void SetBGColor(ConsoleColor c) { Console.BackgroundColor = c; }
	public void SetFGColor(byte r, byte g, byte b) { Console.Write("\x1b[38;2;" + r.ToString() + ";" + g.ToString() + ";" + b.ToString() + "m"); }
	public void SetBGColor(byte r, byte g, byte b) { Console.Write("\x1b[48;2;" + r.ToString() + ";" + g.ToString() + ";" + b.ToString() + "m"); }
	public void Clear() { Console.Clear(); }
	public void ResetColor() { Console.ResetColor(); }

	public void Print(string msg) { Console.Write(msg);}
	[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
	public static extern UInt32 CreateConsoleScreenBuffer
		(
			UInt32 dwDesiredAccess,  // GENERIC_WRITE 
			UInt32 dwShareMode,       //  0x0
			IntPtr lpSecurityAttributes,  // IntPtr.Zero
			UInt32 dwFlags,               // CONSOLE_TEXTMODE_BUFFER
			IntPtr lpScreenBufferData //
		);
	[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
	public static extern int WriteConsoleOutput(
		UInt32 hConsoleOutput,
		IntPtr lpBuffer,
		UInt32 nNumberOfCharsToWrite,
		IntPtr lpNumberOfCharsWritten,
		IntPtr lpReserved
	);

	public struct COORD
	{
		byte X;
		byte Y;
	}
}

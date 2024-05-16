namespace Win32_Console;
using System.Runtime.InteropServices;

internal class Program
{
	static unsafe void Main(string[] args)
	{
		WinConsole screen = new WinConsole(80, 40);
		screen.SetFGColor(ConsoleColor.Green);

		while (true) Thread.Sleep(1000);
	}
}


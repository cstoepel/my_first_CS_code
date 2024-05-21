namespace Win32_Console;
using System.Runtime.InteropServices;

internal class Program
{
	static unsafe void Main(string[] args)
	{
		WinConsole.Init(140, 50);

		Window W0 = new Window("Calc", 10, 10, 40, 25);
		Window W1 = new Window("Win2", 32, 15, 30, 24);
		Window W2 = new Window("Win3", 52, 3, 40, 25);

		WinConsole.AddWindow(W0);
		WinConsole.AddWindow(W1);
		WinConsole.AddWindow(W2);
		WinConsole.Clear();
		W0.Refresh();
		W1.Refresh();
		W2.Refresh();
		WinConsole.SetActiveWindow(W1);

		while (true) Thread.Sleep(1000);
	}
}


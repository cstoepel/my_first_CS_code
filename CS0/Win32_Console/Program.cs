namespace Win32_Console;

internal class Program
{
	static void Main(string[] args)
	{
		WinConsole screen = new WinConsole(100, 100);
		UInt32 screen_handle = WinConsole.CreateConsoleScreenBuffer(0x40000000, 0, 0, 0, 0);
		screen.Print($"{screen_handle}");

		WinConsole.WriteConsoleOutput(screen_handle, 0,0 ,0,0);
	}
}

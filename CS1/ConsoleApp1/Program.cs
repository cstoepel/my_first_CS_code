using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace ConsoleApp1;

internal class Program
{
	static void Main(string[] args)
	{
		Console.WriteLine("Hello, World!");
		Win32 a = new Win32();
		//a.test();
		Win32.MessageBox(0, "HALLO CLAUS", "Message", 0);

		 private readonly SafeFileHandle _consolehandle;
		_consolehandle = CreateFile("CONOUT$", 0x40000000, 0x02, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);

	}






	class Win32
	{
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public static extern int MessageBox(IntPtr hWnd, String text, String caption, uint type);
	[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
	public static extern IntPtr CreateFile
	(
		UInt32 dwDesiredAccess,
		UInt32 dwShareMode,
		IntPtr lpSecurityAttributes,
		UInt32 dwFlags,
		IntPtr lpScreenBufferData
	);
	[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		public static extern IntPtr CreateConsoleScreenBuffer
		(
			UInt32 dwDesiredAccess,
			UInt32 dwShareMode,
			IntPtr lpSecurityAttributes,
			UInt32 dwFlags,
			IntPtr lpScreenBufferData
		);
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		public static extern int WriteConsoleOutput(
			UInt32  hConsoleOutput,
			IntPtr lpBuffer,
			UInt32 nNumberOfCharsToWrite,
			IntPtr lpNumberOfCharsWritten,
			IntPtr lpReserved
		);
	}

}


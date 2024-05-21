using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Win32_Console.Win32;
using static Win32_Console.WinConsole;

namespace Win32_Console;

internal class WinConsole
{
	public  int Width  { get; }
	public  int Height { get; }
	private Win32.CHAR_INFO[] CharInfoBuffer { get; set; }
	public int CharInfoBufferSize;
	Win32.CHAR_INFO Character = new Win32.CHAR_INFO();
	public SafeFileHandle Handle_STDOUT;
	public SafeFileHandle Handle_STDIN;
	public SafeFileHandle Handle_SB;
	public SafeFileHandle Handle_ActiveBuffer;
	private Win32.SMALL_RECT Rect;
	private Win32.COORD      x0y0;
	private Win32.COORD      DimXY;
	private Win32.PCONSOLE_SCREEN_BUFFER_INFOEX sb_infoex;
	private System.Timers.Timer RefreshTimer;
	public Win32.INPUT_RECORD[] InputRecords = new INPUT_RECORD[32];
	public static Int32 FrameCounter = 0;
	private short Mouse_x = 0;
	private short Mouse_y = 0;
	private int Mouse_button = 0;
	private int Mouse_control = 0;
	private int Mouse_flags = 0;
	private int Key_bKeyDown = 0;
	private short Key_wRepeatCount = 0;
	private short Key_wVirtualKeyCode =0;
	private short Key_wVirtualScanCode = 0;
	private int Key_dwControlKeyState = 0;
	private short ColorIndex = 15;


	public WinConsole(int width, int height, ConsoleColor fg = ConsoleColor.DarkGray, ConsoleColor bg = ConsoleColor.Black)
	{
		Width  = width;
		Height = height;
		Console.SetWindowSize(Width, Height);
		Console.ForegroundColor = fg;
		Console.BackgroundColor = bg;
		Rect  = new Win32.SMALL_RECT { X0 = 0, Y0 = 0, X1 = (short)Width, Y1 = (short)Height };
		DimXY = new Win32.COORD() { X = (short)Width, Y = (short)Height };
		x0y0  = new Win32.COORD() { X = (short)0,     Y = (short)0 };
		Handle_STDOUT = Win32.GetStdHandle(Win32.STD_OUTPUT_HANDLE);
		Handle_STDIN  = Win32.GetStdHandle(Win32.STD_INPUT_HANDLE);
		Handle_SB     = Win32.CreateConsoleScreenBuffer
		(
			0x40000000,
			0x00000002,
			IntPtr.Zero, // default security attributes
			Win32.CONSOLE_TEXTMODE_BUFFER,
			IntPtr.Zero
		);
		Handle_ActiveBuffer = Handle_STDOUT;
		//Win32.SetConsoleActiveScreenBuffer(Handle_ActiveBuffer);
		if (!Handle_ActiveBuffer.IsInvalid)
		{
			CharInfoBuffer = new Win32.CHAR_INFO[Width * Height];
			CharInfoBufferSize = Width * Height;
		}
		if (!Win32.SetConsoleMode(Handle_STDIN, Win32.ENABLE_EXTENDED_FLAGS | Win32.ENABLE_WINDOW_INPUT | Win32.ENABLE_MOUSE_INPUT))
		{
			Console.WriteLine("Win32.SetConsoleMode ERROR");
		}
		RefreshTimer = new System.Timers.Timer(10.0);
		RefreshTimer.Elapsed += RefreshScreen;
		RefreshTimer.Enabled = true;
		ClearScreen();
	}

	public void SetFGColor(ConsoleColor c) { Console.ForegroundColor = c; }
	public void SetBGColor(ConsoleColor c) { Console.BackgroundColor = c; }
	public void SetFGColor(byte r, byte g, byte b) { Console.Write("\x1b[38;2;" + r.ToString() + ";" + g.ToString() + ";" + b.ToString() + "m"); }
	public void SetBGColor(byte r, byte g, byte b) { Console.Write("\x1b[48;2;" + r.ToString() + ";" + g.ToString() + ";" + b.ToString() + "m"); }
	public void Clear()      { Console.Clear(); }
	public void ResetColor() { Console.ResetColor(); }

	public bool CopyBuffer()
	{
		return Win32.WriteConsoleOutputW
		(
			Handle_ActiveBuffer,
			CharInfoBuffer,
			DimXY,
			x0y0,
			ref Rect
		);
	}
	public void SetPixel(int x, int y, char c = '█', Int16 attr = 0x000f)
	{
		Character.UnicodeChar = c;
		Character.Attributes = attr;
		CharInfoBuffer[y * Width + x] = Character;
	}

	public void ClearScreen(char c = '.')
	{
		Character.UnicodeChar = c;
		Character.Attributes = 0x000f;
		for (int i = 0; i < CharInfoBufferSize; i++) CharInfoBuffer[i] = Character;
	}

	public void RefreshScreen(Object source, System.Timers.ElapsedEventArgs e)
	{
		//ClearScreen();
		FrameCounter++;
		//Console.SetCursorPosition(0, 0);
		//Console.WriteLine("╔═══════════════════════╗");
		//Console.WriteLine($"║ RefreshScreen: {FrameCounter:d5}  ║");
		//Console.WriteLine("╚═══════════════════════╝");

		UInt32 Number_of_Events  = 0;
		UInt32 Events_ausgelesen = 0;
		Win32.GetNumberOfConsoleInputEvents(Handle_STDIN, ref Number_of_Events);

		//Console.Write($"    Win32.GetNumberOfConsoleInputEvents()\n");
		//Console.Write($"        Nr Events in Warteschlange = {Number_of_Events}\n");

		if (Number_of_Events > 0) Win32.ReadConsoleInput(Handle_STDIN, InputRecords, 1, ref Events_ausgelesen);

		//Console.Write($"    Win32.ReadConsoleInput()\n");
		//Console.Write($"        Nr. Events ausgelesen = {Events_ausgelesen}\n\n");

		for (int i = 0; i < Events_ausgelesen; i++)
		{
			//Console.Write($"    EVENT_RECORD[{i}].EventType = {InputRecords[i].EventType}\n");
			switch (InputRecords[i].EventType)
			{
				case Win32.KEY_EVENT:
					Key_bKeyDown          = InputRecords[i].EventRecord.KeyEvent.bKeyDown;
					Key_wRepeatCount      = InputRecords[i].EventRecord.KeyEvent.wRepeatCount;
					Key_wVirtualKeyCode   = InputRecords[i].EventRecord.KeyEvent.wVirtualKeyCode;
					Key_wVirtualScanCode  = InputRecords[i].EventRecord.KeyEvent.wVirtualScanCode;
					Key_dwControlKeyState = InputRecords[i].EventRecord.KeyEvent.dwControlKeyState;
					switch(Key_wVirtualKeyCode)
					{
						case 0x0031: ColorIndex = 9; break;
						case 0x0032: ColorIndex = 1; break;
						case 0x0033: ColorIndex = 2; break;
						case 0x0034: ColorIndex = 3; break;
						case 0x0035: ColorIndex = 4; break;
						case 0x0036: ColorIndex = 5; break;
						case 0x0037: ColorIndex = 6; break;
						case 0x0038: ColorIndex = 7; break;
						case 0x0039: ColorIndex =10; break;
						case 0x0043: ClearScreen(' '); break;
						case 0x0050: ClearScreen(); break;
					}

					break;
				case Win32.MOUSE_EVENT:
					Mouse_x       = InputRecords[i].EventRecord.MouseEvent.dwMousePosition.X;
					Mouse_y       = InputRecords[i].EventRecord.MouseEvent.dwMousePosition.Y;
					Mouse_button  = InputRecords[i].EventRecord.MouseEvent.dwButtonState;
					Mouse_control = InputRecords[i].EventRecord.MouseEvent.dwControlKeyState;
					Mouse_flags   = InputRecords[i].EventRecord.MouseEvent.dwEventFlags;
					switch (Mouse_button)
					{
						case 0x00000001: SetPixel(Mouse_x, Mouse_y, '█', ColorIndex); break;
						case 0x00000002: SetPixel(Mouse_x, Mouse_y, 'X', ColorIndex); break;
					}
					break;
				case Win32.WINDOW_BUFFER_SIZE_EVENT: break;
				case 0: break;
			}
		}
		//Console.Write($"MOUSE:          XY = [{Mouse_x,3},{Mouse_y,3}]\n            button = 0x{Mouse_button:x8}\n           control = 0x{Mouse_control:x8}\n             flags = 0x{Mouse_flags:x8} \n");
		//Console.Write($"  KEY:     KeyDown = 0x{Key_bKeyDown:x8}\n       RepeatCount = 0x{Key_wRepeatCount:x4}\n    VirtualKeyCode = 0x{Key_wVirtualKeyCode:x4}\n   VirtualScanCode = 0x{Key_wVirtualScanCode:x4} \n");
		CopyBuffer();
	}
}

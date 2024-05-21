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

internal static class WinConsole
{
	static private System.Timers.Timer RefreshTimer;
	static public int Width;
	static public int Height;
	static public  SafeFileHandle   Handle_STDOUT;
	static public  SafeFileHandle   Handle_STDIN;
	static public  SafeFileHandle   Handle_SB;
	static public  SafeFileHandle   Handle_ActiveBuffer;
	static public  Win32.CHAR_INFO  Character = new Win32.CHAR_INFO();
	static private Win32.SMALL_RECT Rect;
	static private Win32.COORD      x0y0;
	static private Win32.COORD      DimXY;
	static private Win32.PCONSOLE_SCREEN_BUFFER_INFOEX sb_infoex;
	static public  Win32.INPUT_RECORD[]       InputRecords = new INPUT_RECORD[32];
	static public  Win32.PCONSOLE_FONT_INFOEX fontinfo     = new Win32.PCONSOLE_FONT_INFOEX();
	static private Win32.CHAR_INFO[]          CharInfoBuffer { get; set; }
	static public  Int32 CharInfoBufferSize;
	static public  Int32 FrameCounter = 0;
	static public short Mouse_x = 0;
	static public short Mouse_y = 0;
	static public int   Mouse_button  = 0;
	static public int   Mouse_control = 0;
	static public int   Mouse_flags   = 0;
	static public int   Key_bKeyDown  = 0;
	static public short Key_wRepeatCount      = 0;
	static public short Key_wVirtualKeyCode   = 0;
	static public short Key_wVirtualScanCode  = 0;
	static public int   Key_dwControlKeyState = 0;
	static public short ColorIndex = 15;
	static public Window ActiveWindow;
	static Color TextColor       = Color.FromArgb(0x00, 0xff, 0xff, 0xff);
	static Color BackgroundColor = Color.FromArgb(0x00, 0x00, 0x00, 0x00);

	static private List<Window> WindowList = new List<Window>();

	//static public void Init(int width, int height, ConsoleColor fg = ConsoleColor.DarkGray, ConsoleColor bg = ConsoleColor.Black)
	static public void Init(int width, int height)
	{
		Width  = width;
		Height = height;
		Console.SetWindowSize(width, height);
		//Console.ForegroundColor = fg;
		//Console.BackgroundColor = bg;
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
		//Win32.SetConsoleActiveScreenBuffer(Handle_ActiveBuffer);
		//CharInfoBuffer = new Win32.CHAR_INFO[Width * Height];
		//CharInfoBufferSize = Width * Height;

		Handle_ActiveBuffer = Handle_STDOUT;
		CharInfoBuffer = new Win32.CHAR_INFO[Width * Height];
		CharInfoBufferSize = Width * Height;
		if (!Win32.SetConsoleMode(Handle_STDIN, Win32.ENABLE_EXTENDED_FLAGS | Win32.ENABLE_WINDOW_INPUT | Win32.ENABLE_MOUSE_INPUT))
		{
			Console.WriteLine("Win32.SetConsoleMode ERROR");
		}

		//RootWindow = new Window("Root", 0, 0, Width, Height);
		//RootWindow.Refresh();
		//AddWindow(RootWindow);

		RefreshTimer = new System.Timers.Timer(20.0);
		RefreshTimer.Elapsed += RefreshScreen;
		RefreshTimer.Enabled = true;
		//ClearScreen();
	}

	static public void SetFGColor(ConsoleColor c) { Console.ForegroundColor = c; }
	static public void SetBGColor(ConsoleColor c) { Console.BackgroundColor = c; }
	static public void SetFGColor(byte r, byte g, byte b) { Console.Write("\x1b[38;2;" + r.ToString() + ";" + g.ToString() + ";" + b.ToString() + "m"); }
	static public void SetBGColor(byte r, byte g, byte b) { Console.Write("\x1b[48;2;" + r.ToString() + ";" + g.ToString() + ";" + b.ToString() + "m"); }
	static public void Clear()      { Console.Clear(); }
	static public void ResetColor() { Console.ResetColor(); }
	static public void AddWindow(Window w)
	{
		WindowList.Add(w);
	}

	static public void SetActiveWindow(Window w)
	{
		ActiveWindow = w;
	}
	static public void CopyBuffer(Window w)
	{
		//Console.WriteLine($"{w}");
		//return;
		for (int y = 0; y < w.Height; y++)
		{
			for (int x = 0; x < w.Width; x++)
			{
				//CHAR_INFO a = w.CharInfoBuffer[y * w.Width + x];
				CharInfoBuffer[(y + w.Y) * Width + x + w.X] = w.CharInfoBuffer[y * w.Width + x];
			}
		}
	}


	static public bool FlushBuffer()
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


	//static public void SetPixel(int x, int y, char c = '█', Int16 attr = 0x000f)
	//{
	//	Character.UnicodeChar = c;
	//	Character.Attributes = attr;
	//	CharInfoBuffer[y * Width + x] = Character;
	//}

	static public void SetPixel(Window w, int x, int y, char c = '█', Int16 attr = 0x000f)
	{
		Character.UnicodeChar = c;
		Character.Attributes = attr;
		w.CharInfoBuffer[y * w.Width + x] = Character;
	}
	static public void Clear(Window w, char c = '.')
	{
		Character.UnicodeChar = c;
		Character.Attributes = 0x000f;
		for (int i = 0; i < w.CharInfoBufferSize; i++) w.CharInfoBuffer[i] = Character;
	}

	static public void ClearBuffer(char c = '.')
	{
		Character.UnicodeChar = c;
		Character.Attributes = 0x000f;
		for (int i = 0; i < CharInfoBufferSize; i++) CharInfoBuffer[i] = Character;
	}


	static public void RefreshScreen(Object source, System.Timers.ElapsedEventArgs e)
	{
		FrameCounter++;
		UInt32 Number_of_Events  = 0;
		UInt32 Events_ausgelesen = 0;
		Win32.GetNumberOfConsoleInputEvents(Handle_STDIN, ref Number_of_Events);

		if (Number_of_Events > 0) Win32.ReadConsoleInput(Handle_STDIN, InputRecords, 1, ref Events_ausgelesen);

		for (int i = 0; i < Events_ausgelesen; i++)
		{
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
						//case 0x0043: ClearScreen(' '); break;
						//case 0x0050: ClearScreen(); break;

					}

					break;
				case Win32.MOUSE_EVENT:
					Mouse_x       = InputRecords[i].EventRecord.MouseEvent.dwMousePosition.X;
					Mouse_y       = InputRecords[i].EventRecord.MouseEvent.dwMousePosition.Y;
					Mouse_button  = InputRecords[i].EventRecord.MouseEvent.dwButtonState;
					Mouse_control = InputRecords[i].EventRecord.MouseEvent.dwControlKeyState;
					Mouse_flags   = InputRecords[i].EventRecord.MouseEvent.dwEventFlags;
					if (ActiveWindow != null)
					{
						ActiveWindow.X = Mouse_x;
						ActiveWindow.Y = Mouse_y;
					}
					//switch (Mouse_button)
					//{
					//	case 0x00000001: SetPixel(Mouse_x, Mouse_y, '█', ColorIndex); break;
					//	case 0x00000002: SetPixel(Mouse_x, Mouse_y, 'x', ColorIndex); break;
					//}
					break;
				case Win32.WINDOW_BUFFER_SIZE_EVENT: break;
				case 0: break;
			}
		}
		ClearBuffer();
		foreach(var w in WindowList)
		{
			CopyBuffer(w);
		}
		FlushBuffer();
		Console.SetCursorPosition(0, 0);
		Console.WriteLine($"Refresh: {FrameCounter}");
	}
}

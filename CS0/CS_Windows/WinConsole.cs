using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Win32_Console.Win32;
using static Win32_Console.WinConsole;

namespace Win32_Console;

internal static class WinConsole
{
	//static private System.Timers.Timer RefreshTimer;
	static public int Width;
	static public int Height;
	static public  IntPtr   Handle_STDOUT;
	static public  IntPtr   Handle_STDIN;
	static public  IntPtr   Handle_SB;
	static public  IntPtr   Handle_ActiveBuffer;
	static public  Win32.CHAR_INFO  Character = new();
	static private Win32.SMALL_RECT Rect;
	static private Win32.COORD      x0y0;
	static private Win32.COORD      DimXY;
	static private Win32.PCONSOLE_SCREEN_BUFFER_INFOEX sb_infoex;
	static public  Win32.INPUT_RECORD[]       InputRecords = new INPUT_RECORD[32];
	static public  Win32.PCONSOLE_FONT_INFOEX fontinfo     = new();
	static private Win32.CHAR_INFO[]          CharInfoBuffer { get; set; }
	static public  Int32 CharInfoBufferSize;
	static public  Int32 FrameCounter = 0;
	static public Int16 Mouse_x = 0;
	static public Int16 Mouse_y = 0;
	static public Int16 MouseDX = 0;
	static public Int16 MouseDY = 0;
	static public int   MouseButton  = 0;
	static public int   MouseButton1 = 0;
	static public int   Mouse_control = 0;
	static public int   Mouse_flags   = 0;
	static public int   Key_bKeyDown  = 0;
	static public short Key_wRepeatCount      = 0;
	static public short Key_wVirtualKeyCode   = 0;
	static public short Key_wVirtualScanCode  = 0;
	static public int   Key_dwControlKeyState = 0;
	static public short ColorIndex = 15;
	static public bool MoveFlag = false;
	static public Window ActiveWindow;
	static public Window RootWindow;
	static public Mutex CopyBufferMutex = new();
	static private List<Window> WindowList = new();
	static public PCONSOLE_SCREEN_BUFFER_INFOEX csbi = new();

	static public unsafe void Init(int width, int height)
	{
		Width  = width;
		Height = height;
		Console.SetWindowSize(width, height);
		Rect          = new Win32.SMALL_RECT { X0 = 0, Y0 = 0, X1 = (short)Width, Y1 = (short)Height };
		DimXY         = new Win32.COORD() { X = (short)Width, Y = (short)Height };
		x0y0          = new Win32.COORD() { X = (short)0,     Y = (short)0 };
		Handle_STDOUT = Win32.GetStdHandle(Win32.STD_OUTPUT_HANDLE);
		Handle_STDIN  = Win32.GetStdHandle(Win32.STD_INPUT_HANDLE);
		Handle_SB     = Win32.CreateConsoleScreenBuffer (
			Win32.GENERIC_READ | Win32.GENERIC_WRITE,
			Win32.FILE_SHARE_READ | Win32.FILE_SHARE_WRITE,
			IntPtr.Zero, // default security attributes
			Win32.CONSOLE_TEXTMODE_BUFFER,
			IntPtr.Zero);

		Handle_ActiveBuffer = Handle_STDOUT;
		//Handle_ActiveBuffer = Handle_SB;
		Win32.SetConsoleActiveScreenBuffer(Handle_ActiveBuffer);
		CharInfoBuffer = new Win32.CHAR_INFO[Width * Height];
		CharInfoBufferSize = Width * Height;
		if (!Win32.SetConsoleMode(Handle_STDIN, Win32.ENABLE_EXTENDED_FLAGS | Win32.ENABLE_WINDOW_INPUT | Win32.ENABLE_MOUSE_INPUT))
			Console.WriteLine("ERROR SetConsoleMode");
		csbi.cbSize = (int)Marshal.SizeOf(csbi);
		if (Win32.GetConsoleScreenBufferInfoEx(Handle_STDOUT, ref csbi)) {
			Console.WriteLine("ERROR GetConsoleScreenBufferInfoEx");
		}
		// Custom text color in C# console application
		// https://stackoverflow.com/questions/7937256/custom-text-color-in-c-sharp-console-application
		csbi.color0 = new COLORREF(Color.FromArgb(0x00, 0x00, 0x00, 0x00)); // schwarz
		csbi.color1 = new COLORREF(Color.FromArgb(0x00, 0xff, 0x30, 0x00)); // rot
		csbi.color2 = new COLORREF(Color.FromArgb(0x00, 0x99, 0x33, 0x00)); // dunkelrot
		csbi.color3 = new COLORREF(Color.FromArgb(0x00, 0x66, 0xff, 0x00)); // gruen
		csbi.color4 = new COLORREF(Color.FromArgb(0x00, 0x33, 0x99, 0x00)); // dunkelgruen
		csbi.color5 = new COLORREF(Color.FromArgb(0x00, 0x00, 0x55, 0xff)); // blau
		csbi.color6 = new COLORREF(Color.FromArgb(0x00, 0x00, 0x11, 0x99)); // dunkelbau
		csbi.color7 = new COLORREF(Color.FromArgb(0x00, 0x76, 0x11, 0x99)); // megenta
		csbi.color8 = new COLORREF(Color.FromArgb(0x00, 0xeb, 0xca, 0x50)); // gelb
		csbi.color9 = new COLORREF(Color.FromArgb(0x00, 0x50, 0xcf, 0xcf)); // cyan
		csbi.colorA = new COLORREF(Color.FromArgb(0x00, 0x00, 0x00, 0x00)); //
		csbi.colorB = new COLORREF(Color.FromArgb(0x00, 0x33, 0x33, 0x33)); // grau1
		csbi.colorC = new COLORREF(Color.FromArgb(0x00, 0x66, 0x66, 0x66)); // grau2
		csbi.colorD = new COLORREF(Color.FromArgb(0x00, 0x99, 0x99, 0x99)); // grau3
		csbi.colorE = new COLORREF(Color.FromArgb(0x00, 0xcc, 0xcc, 0xcc)); // grau4
		csbi.colorF = new COLORREF(Color.FromArgb(0x00, 0xff, 0xff, 0xff)); // weiss
		if (Win32.SetConsoleScreenBufferInfoEx(Handle_ActiveBuffer, ref csbi))
			Console.WriteLine("ERROR SetConsoleScreenBufferInfoEx");

		// Window immer im Hintergrund von Größe der gesamten Console
		RootWindow = new Window("", 0, 0, Width, Height);
		RootWindow.HasBorder      = false;
		RootWindow.IsMovable      = false;
		RootWindow.IsRoot         = true;
		RootWindow.BackgroundChar = '░';
		RootWindow.BackgroundAttr = 0x000b6;
		RootWindow.TitleAttr      = 0x002f;
		RootWindow.TitleOffset    = 0;
		RootWindow.Name = "░░░░░░░ Windows 3000 ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░" +
						  "░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░";
		RootWindow.Refresh();
		AddWindow(RootWindow);
	}

	static public void SetFGColor(byte r, byte g, byte b) { Console.Write("\x1b[38;2;" + r.ToString() + ";" + g.ToString() + ";" + b.ToString() + "m"); }
	static public void SetBGColor(byte r, byte g, byte b) { Console.Write("\x1b[48;2;" + r.ToString() + ";" + g.ToString() + ";" + b.ToString() + "m"); }
	static public void SetFGColor(ConsoleColor c) { Console.ForegroundColor = c; }
	static public void SetBGColor(ConsoleColor c) { Console.BackgroundColor = c; }
	static public void Clear()                    { Console.Clear(); }
	static public void ResetColor()               { Console.ResetColor(); }
	static public void AddWindow(Window w)        { WindowList.Add(w); }
	
	static public void SetActiveWindow(int index)
	{
		// Index 0 = Root Window
		if (index < 1 || index >= WindowList.Count) return;
		SetActiveWindow(WindowList[index]); }

	static public void SetActiveWindow(Window window){
		foreach(Window w in WindowList) w.HasFocus = false;
		ActiveWindow = window;
		ActiveWindow.HasFocus = true;
		foreach (Window w in WindowList) w.Refresh();
	}

	static public void RiseWindow(Window w)
	{
		if (!w.IsRoot)
		{
			WindowList.Remove(w);
			WindowList.Add(w);
		}
	}

	static public Window GetWindowClicked(int Mouse_x, int Mouse_y) {
		int x0, y0, x1, y1;
		for (int i = WindowList.Count - 1; i >= 0; i--)
		{
			x0 = WindowList[i].X;
			y0 = WindowList[i].Y;
			x1 = x0 + WindowList[i].Width;
			y1 = y0 + WindowList[i].Height;
			if (Mouse_x >= x0 && Mouse_x <= x1 && Mouse_y >= y0 && Mouse_y <= y1)
				return WindowList[i];
		}
		return WindowList[0];
	}

	static public void MoveWindow(Window w, int Mouse_x, int Mouse_y, int dx, int dy)
	{
		if (w.IsMovable)
		{
			w.X = (short)(Mouse_x - dx);
			w.Y = (short)(Mouse_y - dy);
		}
	}

	// Copy Window-Buffer to Screen-Buffer
	static public void CopyBuffer(Window w)	{
		int source_index = 0, dest_index = 0;
		for (int y = 0; y < w.Height; y++) {
			for (int x = 0; x < w.Width; x++) {
				source_index = y * w.Width + x;
				dest_index   = (y + w.Y) * Width + x + w.X;
				if (dest_index < CharInfoBufferSize) {
					CharInfoBuffer[dest_index] = w.WindowBuffer[source_index];
				}
			}
		}
	}

	// Copy Screen-Buffer to Console-Buffer
	static public bool FlushBuffer() {
		return Win32.WriteConsoleOutputW (
			Handle_ActiveBuffer,
			CharInfoBuffer,
			DimXY,
			x0y0,
			ref Rect);
	}

	static public void SetPixel(Window w, int x, int y, char c = '█', Int16 attr = 0x000f) {
		CopyBufferMutex.WaitOne();
			Character.UnicodeChar = c;
			Character.Attributes  = attr;
			w.WindowBuffer[y * w.Width + x] = Character;
		CopyBufferMutex.ReleaseMutex();
	}

	//static public void Clear(Window w, char c = '.')
	//{
	//	CopyBufferMutex.WaitOne();
	//		Character.UnicodeChar = c;
	//		Character.Attributes = 0x000f;
	//		for (int i = 0; i < w.WindowBufferSize; i++) w.WindowBuffer[i] = Character;
	//	CopyBufferMutex.ReleaseMutex();
	//}

	static public void ClearBuffer(char c = '.') {
		Character.UnicodeChar = c;
		Character.Attributes = 0x000f;
		for (int i = 0; i < CharInfoBufferSize; i++) CharInfoBuffer[i] = Character;
	}

	static public void Start()
	{
		Thread thread = new Thread(Loop);
		thread.Start();
	}

	static public void Loop()
	{
		while (true)
		{
			FrameCounter++;
			ProcessEvents();
			RefreshScreen();
			//Console.WriteLine($"Refresh: {FrameCounter}");
			Thread.Sleep(20);
		}
	}

	static public void ProcessEvents()
	{
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
					switch (Key_wVirtualKeyCode)
					{
						case 0x0031: SetActiveWindow(1); break;
						case 0x0032: SetActiveWindow(2); break;
						case 0x0033: SetActiveWindow(3); break;
						case 0x0034: SetActiveWindow(4); break;
						case 0x0035: SetActiveWindow(5); break;
						case 0x0036: SetActiveWindow(6); break;
						case 0x0037: SetActiveWindow(7); break;
						case 0x0038: SetActiveWindow(8); break;
						case 0x0039: SetActiveWindow(9); break;
					}
					break;
				case Win32.MOUSE_EVENT:
					//MouseButton: 0x00001   links
					//MouseButton: 0x00002   rechts
					//MouseButton: 0x00004   mitte
					// Bit: 76543210    Mouse_control modifier kys
					//      00100000
					//         || |        kein  0x0020
					//         || +------- Alt   0x0022
					//         |+--------- Crtl  0x0028
					//         +---------- Shift 0x0030
					//             Shift + Crtl  0x0038
					//               Alt + Crtl  0x003A
					Mouse_x       = InputRecords[i].EventRecord.MouseEvent.dwMousePosition.X;
					Mouse_y       = InputRecords[i].EventRecord.MouseEvent.dwMousePosition.Y;
					MouseButton   = InputRecords[i].EventRecord.MouseEvent.dwButtonState;
					Mouse_control = InputRecords[i].EventRecord.MouseEvent.dwControlKeyState;
					Mouse_flags   = InputRecords[i].EventRecord.MouseEvent.dwEventFlags;
					if (MouseButton1 == 0 && MouseButton == 1)
					{
						Window w = GetWindowClicked(Mouse_x, Mouse_y);
						RiseWindow(w);
						SetActiveWindow(w);
						if (Mouse_y == ActiveWindow.Y)
						{
							MoveFlag = true;
							MouseDX = (short)(Mouse_x - ActiveWindow.X);
							MouseDY = (short)(Mouse_y - ActiveWindow.Y);
						}
					}
					else if (MouseButton1 == 1 && MouseButton == 1 && MoveFlag)
					{
						MoveWindow(ActiveWindow, Mouse_x, Mouse_y, MouseDX, MouseDY);
					}
					else
					{
						MoveFlag = false;
					}
					MouseButton1 = MouseButton;
					break;
				case Win32.WINDOW_BUFFER_SIZE_EVENT: break;
				case 0: break;
			}
		}
	}

	static public void RefreshScreen()
	{
		//ClearBuffer();  // Wenn es ein RootWindow gibt dann kann man sich das sparen
		CopyBufferMutex.WaitOne();
			foreach (var w in WindowList) CopyBuffer(w);
		CopyBufferMutex.ReleaseMutex();
		FlushBuffer();
		Console.SetCursorPosition(0, 0);
	}
}

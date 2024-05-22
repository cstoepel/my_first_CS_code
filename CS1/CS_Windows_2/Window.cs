using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Win32_Console;

// Die Window Klasse besitzt zwei Buffer,
// 1. WinBuffer enhält die Gesamtinhalt des Fenster inklusive Rahmen
// 2. AppBuffer enthält nur die Region innerhalb des Rahmens
// Jedes Fenster ist selbst für das Kopieren des AppBuffers in den WinBuffer verantwortlich.
// Der WinBuffer jedes Fensters wird automatisch im WinConsole Thread in den ScreenBuffer kopiert.
// Achtung! Ein Mutex ist erforderlich, wenn auf den  WinBuffer zugegriffen wird. Der AppBuffer
// kann ohne Mutex beschrieben werden.
internal class Window {
	public Win32.CHAR_INFO[] WindowBuffer { get; set; }
	public Win32.CHAR_INFO[] AppBuffer { get; set; }
	public           Int32   WindowBufferSize;
	public           Int32   AppBufferSize;
	public Win32.CHAR_INFO   ClearChar;
	public Win32.CHAR_INFO   CurrentChar;
	public string DemoText = " Hello I am a Window.                        Give me some text.               I want Text!                     Where is my Text to display??              Give me TEXT!!!!                   Without text I will reset myself.....                               RESETTING                                                                                                                                         ";
	public int TextIndex = 0;
	public int BuffIndex = 0;
	public short _X, _Y;
	public short X
	{
		get { return _X; }
		set { _X = value < (short)0 ? (short)0 : value >= WinConsole.Width - Width ? (short)(WinConsole.Width - Width) : value; }
	}
	public short Y
	{
		get { return _Y; }
		set { _Y = value < (short)0 ? (short)0 : value >= WinConsole.Height - Height ? (short)(WinConsole.Height - Height) : value; }
	}
	public short Width, Height;
	public short AppWidth, AppHeight;
	public string WindowTitle;
	public bool HasBorder = true;
	public bool HasFocus = true;
	public bool IsMovable = true;
	public bool IsRoot = false;
	public short TitleAttr = 0x00f0;
	public short BorderAttr = 0x000f;
	public int WindowTitleOffset = 3;
	public int Counter = 0;
	public int CursorX = 0;
	public int CursorY = 0;
	public int CursorIndexAppBuffer = 0;

	public Window(string name, int x, int y, int w, int h) {
		WindowTitle = name;
		X = (short)(x < 0 ? 0 : x);
		Y = (short)(y < 0 ? 0 : y);
		Width = (short)(w < 3 ? 3 : w);
		Height = (short)(h < 3 ? 3 : h);
		WindowBufferSize = Width * Height;
		WindowBuffer = new Win32.CHAR_INFO[WindowBufferSize];
		AppWidth = (short)(Width - 2);
		AppHeight = (short)(Height - 2);
		AppBufferSize = AppWidth * AppHeight;
		AppBuffer = new Win32.CHAR_INFO[AppBufferSize];
		ClearChar.UnicodeChar = ' ';
		ClearChar.Attributes = 0x0000;
		CurrentChar.UnicodeChar = '█';
		CurrentChar.Attributes = 0x000f;
	}

	public void DrawBorder() {
		char Corner0, Corner1, Corner2, Corner3, Vert, Hori, HoriTitle;
		if (HasFocus)
		{
			Corner0 = '█';
			Corner1 = '█';
			Corner2 = '╚';
			Corner3 = '╝';
			Vert = '║';
			Hori = '═';
			HoriTitle = '█';
		}
		else
		{
			Corner0 = '┌';
			Corner1 = '┐';
			Corner2 = '└';
			Corner3 = '┘';
			Vert = '│';
			Hori = '─';
			HoriTitle = '─';
		}
		PutCharWindowBuffer(0, 0, Corner0, BorderAttr);
		PutCharWindowBuffer(Width - 1, 0, Corner1, BorderAttr);
		PutCharWindowBuffer(0, Height - 1, Corner2, BorderAttr);
		PutCharWindowBuffer(Width - 1, Height - 1, Corner3, BorderAttr);
		for (int x = 1; x < Width - 1; x++)
		{
			PutCharWindowBuffer(x, 0, HoriTitle, BorderAttr);
			PutCharWindowBuffer(x, Height - 1, Hori, BorderAttr);
		}
		for (int y = 1; y < Height - 1; y++)
		{
			PutCharWindowBuffer(0, y, Vert, BorderAttr);
			PutCharWindowBuffer(Width - 1, y, Vert, BorderAttr);
		}
	}

	public void PutCharWindowBuffer(int x, int y, char c = '█', Int16 attr = 0x000f)
	{
		WinConsole.CopyBufferMutex.WaitOne();
		Win32.CHAR_INFO Char = new();
		Char.UnicodeChar = c;
		Char.Attributes = attr;
		int i = y * Width + x;
		if (i < WindowBufferSize) WindowBuffer[i] = Char;
		WinConsole.CopyBufferMutex.ReleaseMutex();
	}

	public void PutCharAppBuffer(int x, int y, char c = '█', Int16 attr = 0x000f)
	{
		Win32.CHAR_INFO Char = new();
		Char.UnicodeChar = c;
		Char.Attributes = attr;
		int i = y * AppWidth + x;
		if (i < AppBufferSize) AppBuffer[i] = Char;
	}

	public void PutChar(char c, Int16 attr)
	{
		Win32.CHAR_INFO Char = new();
		Char.UnicodeChar = c;
		Char.Attributes = attr;
		AppBuffer[CursorIndexAppBuffer] = Char;
		if (++CursorIndexAppBuffer >= AppBufferSize)
		{
			CursorIndexAppBuffer -= AppWidth;
			ScrollAppBuffer();
		}
	}

	public void PrintStr(string s)
	{
		foreach (char c in s) { PutChar(c, CurrentChar.Attributes); }
	}

	public void WriteTitle() {
		for (int i = 0; i < WindowTitle.Length; i++) {
			int x = i + WindowTitleOffset;
			if (x < Width) PutCharWindowBuffer(x, 0, WindowTitle[i], TitleAttr);
		}
	}


	public void ClearWindowBuffer()	{for (int i = 0; i < WindowBufferSize; i++) WindowBuffer[i] = ClearChar;}

	public void ClearAppBuffer() {for (int i = 0; i < AppBufferSize; i++) AppBuffer[i] = ClearChar;}

	public void CopyAppBuffer()
	{
		int source_index = 0, dest_index = 0;
		WinConsole.CopyBufferMutex.WaitOne();
		for (int y = 0; y < AppHeight; y++)
		{
			for (int x = 0; x < AppWidth; x++)
			{
				source_index = y * AppWidth + x;
				dest_index = (y + 1) * Width + x + 1;
				if (dest_index < WindowBufferSize)
				{
					WindowBuffer[dest_index] = AppBuffer[source_index];
				}
			}
		}
		WinConsole.CopyBufferMutex.ReleaseMutex();
	}

	public void ScrollAppBuffer()
	{
		int source_index = 0, dest_index = 0;
		for (int y = 1; y < AppHeight; y++)
		{
			for (int x = 0; x < AppWidth; x++)
			{
				source_index = y * AppWidth + x;
				dest_index = (y - 1) * AppWidth + x;
				AppBuffer[dest_index] = AppBuffer[source_index];
			}
		}
		for (int x = 0; x < AppWidth; x++)
			AppBuffer[(AppHeight - 1) * AppWidth + x] = ClearChar;
	}

	public void Refresh() {
		if (HasBorder) DrawBorder();
		WriteTitle();
		// Wenn das Fenster in WinConsole.WindowList ist, wird es automatisch
		// in den ConsolenBuffer kopiert
		//WinConsole.CopyBuffer(this);
	}

	public void Start(ParameterizedThreadStart thread)
	{
		Thread app = new Thread(thread);
		app.Start(this);
	}

	public void Start()
	{
		Thread app = new Thread(Application);
		app.Start();
	}

	public void Application()
	{
		while (true)
		{
			Thread.Sleep(2);
			PutChar(DemoText[TextIndex], CurrentChar.Attributes);
			TextIndex = ++TextIndex < DemoText.Length  ? TextIndex : 0;
			CopyAppBuffer();
		}
	}
}

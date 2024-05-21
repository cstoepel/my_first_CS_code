using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win32_Console;

internal class Window {
	public Win32.CHAR_INFO[] WindowBuffer { get; set; }
	public Int32 WindowBufferSize;
	public Win32.CHAR_INFO[] AppBuffer { get; set; }
	public Int32 AppBufferSize;
	static public Win32.CHAR_INFO ClearChar;
	public Win32.CHAR_INFO Character;
	public string Text = " Hello I am a Window.                        Give me some text.               I want Text!                     Where is my Text to display??              Give me TEXT!!!!                   Without text I will reset myself.....                               RESETTING                                                                                                                                         ";
	public int TextIndex = 0;
	public int BuffIndex = 0;
	public short _X, _Y;
	public short X
	{
		get { return _X; }
		set { _X = value < (short)0 ? (short)0 : value >= WinConsole.Width - Width ? (short)(WinConsole.Width - Width): value; }
	}
	public short Y
	{
		get { return _Y; }
		set { _Y = value < (short)0 ? (short)0 : value >= WinConsole.Height - Height ? (short)(WinConsole.Height - Height) : value; }
	}
	public short  Width;
	public short  Height;
	public short AppWidth;
	public short AppHeight;
	public string Name;
	public bool   HasBorder      = true;
	public bool   HasFocus       = true;
	public bool   IsMovable      = true;
	public bool   IsRoot         = false;
	public char   BackgroundChar = ' ';
	public short  BackgroundAttr = 0x000f;
	public short  TitleAttr      = 0x00f0;
	//public short  TitleBarAttr   = 0x0005;
	public short  BorderAttr     = 0x000f;
	public int    TitleOffset    = 3;
	public int Counter = 0;

	public Window(string name, int x, int y, int w, int h) {
		Name = name;
		X      = (short)(x < 0 ? 0 : x);
		Y      = (short)(y < 0 ? 0 : y);
		Width  = (short)(w < 3 ? 3 : w);
		Height = (short)(h < 3 ? 3 : h);
		WindowBufferSize = Width * Height;
		WindowBuffer     = new Win32.CHAR_INFO[WindowBufferSize];
		AppWidth = (short)(Width - 2);
		AppHeight = (short)(Height - 2);
		AppBufferSize = AppWidth * AppHeight;
		AppBuffer = new Win32.CHAR_INFO[AppBufferSize];
		Character.UnicodeChar = '█';
		Character.Attributes = 0x000f;
	}

	public void DrawBorder() {
		char Corner0, Corner1, Corner2, Corner3, Vert, Hori, HoriTitle;
		if (HasFocus)
		{
			Corner0   = '█';
			Corner1   = '█';
			Corner2   = '╚';
			Corner3   = '╝';
			Vert      = '║';
			Hori      = '═';
			HoriTitle = '█';
		}
		else
		{
			Corner0   = '┌';
			Corner1   = '┐';
			Corner2   = '└';
			Corner3   = '┘';
			Vert      = '│';
			Hori      = '─';
			HoriTitle = '─';
		}
		WinConsole.SetPixel(this, 0,          0,         Corner0, BorderAttr);
		WinConsole.SetPixel(this, Width - 1,  0,         Corner1, BorderAttr);
		WinConsole.SetPixel(this, 0, Height - 1,         Corner2, BorderAttr);
		WinConsole.SetPixel(this, Width - 1, Height - 1, Corner3, BorderAttr);
		for (int x = 1; x < Width - 1; x++)	{
			WinConsole.SetPixel(this, x, 0,          HoriTitle, BorderAttr);
			WinConsole.SetPixel(this, x, Height - 1, Hori,      BorderAttr);
		}
		for (int y = 1; y < Height - 1; y++) {
			WinConsole.SetPixel(this, 0,         y, Vert, BorderAttr);
			WinConsole.SetPixel(this, Width - 1, y, Vert, BorderAttr);
		}
	}

	public void WriteTitle() {
		for (int i = 0; i < Name.Length; i++) {
			int x = i + TitleOffset;
			if (x < Width) WinConsole.SetPixel(this, x, 0, Name[i], TitleAttr);
		}
	}

	public void PutChar(int x, int y, char c = '█')
	{
		Character.UnicodeChar = c;
		int index = y * AppWidth + x;
		if ( index > AppBufferSize - 1) AppBuffer[index] = Character;
	}

	public void ClearWindowBuffer()	{
		ClearChar.UnicodeChar = BackgroundChar;
		ClearChar.Attributes  = BackgroundAttr;
		for (int i = 0; i < WindowBufferSize; i++) WindowBuffer[i] = ClearChar;
	}

	public void ClearAppBuffer()
	{
		ClearChar.UnicodeChar = BackgroundChar;
		ClearChar.Attributes = BackgroundAttr;
		for (int i = 0; i < AppBufferSize; i++) AppBuffer[i] = ClearChar;
	}

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

	public void Refresh() {
		//ClearWindowBuffer(); // <-- ersetzen mit Fensterinhalt-Refresh / Content-Buffer
		if (HasBorder) DrawBorder();
		WriteTitle();
		WinConsole.CopyBuffer(this);
	}

	public void Start()
	{
		Thread app = new Thread(this.Application);
		app.Start();
	}

	public void Application()
	{
		while (true)
		{
			Thread.Sleep(100);
			TextIndex = TextIndex >= Text.Length -1  ? 0 : TextIndex;
			if (BuffIndex >= AppBuffer.Length - 1)
			{
				BuffIndex = 0;
				TextIndex = 0;
				Thread.Sleep(3000);
				ClearAppBuffer();

			}
			Character.UnicodeChar = Text[TextIndex];
			AppBuffer[BuffIndex] = Character;
			TextIndex++;
			BuffIndex++;
			CopyAppBuffer();
		}
	}
}

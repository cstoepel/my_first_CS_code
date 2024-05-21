using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win32_Console;

internal class Window
{
	public Win32.CHAR_INFO[] CharInfoBuffer { get; set; }
	public Int32 CharInfoBufferSize;
	static public Win32.CHAR_INFO ClearChar;
	public short X;
	public short Y;
	public short Width;
	public short Height;
	public string Name;


	public Window(string name, int x, int y, int w, int h)
	{
		Name = name;
		X = (short)(x < 0 ? 0 : x);
		Y = (short)(y < 0 ? 0 : y);
		Width  = (short)(w < 3 ? 3 : w);
		Height = (short)(h < 3 ? 3 : h);
		//Console.WriteLine($"{Width} x {Height}"); return;
		CharInfoBufferSize = Width * Height;
		CharInfoBuffer = new Win32.CHAR_INFO[CharInfoBufferSize];
	}

	public void DrawBorder()
	{
		WinConsole.SetPixel(this, 0,         0,          '╔', 0x0002);
		WinConsole.SetPixel(this, Width -1 , 0,          '╗', 0x0002);
		WinConsole.SetPixel(this, 0,         Height -1,  '╚', 0x0002);
		WinConsole.SetPixel(this, Width - 1, Height - 1, '╝', 0x0002);
		for (int x = 1; x < Width - 1; x++)
		{
			WinConsole.SetPixel(this, x, 0, x - 1 < Name.Length ? Name[x - 1] : '═', 0x0002);
			WinConsole.SetPixel(this, x, Height - 1, '═', 0x0002);
		}
		for (int y = 1; y < Height - 1; y++)
		{
			WinConsole.SetPixel(this, 0,         y, '║', 0x0002);
			WinConsole.SetPixel(this, Width - 1, y, '║', 0x0002);
		}
	}

	public void Clear()
	{
		ClearChar.UnicodeChar = '#';
		ClearChar.Attributes = 0x0005;
		for (int i = 0; i < CharInfoBufferSize; i++) CharInfoBuffer[i] = ClearChar;
	}

	public void Refresh()
	{
		Clear();
		DrawBorder();
		WinConsole.CopyBuffer(this);
	}
}

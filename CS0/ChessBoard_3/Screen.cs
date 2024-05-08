using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoard;

internal class ConsoleScreen
{
	public ConsoleScreen()
	{
		Width = Console.WindowWidth;
		Height = Console.WindowHeight;
		Console.ResetColor();
	}

	public ConsoleScreen(int width, int height, ConsoleColor fg = ConsoleColor.DarkGray, ConsoleColor bg = ConsoleColor.Black)
	{
		Width = width;
		Height = height;
		Console.SetWindowSize(width, height);
		Console.ForegroundColor = fg;
		Console.BackgroundColor = bg;
	}

	public int Width { get; }
	public int Height { get; }
	public void SetFGColor(ConsoleColor c) { Console.ForegroundColor = c;}
	public void SetBGColor(ConsoleColor c) { Console.BackgroundColor = c; }
	public void SetFGColor(byte r, byte g, byte b) { Console.Write("\x1b[38;2;" + r.ToString() + ";" + g.ToString() + ";" + b.ToString() + "m");}
	public void SetBGColor(byte r, byte g, byte b) { Console.Write( "\x1b[48;2;" + r.ToString() + ";" + g.ToString() + ";" + b.ToString() + "m");}
	public void Clear() { Console.Clear(); }
	public void ResetColor() { Console.ResetColor(); }
}

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
		ForeGround = fg;
		BackGround = bg;
		Console.SetWindowSize(width, height);
		Console.ResetColor();
		Console.ForegroundColor = ForeGround;
		Console.BackgroundColor = BackGround;

	}

	public int Width { get; }
	public int Height { get; }
	private ConsoleColor _ForeGround;
	private ConsoleColor _BackGround;
	public ConsoleColor ForeGround
	{
		get
		{
			return _ForeGround;
		}
		set
		{
			_ForeGround = value;
			Console.ForegroundColor = ForeGround;
		}
	}
	public ConsoleColor BackGround
	{
		get
		{
			return _BackGround;
		}
		set
		{
			_BackGround = value;
			Console.BackgroundColor = BackGround;
		}
	}
	private RGBColor _ForeGroundRGB;
		public RGBColor ForeGroundRGB 
		{ 
			get => _ForeGroundRGB; 
			set
			{
				_ForeGroundRGB = value;
				const string CSI = "\x1b[";
				const string SeqEnd = "0m";
				const string RGBColor = CSI + "38;2;";
				Console.Write(
					RGBColor +
					_ForeGroundRGB.r.ToString() + ";" + 
					_ForeGroundRGB.g.ToString() + ";" + 
					_ForeGroundRGB.b.ToString() + 
					SeqEnd
				);
		}
	}
		public void Clear() { Console.Clear(); }
		public void ResetColor() { Console.ResetColor(); }
}

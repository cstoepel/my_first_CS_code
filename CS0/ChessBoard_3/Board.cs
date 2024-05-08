using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoard;

internal class Board
{
	public int BoardDimension;
	List<ChessField> Fields = new List<ChessField>();
	public ConsoleScreen Screen;
	public RGBColor BlackColor = new RGBColor() { r = 0x44, g = 0x44, b = 0x44 };
	public RGBColor WhiteColor = new RGBColor() { r = 0x77, g = 0x77, b = 0x77 };
	public Board(ConsoleScreen screen, int n)
	{
		Screen = screen;
		BoardDimension = n;
		for (int x = 0; x < n; x++)
		{
			for (int y = 0; y < n; y++)
			{
				Fields.Add(
					new ChessField
					{
						Screen = screen,
						d = 12,
						ix = x,
						iy = y,
						fg = { r = 0, g = 0, b = 0 },
						bg = ((x + y) % 2 == 0) ? WhiteColor : BlackColor
						}
					);
			}
		}
	}

	public void Draw()
	{
		foreach (ChessField field in Fields) { field.Draw(); }
		Screen.ResetColor();
	}

	public void Move(string MoveCode)
	{
		// a2a4, b1c3, ...
		// xyxy
		// 0011
		int y0 = Decode(MoveCode[0]);
		int x0 = Decode(MoveCode[1]);
		int y1 = Decode(MoveCode[2]);
		int x1 = Decode(MoveCode[3]);

		int a = BoardDimension * y0 + x0;
		int b = BoardDimension * y1 + x1;

		Fields[b].p = Fields[a].p;
		Fields[a].p = null;
		Fields[a].Draw();
		Fields[b].Draw();
	}

	private int Decode(char c)
		{
			switch (c)
			{
				case 'a': return 0;
				case 'b': return 1;
				case 'c': return 2;
				case 'd': return 3;
				case 'e': return 4;
				case 'f': return 5;
				case 'g': return 6;
				case 'h': return 7;
				case '1': return 7;
				case '2': return 6;
				case '3': return 5;
				case '4': return 4;
				case '5': return 3;
				case '6': return 2;
				case '7': return 1;
				case '8': return 0;
				default: return 0;
			}
		}

	public void PutChessPiece(ChessPiece p, int x, int y) { Fields[BoardDimension * y + x].p = p; }
	public ChessPiece GetChessPiece(int x, int y) { return Fields[BoardDimension * y + x].p; }
}

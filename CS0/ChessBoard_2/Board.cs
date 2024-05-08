using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoard;

internal class Board
{
	public int N;
	//ChessField cf = new ChessField();
	List<ChessField> Fields = new List<ChessField>();
	public ConsoleScreen Screen;
	public Board(ConsoleScreen screen, int n)
	{
		Screen = screen;
		N = n;
		for (int x = 0; x < n; x++)
		{
			for (int y = 0; y < n; y++)
			{
				//Console.Write($"{x},{y}  ");
				Fields.Add(
					new ChessField
						{
							Screen = screen,
							d = 12,
							ix = x,
							iy = y,
							FGColor = ConsoleColor.DarkGreen,
							BGColor = ((x + y) % 2 == 0) ? ConsoleColor.Gray : ConsoleColor.Black
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
		int y0 = MoveCode2Int(MoveCode[0]);
		int x0 = MoveCode2Int(MoveCode[1]);
		int y1 = MoveCode2Int(MoveCode[2]);
		int x1 = MoveCode2Int(MoveCode[3]);

		int a = N * x0 + y0;
		int b = N * x1 + y1;
		//Console.Write($" von:{a}\n");
		//Console.Write($"nach:{b}\n");
		PutChessPiece(GetChessPiece(x0, y0), x1, y1);
		PutChessPiece(null, x0, y0);
		Fields[N * y0 + x0].Draw();
		Fields[N * y1 + x1].Draw();

	}

	private int MoveCode2Int(char c)
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

	public void PutChessPiece(ChessPiece p, int x, int y)
	{
		Fields[N * y + x].p = p;
	}

	public ChessPiece GetChessPiece(int x, int y)
	{
		return Fields[N * y + x].p;
	}
}

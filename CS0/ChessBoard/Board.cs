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

	public void SetChessPiece(ChessPiece p, int x, int y)
	{
		int Index = y * N + x;
		if (Fields[Index].p == null) Fields[Index].p = p;
	}
}

﻿using System.Reflection;

namespace ChessBoard;

// Console Eigenschaften:
// Rasterfont 8x8
// Window Size: Width > 100, Height > 100

internal class Program
{
	static void Main(string[] args)
	{
		ConsoleScreen screen = new ConsoleScreen(100, 100);

		Board board = new Board(screen, 8);

		RGBColor WhiteColor =	new RGBColor { r = 0xf0, g = 0xf0, b = 0xf0};
		RGBColor BlackColor =	new RGBColor { r = 0x00, g = 0x00, b = 0x10};

		ChessPiece BlackPawn   = new ChessPiece { fg = WhiteColor, bitmap = PiecesBitmaps.Pawn };
		ChessPiece BlackKnight = new ChessPiece { fg = WhiteColor, bitmap = PiecesBitmaps.Knight };
		ChessPiece BlackBishop = new ChessPiece { fg = WhiteColor, bitmap = PiecesBitmaps.Bishop };
		ChessPiece BlackRook   = new ChessPiece { fg = WhiteColor, bitmap = PiecesBitmaps.Rook };
		ChessPiece BlackQueen  = new ChessPiece { fg = WhiteColor, bitmap = PiecesBitmaps.Queen };
		ChessPiece BlackKing   = new ChessPiece { fg = WhiteColor, bitmap = PiecesBitmaps.King };

		ChessPiece WhitePawn   = new ChessPiece { fg = BlackColor, bitmap = PiecesBitmaps.Pawn };
		ChessPiece WhiteKnight = new ChessPiece { fg = BlackColor, bitmap = PiecesBitmaps.Knight };
		ChessPiece WhiteBishop = new ChessPiece { fg = BlackColor, bitmap = PiecesBitmaps.Bishop };
		ChessPiece WhiteRook   = new ChessPiece { fg = BlackColor, bitmap = PiecesBitmaps.Rook };
		ChessPiece WhiteQueen  = new ChessPiece { fg = BlackColor, bitmap = PiecesBitmaps.Queen };
		ChessPiece WhiteKing   = new ChessPiece { fg = BlackColor, bitmap = PiecesBitmaps.King };

		board.PutChessPiece(BlackRook   ,0 , 0);
		board.PutChessPiece(BlackKnight ,0 , 1);
		board.PutChessPiece(BlackBishop ,0 , 2);
		board.PutChessPiece(BlackQueen  ,0 , 3);
		board.PutChessPiece(BlackKing   ,0 , 4);
		board.PutChessPiece(BlackBishop ,0 , 5);
		board.PutChessPiece(BlackKnight ,0 , 6);
		board.PutChessPiece(BlackRook   ,0 , 7);

		board.PutChessPiece(BlackPawn   ,1 , 0);
		board.PutChessPiece(BlackPawn   ,1 , 1);
		board.PutChessPiece(BlackPawn   ,1 , 2);
		board.PutChessPiece(BlackPawn   ,1 , 3);
		board.PutChessPiece(BlackPawn   ,1 , 4);
		board.PutChessPiece(BlackPawn   ,1 , 5);
		board.PutChessPiece(BlackPawn   ,1 , 6);
		board.PutChessPiece(BlackPawn   ,1 , 7);

		board.PutChessPiece(WhitePawn   ,6 , 0);
		board.PutChessPiece(WhitePawn   ,6 , 1);
		board.PutChessPiece(WhitePawn   ,6 , 2);
		board.PutChessPiece(WhitePawn   ,6 , 3);
		board.PutChessPiece(WhitePawn   ,6 , 4);
		board.PutChessPiece(WhitePawn   ,6 , 5);
		board.PutChessPiece(WhitePawn   ,6 , 6);
		board.PutChessPiece(WhitePawn   ,6 , 7);

		board.PutChessPiece(WhiteRook   ,7 , 0);
		board.PutChessPiece(WhiteKnight ,7 , 1);
		board.PutChessPiece(WhiteBishop ,7 , 2);
		board.PutChessPiece(WhiteQueen  ,7 , 3);
		board.PutChessPiece(WhiteKing   ,7 , 4);
		board.PutChessPiece(WhiteBishop ,7 , 5);
		board.PutChessPiece(WhiteKnight ,7 , 6);
		board.PutChessPiece(WhiteRook   ,7 , 7);

		board.Draw();
		
		string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

		string[] lines = File.ReadAllLines(projectDirectory + "\\Data\\spassky-fischer-1_1972.txt");

		foreach (string line in lines)
		{
			ConsoleKeyInfo key = Console.ReadKey(true);
			if (key.Key == ConsoleKey.Escape || line[0] == '-') break;
			board.Move(line);
		}
	}
}

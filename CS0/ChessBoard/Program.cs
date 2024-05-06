namespace ChessBoard;

internal class Program
{
	static void Main(string[] args)
	{
		ConsoleScreen screen = new ConsoleScreen(120, 120);

		Board b = new Board(screen, 8);

		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.DarkRed, bitmap = PiecesBitmaps.Rook   }, 0, 0);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.DarkRed, bitmap = PiecesBitmaps.Knight }, 0, 1);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.DarkRed, bitmap = PiecesBitmaps.Bishop }, 0, 2);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.DarkRed, bitmap = PiecesBitmaps.Queen  }, 0, 3);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.DarkRed, bitmap = PiecesBitmaps.King   }, 0, 4);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.DarkRed, bitmap = PiecesBitmaps.Bishop }, 0, 5);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.DarkRed, bitmap = PiecesBitmaps.Knight }, 0, 6);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.DarkRed, bitmap = PiecesBitmaps.Rook   }, 0, 7);

		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.DarkRed, bitmap = PiecesBitmaps.Pawn   }, 1, 0);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.DarkRed, bitmap = PiecesBitmaps.Pawn   }, 1, 1);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.DarkRed, bitmap = PiecesBitmaps.Pawn   }, 1, 2);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.DarkRed, bitmap = PiecesBitmaps.Pawn   }, 1, 3);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.DarkRed, bitmap = PiecesBitmaps.Pawn   }, 3, 4);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.DarkRed, bitmap = PiecesBitmaps.Pawn   }, 1, 5);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.DarkRed, bitmap = PiecesBitmaps.Pawn   }, 1, 6);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.DarkRed, bitmap = PiecesBitmaps.Pawn   }, 1, 7);

		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.Green, bitmap = PiecesBitmaps.Pawn  }, 6, 0);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.Green, bitmap = PiecesBitmaps.Pawn  }, 6, 1);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.Green, bitmap = PiecesBitmaps.Pawn  }, 6, 2);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.Green, bitmap = PiecesBitmaps.Pawn  }, 6, 3);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.Green, bitmap = PiecesBitmaps.Pawn  }, 4, 4);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.Green, bitmap = PiecesBitmaps.Pawn  }, 6, 5);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.Green, bitmap = PiecesBitmaps.Pawn  }, 6, 6);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.Green, bitmap = PiecesBitmaps.Pawn  }, 6, 7);

		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.Green, bitmap = PiecesBitmaps.Rook  }, 7, 0);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.Green, bitmap = PiecesBitmaps.Knight}, 5, 2);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.Green, bitmap = PiecesBitmaps.Bishop}, 7, 2);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.Green, bitmap = PiecesBitmaps.Queen }, 7, 3);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.Green, bitmap = PiecesBitmaps.King  }, 7, 4);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.Green, bitmap = PiecesBitmaps.Bishop}, 7, 5);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.Green, bitmap = PiecesBitmaps.Knight}, 7, 6);
		b.SetChessPiece(new ChessPiece { Dim = 12, ForeGround = ConsoleColor.Green, bitmap = PiecesBitmaps.Rook  }, 7, 7);

		b.Draw();
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoard;

internal class ChessPiece
{
	public int Dim;  // Dimensionen der quadratischen Bitmap der Schachfigur Dim x Dim
	public ConsoleColor ForeGround = ConsoleColor.Magenta;
	public string bitmap;
}

internal static class PiecesBitmaps{
	// 12 x 12 Bitmaps (Char Maps)
	public const string Pawn   = 
		"            " +
		"            " +
		"            " +
		"            " +
		"    ████    " +
		"   ██████   " +
		"   ██████   " +
		"    ████    " +
		"    ████    " +
		"  ████████  " +
		" ██████████ " +
		"            "
		;
	public const string Knight =
		"            " +
		"        █   " +
		"   ███████  " +
		"  █████████ " +
		" ███ ██████ " +
		" ██ ███████ " +
		"   ████████ " +
		"  █████████ " +
		"  ████████  " +
		" ██████████ " +
		" ██████████ " +
		"            "
		;

	public const string Bishop =
		"            " +
		"     █      " +
		"    ██ █    " +
		"   ███ ██   " +
		"   ██████   " +
		"    ████    " +
		"     ██     " +
		"    ████    " +
		"  ████████  " +
		" ██████████ " +
		" ██████████ " +
		"            "
		;

	public const string Rook   =
		"            " +
		"            " +
		"            " +
		" ██ ████ ██ " +
		" ██████████ " +
		"   ██████   " +
		"   ██████   " +
		"   ██████   " +
		"   ██████   " +
		" ██████████ " +
		" ██████████ " +
		"            "
		;

	public const string Queen  =
		"            " +
		" █   ██   █ " +
		"  ████████  " +
		"   ██████   " +
		"     ██     " +
		"   ██████   " +
		"     ██     " +
		"    ████    " +
		"  ████████  " +
		" ██████████ " +
		" ██████████ " +
		"            "
		;

	public const string King   =
		"            " +
		"     ██     " +
		"   ██████   " +
		"     ██     " +
		"  ████████  " +
		"  ████████  " +
		"     ██     " +
		"    ████    " +
		"  ████████  " +
		" ██████████ " +
		" ██████████ " +
		"            "
		;

}

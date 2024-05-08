using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoard;

internal class ChessPiece
{
	public ChessPiece()
	{
		fg = new RGBColor() { r = 0xff, g = 0xff, b = 0xff };
		bg = new RGBColor() { r = 0x00, g = 0x00, b = 0x00 };
	}
	public int Dim = 12;  // Dimensionen der quadratischen Bitmap der Schachfigur Dim x Dim
	public RGBColor fg;
	public RGBColor bg;
	public string bitmap;
}

internal struct RGBColor
{
	public byte r, g, b;
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

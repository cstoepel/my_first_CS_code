using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoard;
internal static class Pixel
{
	static public void SetPixel(ConsoleScreen cs, int x, int y, char character = ' ')
	{
		if (x < 0 || y < 0) return;
		if (x > cs.Width - 1 || y > cs.Height - 1) return;
		Console.SetCursorPosition(x, y);
		Console.Write(character);
	}

}

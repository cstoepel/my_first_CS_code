﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoard;

internal class ChessField
{
	public ChessField() { }
	public ConsoleScreen Screen;
	public RGBColor fg, bg;
	public int ix, iy, d;
	public ChessPiece p;
	public void Draw()
	{
		Screen.SetBGColor(bg.r, bg.g, bg.b);
		if (p != null) Screen.SetFGColor(p.fg.r, p.fg.g, p.fg.b);
		for (int y = 0; y < d;  y++)
		{
			for(int x = 0; x < d; x++)
			{
				char c = ' ';
				if (p != null) c = p.bitmap[y * d + x];
				Pixel.SetPixel(Screen, x + d * ix, y + d * iy, c);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Screen_3
{
	internal class Line : Shape
	{
		public Line() { }
		public int _X1, _Y1, _X2, _Y2;
		public int X1 { get { return _X1; } set { _X1 = value < 0 ? 0 : value > Screen.Width - 1 ? Screen.Width : value; } }
		public int Y1 { get { return _Y1; } set { _Y1 = value < 0 ? 0 : value > Screen.Width - 1 ? Screen.Width : value; } }
		public int X2 { get { return _X2; } set { _X2 = value < 0 ? 0 : value > Screen.Width - 1 ? Screen.Width : value; } }
		public int Y2 { get { return _Y2; } set { _Y2 = value < 0 ? 0 : value > Screen.Width - 1 ? Screen.Width : value; } }
		public ConsoleScreen Screen { get; set; }
		internal override void Move(int dx, int dy)
		{
			X1 += dx;
			Y1 += dy;
			X2 += dx;
			Y2 += dy;
		}
		public void Draw(int x1, int y1, int x2, int y2)
		{
			X1 = x1;
			Y1 = y1;
			X2 = x2;
			Y2 = y2;
			Draw();
		}
		// Bresenham's line algorithm
		// https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm
		internal override void Draw()
		{
			Screen.ForeGround = color;
			DrawLine(X1, Y1,X2,Y2);
		}
		internal void DrawLine(int x0, int y0, int x1, int y1)
		{
			int dx = Math.Abs(x1 - x0);
			int sx = x0 < x1 ? 1 : -1;
			int dy = -Math.Abs(y1 - y0);
			int sy = y0 < y1 ? 1 : -1;
			int err = dx - dy;

			while (true)
			{
				Pixel.SetPixel(Screen, x0, y0);
				if (x0 == x1 && y0 == y1) break;
				int e2 = 2 * err;
				if (e2 >= dy)
				{
					if ( x0 == x1) break;
					err = err + dy;
					x0 = x0 + sx;
				}
				if (e2 <= dx)
				{
					if (y0 == y1) break;
					err = err + dx;
					y0 = y0 + sy;
				}
			}
		}
	}
}

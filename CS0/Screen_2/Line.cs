using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Screen_2
{
	internal class Line
	{
		public Line() { }
		public int X1 { get; set; }
		public int Y1 { get; set; }
		public int X2 { get; set; }
		public int Y2 { get; set; }
		public ConsoleScreen Screen { get; set; }
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
		public void Draw()
		{
			int m = 2 * (Y2 - Y1);
			int err = m - (X2 - X1);
			for (int x = X1, y = Y1; x <= X2; ++x)
			{
				Pixel.SetPixel(Screen, x, y);
				err += m;
				if (err >= 0)
				{
					++y;
					err = err - 2 * (X2 - X1);
				}
			}
		}
		//private void SetPixel(int x, int y)
		//{
		//	if (x < 0 || y < 0) return;
		//	if (x > Screen.Width - 1 || y > Screen.Height - 1) return;
		//	Console.SetCursorPosition(y, x);
		//	Console.Write("o");
		//}

	}
}

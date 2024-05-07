using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screen_3
{
	internal class Circle : Shape
	{
		public Circle() { }
		public int _X;
		public int _Y;
		public int _R;
		public int X { get { return _X; } set { _X = value < 0 ? 0 : value > Screen.Width - 1 ? Screen.Width : value; } }
		public int Y { get { return _Y; } set { _Y = value < 0 ? 0 : value > Screen.Height - 1 ? Screen.Height : value; } }
		public int R { get { return _R; } set { _R = value < 1 ? 1 : value > 100 ? 100 : value; } }
		public ConsoleScreen Screen { get; set; }
		internal override void Move(int dx, int dy)
		{
			X += dx;
			Y += dy;
		}
		internal override void Draw() {  Draw(X, Y, R); }
		public void Draw(int xc, int yc, int r)
		{
			Screen.ForeGround = color;
			// Midpoint circle algorithm
			// Jesko's Method (x und y vertauscht, aber egal weil symetrisch)
			// https://en.wikipedia.org/wiki/Midpoint_circle_algorithm

			int x = 0, y = r;
			int d = 3 - 2 * r;
			DrawOctant(xc, yc, x, y);
			while (y >= x)
			{
				x++;
				if (d > 0)
				{
					y--;
					d = d + 4 * (x - y) + 10;
				}
				else
				{
					d = d + 4 * x + 6;
				}
				DrawOctant(xc, yc, x, y);
			}
		}
		private void DrawOctant(int xc, int yc, int x, int y)
		{
			Pixel.SetPixel(Screen, xc + x, yc + y);
			Pixel.SetPixel(Screen, xc - x, yc + y);
			Pixel.SetPixel(Screen, xc + x, yc - y);
			Pixel.SetPixel(Screen, xc - x, yc - y);
			Pixel.SetPixel(Screen, xc + y, yc + x);
			Pixel.SetPixel(Screen, xc - y, yc + x);
			Pixel.SetPixel(Screen, xc + y, yc - x);
			Pixel.SetPixel(Screen, xc - y, yc - x);
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

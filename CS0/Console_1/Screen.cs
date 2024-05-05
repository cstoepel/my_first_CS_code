using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screen_1
{
	internal class ConsoleScreen
	{
		public ConsoleScreen() {
			Width = Console.WindowWidth;
			Height = Console.WindowHeight;
			Console.ResetColor();
		}

		public ConsoleScreen(int width, int height)
		{
			Width = width;
			Height = height;
			Console.SetWindowSize(width, height);
			Console.ResetColor();

		}

		public int Width { get; }
		public int Height { get; }

		// Midpoint circle algorithm
		// Jesko's Method (x und y vertauscht, aber egal weil symetrisch)
		// https://en.wikipedia.org/wiki/Midpoint_circle_algorithm
		public void DrawCricle(int xc, int yc, int r)
		{
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
		void DrawOctant(int xc, int yc, int x, int y)
		{
			SetPixel(xc + x, yc + y);
			SetPixel(xc - x, yc + y);
			SetPixel(xc + x, yc - y);
			SetPixel(xc - x, yc - y);
			SetPixel(xc + y, yc + x);
			SetPixel(xc - y, yc + x);
			SetPixel(xc + y, yc - x);
			SetPixel(xc - y, yc - x);
		}

		public void SetPixel(int x, int y)
		{
			if (x < 0 || y < 0) return;
			if (x > Width - 1 || y > Height - 1) return;
			Console.SetCursorPosition(y, x);
			Console.Write("o");
		}

		// Bresenham's line algorithm
		// https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm
		public void DrawLine(int x1, int y1, int x2, int y2)
		{
			int m = 2 * (y2 - y1);
			int err = m - (x2 - x1);
			for (int x = x1, y = y1; x <= x2; ++x)
			{
				SetPixel(x, y);
				err += m;
				if (err >= 0)
				{
					++y;
					err = err - 2 * (x2 - x1);
				}
			}
		}
	}
} 


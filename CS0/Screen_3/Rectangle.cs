using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screen_3
{
	internal class Rectangle : Shape
	{
		public Rectangle() { }
		public int _X1, _Y1, _X2, _Y2;
		public int X1 { get { return _X1; } set { _X1 = value < 0 ? 0 : value > Screen.Width - 1 ? Screen.Width : value; } }
		public int Y1 { get { return _Y1; } set { _Y1 = value < 0 ? 0 : value > Screen.Width - 1 ? Screen.Width : value; } }
		public int X2 { get { return _X2; } set { _X2 = value < 0 ? 0 : value > Screen.Width - 1 ? Screen.Width : value; } }
		public int Y2 { get { return _Y2; } set { _Y2 = value < 0 ? 0 : value > Screen.Width - 1 ? Screen.Width : value; } }

		public ConsoleScreen Screen;
		internal override void Move(int dx, int dy)
		{
			X1 += dx;
			Y1 += dy;
			X2 += dx;
			Y2 += dy;
		}
		internal override void Draw()
		{
			Screen.ForeGround = color;
			if (Screen == null) return;
			for (int x = X1; x <= X2; x++)
			{
				Pixel.SetPixel(Screen, x, Y1);
				Pixel.SetPixel(Screen, x, Y2); 
			}
			for (int y = Y1; y <= Y2; y++)
			{
				Pixel.SetPixel(Screen, X1, y);
				Pixel.SetPixel(Screen, X2, y);
			}
		}

	}
}

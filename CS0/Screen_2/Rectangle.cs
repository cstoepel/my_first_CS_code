using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screen_2
{
	internal class Rectangle
	{
		public Rectangle() { }
		public int X1 { get; set; }
		public int Y1 { get; set; }
		public int X2 { get; set; }
		public int Y2 { get; set; }

		public ConsoleScreen Screen;
		public void Draw()
		{
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

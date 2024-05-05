namespace Screen_2
{
	// Console RasterFont 8x8 !
	internal class Program
	{
		static void Main(string[] args)
		{
			int maxX = 100, maxY = 100;

			ConsoleScreen screen = new ConsoleScreen(maxX, maxY);
			Console.Write($"screen.Width  = {screen.Width}\n");
			Console.Write($"screen.Height = {screen.Height}\n");

			Circle c1 = new Circle() { Screen = screen, X = 30, Y = 20, R =  9 };
			Circle c2 = new Circle() { Screen = screen, X = 60, Y = 50, R = 15 };
			Circle c3 = new Circle() { Screen = screen, X = 50, Y = 70, R = 23 };

			Line l1 = new Line() { X1 = 10, Y1 = 10, X2 = 60, Y2 = 40, Screen = screen };

			Rectangle r1 = new Rectangle() { X1 = 20, Y1 = 30, X2 = 80, Y2 = 50, Screen = screen };
			Rectangle r2 = new Rectangle() { X1 = 50, Y1 = 20, X2 = 70, Y2 = 90, Screen = screen };

			c1.Draw();
			c2.Draw();

			screen.ForeGround = ConsoleColor.Green;
			c3.Draw();
			screen.ForeGround= ConsoleColor.Red;
			l1.Draw();
			r1.Draw();
			screen.ForeGround = ConsoleColor.Blue;
			r2.Draw();

			ConsoleKeyInfo key;
			do
			{
				key = Console.ReadKey(true);
				if (key.Key == ConsoleKey.LeftArrow)      { c3.X--; screen.Clear(); c3.Draw(); }
				else if (key.Key == ConsoleKey.RightArrow){ c3.X++; screen.Clear(); c3.Draw(); }
				else if (key.Key == ConsoleKey.UpArrow)   { c3.Y--; screen.Clear(); c3.Draw(); }
				else if (key.Key == ConsoleKey.DownArrow) { c3.Y++; screen.Clear(); c3.Draw(); }
				else if (key.Key == ConsoleKey.Subtract)  { c3.R--; screen.Clear(); c3.Draw(); }
				else if (key.Key == ConsoleKey.Add)       { c3.R++; screen.Clear(); c3.Draw(); }


			} while (key.Key != ConsoleKey.Escape);
		}
	}
}


// Console RasterFont 8x8 !
using Screen_3;

internal class Program
{
	private static void Main(string[] args)
	{
		int maxX = 100, maxY = 100;

		ConsoleScreen screen = new ConsoleScreen(maxX, maxY);

		Shapes Zeichnung = new Shapes();
		// Screen zuerst in Initialisierungsliste, weil gebraucht von get und set
		// Ist das gut so eine Reihenfolgenbeschränkung zu haben? 
		Zeichnung.Add(new Rectangle() { Screen = screen, color = ConsoleColor.Gray, X1 = 10, Y1 = 60, X2 = 60, Y2 = 98 });
		Zeichnung.Add(new Rectangle() { Screen = screen, color = ConsoleColor.Gray, X1 = 20, Y1 = 70, X2 = 30, Y2 = 80 });
		Zeichnung.Add(new Rectangle() { Screen = screen, color = ConsoleColor.Gray, X1 = 35, Y1 = 70, X2 = 50, Y2 = 98 });

		Zeichnung.Add(new Line() { Screen = screen, color = ConsoleColor.DarkRed, X1 = 35, Y1 = 40, X2 = 62, Y2 = 61 });
		Zeichnung.Add(new Line() { Screen = screen, color = ConsoleColor.Red, X1 = 35, Y1 = 40, X2 = 8, Y2 = 61 });

		Zeichnung.Add(new Rectangle() { Screen = screen, color = ConsoleColor.Gray, X1 = 70, Y1 = 70, X2 = 72, Y2 = 98 });
		Zeichnung.Add(new Circle() { Screen = screen, color = ConsoleColor.Green, X = 71, Y = 50, R = 20 });
		Zeichnung.Add(new Circle() { Screen = screen, color = ConsoleColor.Green, X = 71, Y = 50, R = 19 });
		Zeichnung.Add(new Circle() { Screen = screen, color = ConsoleColor.Green, X = 71, Y = 50, R = 18 });

		Zeichnung.Add(new Circle() { Screen = screen, color = ConsoleColor.Yellow, X = 80, Y = 20, R = 6 });

		Zeichnung.DrawAll();

		ConsoleKeyInfo key;
		int index = 0;
		do
		{
			key = Console.ReadKey(true);
			//Console.Write($"index={index}");
			switch (key.Key)
			{
				case ConsoleKey.D0: index = 0; break;
				case ConsoleKey.D1: index = 1; break;
				case ConsoleKey.D2: index = 2; break;
				case ConsoleKey.D3: index = 3; break;
				case ConsoleKey.D4: index = 4; break;
				case ConsoleKey.D5: index = 5; break;
				case ConsoleKey.D6: index = 6; break;
				case ConsoleKey.D7: index = 7; break;
				case ConsoleKey.D8: index = 8; break;
				case ConsoleKey.D9: index = 9; break;
				case ConsoleKey.LeftArrow: Zeichnung.GetShapeByIndex(index).Move(-1, 0); break;
				case ConsoleKey.RightArrow: Zeichnung.GetShapeByIndex(index).Move(1, 0); break;
				case ConsoleKey.UpArrow: Zeichnung.GetShapeByIndex(index).Move(0, -1); break;
				case ConsoleKey.DownArrow: Zeichnung.GetShapeByIndex(index).Move(0, 1); break;
				//case ConsoleKey.Subtract):
				//case ConsoleKey.Add):
				default: continue;
			}
			screen.Clear();
			Zeichnung.DrawAll();
		} while (key.Key != ConsoleKey.Escape);
	}
}
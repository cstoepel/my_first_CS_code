
// Console RasterFont 8x8 !
using Screen_3;

int maxX = 100, maxY = 100;

ConsoleScreen screen = new ConsoleScreen(maxX, maxY);

// Screen zuerst in Initialisierungsliste, weil gebraucht von get und set
// Ist das gut so eine Reihenfolgenbeschränkung zu haben?
Circle c1 = new Circle() { Screen = screen, color = ConsoleColor.Green,  X = 30, Y = 20, R = 9 };
Circle c2 = new Circle() { Screen = screen, color = ConsoleColor.Red, X = 60, Y = 50, R = 15 };
Circle c3 = new Circle() { Screen = screen, color = ConsoleColor.DarkGreen, X = 50, Y = 70, R = 23 };
Line l1 = new Line() { Screen = screen, color = ConsoleColor.DarkBlue, X1 = 10, Y1 = 10, X2 = 60, Y2 = 40 };
Rectangle r1 = new Rectangle() { Screen = screen, color = ConsoleColor.Blue, X1 = 20, Y1 = 30, X2 = 80, Y2 = 50 };
Rectangle r2 = new Rectangle() { Screen = screen, color = ConsoleColor.Yellow, X1 = 50, Y1 = 20, X2 = 70, Y2 = 90 };

Shapes Zeichnung = new Shapes();
//                    Index
Zeichnung.Add(c1); // 0
Zeichnung.Add(c2); // 1
Zeichnung.Add(c3); // 2
Zeichnung.Add(r1); // 3
Zeichnung.Add(r2); // 4
Zeichnung.Add(l1); // 5

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
		case ConsoleKey.LeftArrow:   Zeichnung.GetShapeByIndex(index).Move(-1,  0); break;
		case ConsoleKey.RightArrow:	 Zeichnung.GetShapeByIndex(index).Move( 1,  0); break;
		case ConsoleKey.UpArrow:	 Zeichnung.GetShapeByIndex(index).Move( 0, -1); break;
		case ConsoleKey.DownArrow:	 Zeichnung.GetShapeByIndex(index).Move( 0,  1); break;
		//case ConsoleKey.Subtract):
		//case ConsoleKey.Add):
		default: continue;
	}
	screen.Clear();
	Zeichnung.DrawAll();
} while (key.Key != ConsoleKey.Escape);

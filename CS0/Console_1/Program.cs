namespace Screen_1
{
	// Console RasterFont 8x8 !
	internal class Program
	{
		static void Main(string[] args)
		{
			int maxX = 100, maxY = 100;
			int cx = maxX / 2, cy = maxY / 2, r = maxX / 2 - 2;

			ConsoleScreen screen = new ConsoleScreen(maxX, maxY);
			Console.Write($"screen.Width  = {screen.Width}\n");
			Console.Write($"screen.Height = {screen.Height}\n");

			screen.DrawCricle(cx, cy, r);
			//screen.DrawLine(0, cy, maxY, 200);

			Console.ReadKey();
		}
	}
}

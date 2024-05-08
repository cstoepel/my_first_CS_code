namespace ANSI_Seq
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// Wikipedia ANSI Code Sequenzen
			// https://en.wikipedia.org/wiki/ANSI_escape_code
			Console.ResetColor();
			Console.BackgroundColor = ConsoleColor.Green;
			const string CSI        = "\x1b[";
			const string SeqEnd     = "m";
			const string ResetColor = CSI + "0m";
			const string RGBColor   = CSI + "38;2;";

			for (int i = 0; i < 256; i += 4)
			{
				for (int j = 0; j < 256; j += 4)
				{
					Console.Write(RGBColor + i.ToString() + ";" + j.ToString() + ";50" + SeqEnd);
					Console.Write("█");
				}
				Console.Write(ResetColor + "\n");
			}
		}
	}
}

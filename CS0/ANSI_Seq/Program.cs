namespace ANSI_Seq
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//Console.Write("\x1b[36mTEST\x1b[0m");

			const string CSI = "\x1b[";

			for (int i = 0; i < 256; i++)
			{
				Console.Write(CSI);
				Console.Write("38;2;" + i.ToString() + ";0;0m");
				Console.Write("█");
			}





		}
	}

	internal class ANSI_seq
	{
		ANSI_seq() { }
		public byte r, g, b;
	}
}

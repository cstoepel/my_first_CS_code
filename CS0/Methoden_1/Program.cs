namespace Methoden_1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("___Methoden_1___");

			MakeClassAObject mca = new MakeClassAObject();

			Console.Write("\n___Return Object___\n");
			ClassA obj1 = mca.ReturnNewClassAObj("OBJEKT1");
			obj1.PrintFields();

			Console.Write("\n___New Object___\n");
			ClassA obj2 = new ClassA { Name = "OBJECT2", a = 1, b = 2, basis = 3};
			obj2.PrintFields();

			Console.Write("\n___Call by Value___\n");
			// Call by Value
			mca.MakeNewClassAObj("OBJEKT2A", obj2);
			obj2.PrintFields();


			Console.Write("\n___Call by Ref___\n");
			// Call by Ref
			mca.MakeNewClassAObj("OBJEKT2A", ref obj2);
			obj2.PrintFields();

			Console.Write($"obj2.Add(2,3) = {obj2.Add(2, 3)}\n");
			Console.Write($"obj2.Add() = {obj2.Add()}\n");
			Console.Write($"obj2.Sum() = {obj2.Sum()}\n");

		}
	}
}

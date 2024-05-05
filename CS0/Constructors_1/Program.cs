namespace Constructors_1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("___Konstruktoren_1___");

			// Verschiedene Objektinitialisierungen
			ClassA obj1 = new ClassA();
			ClassA obj2 = new ClassA(1000, 2000);
			ClassA obj3 = new ClassA { b = 4000, a = 3000 };

			obj1.PrintFields();
			obj2.PrintFields();
			obj3.PrintFields();

			Console.WriteLine($"obj1 a = {obj1.a} b = {obj1.b}");
			Console.WriteLine($"obj2 a = {obj2.a} b = {obj2.b}");
			Console.WriteLine($"obj3 a = {obj3.a} b = {obj3.b}");
		}
	}
}

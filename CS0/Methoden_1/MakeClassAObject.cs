using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Methoden_1
{
	internal class MakeClassAObject
	{
		public MakeClassAObject() { Console.Write("Konstruktor: |MakeClassAObject|\n"); }
		public ClassA ReturnNewClassAObj(string name)
		{
			Console.Write("ReturnNewClassAObj(...) {\n");
			ClassA a = new ClassA { Name = name, a = 1000, b = 2000, basis = 3000 };
			a.PrintFields();
			Console.Write("}\n");
			return a;
		}
		public void MakeNewClassAObj(string name, ClassA a)
		{
			Console.Write("MakeNewClassAObj(...) {\n");
			a.PrintFields();
			a = new ClassA { Name = name, a = 1000, b = 2000, basis = 3000 };
			a.PrintFields();
			Console.Write("}\n");
		}
		public void MakeNewClassAObj(string name, ref ClassA a)
		{
			Console.Write("MakeNewClassAObj(ref ...) {\n");
			a.PrintFields();
			a = new ClassA { Name = name, a = 1000, b = 2000, basis = 3000 };
			a.PrintFields();
			Console.Write("}\n");
		}

	}
}

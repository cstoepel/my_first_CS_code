using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;

namespace test;

internal class Program
{
	static void Main(string[] args)
	{
		Class_A a = new Class_A();
		Class_B b = new Class_B();

		//Base ba = b;
		Class_A ba = b;
		ba.print();

		Class_C c = new Class_C();
		c.Print1();
		c.Print2();
	}
}

class Base
{
	public virtual void print() { Console.WriteLine("Base.print"); }
}

class Class_A: Base
{
	public virtual void print() { Console.WriteLine("Class_A.print"); }
}

class Class_B : Class_A
{
	public override void print() { Console.WriteLine("Class_B.print"); }
}

public interface I1
{
	void Print1();
}

public interface I2:I1
{
	void Print2();
}

public class Base_of_C
{
	public void Print() { Console.WriteLine("Base_of_C.Print"); }
}

public class Class_C: Base_of_C, I2
{
	public void Print1() { Console.WriteLine("Class_C.Print1"); }
	public void Print2() { Console.WriteLine("Class_C.Print2"); }
}
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.InteropServices;

namespace test;

internal class Program
{

	public delegate int Func(int a = 1);
	static unsafe void Main(string[] args)
	{

	A a = new();
		Func f = new Func(A.f5);
	}
	public static int f1(int a = 1) { return a; }
	public static int f2(int a) { return a; }
	public  int f3(int a) { return a; }
	public  int f4(int a) { return a; }
	private  int f5(int a) { return a; }
}


public class A
{

	 public void Methode()
	{
		A a = new();
		Func f = new Func(a.f5);
		f(1);
	}
	public delegate int Func(int a = 1);
	public static int f1(int a = 1) { return a; }
	public static int f2(int a) { return a; }
	public int f3() { int a = 1;  return a; }
	public int f4(int a) { return a; }
	private int f5(int a) { return a; }
	public static int f6(int a) { return a; }
}


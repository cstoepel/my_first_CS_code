using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.InteropServices;

namespace test;

internal class Program
{
	static unsafe void Main(string[] args)
	{
		STRUCT_A A = new() { a=0,b=0,c=0};

		Console.WriteLine($"{sizeof(STRUCT_A)}");
		Console.WriteLine($"{sizeof(A)}");

		STRUCT_A[] Ar = new STRUCT_A[12];
		Console.WriteLine($"{sizeof(Ar)}");

		int[] IAr = [1, 2, 3, 4, 5];
		Console.WriteLine($"{sizeof(IAr)}");

		const int[] cIAr = [1, 2, 3, 4, 5];

		// ___in_C_________________________________
		//
		// int ar[100];
		// n = sizeof(ar) / sizeof(ar[0]);

	}
}


struct STRUCT_A
{
	public Int32 a;
	public Int32 b;
	public Int32 c;
}
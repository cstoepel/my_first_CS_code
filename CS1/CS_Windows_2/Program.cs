namespace Win32_Console;

using CS_Windows_2;
using System.Runtime.InteropServices;

internal class Program
{
	static unsafe void Main(string[] args)
	{
		WinConsole.Init(185, 58);
		//WinConsole.Init(100, 100);

		Window W0 = new Window("  Window1  ",  10, 10, 40, 25);
		Window W1 = new Window("  What is C?",  32, 15, 30, 24);
		Window W2 = new Window("  What is C++  ", 52,  3, 40, 25);
		Window W3 = new Window("  What is C#  ",  59,  9, 60, 25);
		Window W4 = new Window("  Prime Numbers 1 ",  70,  2, 80, 40);
		Window W5 = new Window("  Prime Numbers 2 ",  95,  7, 50, 25);
		Window W6 = new Window("  Zufallsmuster 1",  90,  12, 80, 45);
		Window W7 = new Window("  Zufallsmuster 2",  20,  12, 60, 30);

		W0.ClearChar.Attributes = 0x007f;
		W1.ClearChar.Attributes = 0x000f;
		W2.ClearChar.Attributes = 0x000f;
		W3.ClearChar.Attributes = 0x000f;
		W4.ClearChar.Attributes = 0x000f;
		W5.ClearChar.Attributes = 0x000f;
		W6.ClearChar.Attributes = 0x000f;

		W1.DemoText = "A successor to the programming language B, C was originally developed at Bell Labs by Ritchie between 1972 and 1973 to construct utilities running on Unix. It was applied to re-implementing the kernel of the Unix operating system.[8] During the 1980s, C gradually gained popularity. It has become one of the most widely used programming languages,[9][10] with C compilers available for practically all modern computer architectures and operating systems. The book The C Programming Language, co-authored by the original language designer, served for many years as the de facto standard for the language.[11][1] C has been standardized since 1989 by the American National Standards Institute (ANSI) and the International Organization for Standardization (ISO).";

		W2.DemoText = "C++ (pronounced \"C plus plus\" and sometimes abbreviated as CPP) is a high-level, general-purpose programming language created by Danish computer scientist Bjarne Stroustrup. First released in 1985 as an extension of the C programming language, it has since expanded significantly over time; as of 1997, C++ has object-oriented, generic, and functional features, in addition to facilities for low-level memory manipulation for making things like microcomputers or to make operating systems like Linux or Windows. It is almost always implemented as a compiled language, and many vendors provide C++ compilers, including the Free Software Foundation, LLVM, Microsoft, Intel, Embarcadero, Oracle, and IBM.[14] ";

		W3.DemoText = "C# (see SHARP) is a general-purpose high-level programming language supporting multiple paradigms. C# encompasses static typing,[16]: 4  strong typing, lexically scoped, imperative, declarative, functional, generic,[16]: 22  object-oriented (class-based), and component-oriented programming disciplines.[17]\r\n\r\nThe C# programming language was designed by Anders Hejlsberg from Microsoft in 2000 and was later approved as an international standard by Ecma (ECMA-334) in 2002 and ISO/IEC (ISO/IEC 23270 and 20619[c]) in 2003. Microsoft introduced C# along with .NET Framework and Visual Studio, both of which were closed-source. At the time, Microsoft had no open-source products. Four years later, in 2004, a free and open-source project called Mono began, providing a cross-platform compiler and runtime environment for the C# programming language. A decade later, Microsoft released Visual Studio Code (code editor), Roslyn (compiler), and the unified .NET platform (software framework), all of which support C# and are free, open-source, and cross-platform. Mono also joined Microsoft but was not merged into .NET. ";

		W0.CurrentChar.Attributes = 0x0003;
		W1.CurrentChar.Attributes = 0x004f;
		W2.CurrentChar.Attributes = 0x002f;
		W3.CurrentChar.Attributes = 0x006f;
		W4.CurrentChar.Attributes = 0x0004;
		W5.CurrentChar.Attributes = 0x0001;
		W6.CurrentChar.Attributes = 0x0001;
		W7.CurrentChar.Attributes = 0x0006;

		W0.ClearAppBuffer();
		W1.ClearAppBuffer();
		W2.ClearAppBuffer();
		W3.ClearAppBuffer();
		W4.ClearAppBuffer();
		W5.ClearAppBuffer();
		W6.ClearAppBuffer();

		W0.CopyAppBuffer();
		W1.CopyAppBuffer();
		W2.CopyAppBuffer();
		W3.CopyAppBuffer();
		W4.CopyAppBuffer();
		W5.CopyAppBuffer();
		W6.CopyAppBuffer();

		WinConsole.AddWindow(W0);
		WinConsole.AddWindow(W1);
		WinConsole.AddWindow(W2);
		WinConsole.AddWindow(W3);
		WinConsole.AddWindow(W4);
		WinConsole.AddWindow(W5);
		WinConsole.AddWindow(W6);
		WinConsole.AddWindow(W7);

		W0.Refresh();
		W1.Refresh();
		W2.Refresh();
		W3.Refresh();
		W4.Refresh();
		W5.Refresh();
		W6.Refresh();
		WinConsole.SetActiveWindow(W1);

		W0.Start();
		W1.Start();
		W2.Start();
		W3.Start();
		W4.Start(Prime.Calculate);
		W5.Start(Prime.Calculate);
		W6.Start(Rand.RandomPattern3);
		W7.Start(Rand.RandomPattern2);
		WinConsole.Start();

		while (true) Thread.Sleep(1000);
		Console.WriteLine("ENDE.");
	}
}


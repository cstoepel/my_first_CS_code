namespace Win32_Console;
using System.Runtime.InteropServices;

internal class Program
{
	static unsafe void Main(string[] args)
	{
		WinConsole.Init(185, 58);

		Window W0 = new Window("  Window1  ",  10, 10, 40, 25);
		Window W1 = new Window("  What is C?  ",  32, 15, 30, 24);
		Window W2 = new Window("  What is C++  ", 52,  3, 40, 25);
		Window W3 = new Window("  What is C#  ",  59,  9, 60, 25);

		W0.BackgroundAttr = 0x007f;
		W1.BackgroundAttr = 0x00bf;
		W2.BackgroundAttr = 0x00bf;
		W2.BackgroundAttr = 0x00bf;

		W1.Text = "A successor to the programming language B, C was originally developed at Bell Labs by Ritchie between 1972 and 1973 to construct utilities running on Unix. It was applied to re-implementing the kernel of the Unix operating system.[8] During the 1980s, C gradually gained popularity. It has become one of the most widely used programming languages,[9][10] with C compilers available for practically all modern computer architectures and operating systems. The book The C Programming Language, co-authored by the original language designer, served for many years as the de facto standard for the language.[11][1] C has been standardized since 1989 by the American National Standards Institute (ANSI) and the International Organization for Standardization (ISO).";

		W2.Text = "C++ (pronounced \"C plus plus\" and sometimes abbreviated as CPP) is a high-level, general-purpose programming language created by Danish computer scientist Bjarne Stroustrup. First released in 1985 as an extension of the C programming language, it has since expanded significantly over time; as of 1997, C++ has object-oriented, generic, and functional features, in addition to facilities for low-level memory manipulation for making things like microcomputers or to make operating systems like Linux or Windows. It is almost always implemented as a compiled language, and many vendors provide C++ compilers, including the Free Software Foundation, LLVM, Microsoft, Intel, Embarcadero, Oracle, and IBM.[14] ";

		W3.Text = "C# (see SHARP) is a general-purpose high-level programming language supporting multiple paradigms. C# encompasses static typing,[16]: 4  strong typing, lexically scoped, imperative, declarative, functional, generic,[16]: 22  object-oriented (class-based), and component-oriented programming disciplines.[17]\r\n\r\nThe C# programming language was designed by Anders Hejlsberg from Microsoft in 2000 and was later approved as an international standard by Ecma (ECMA-334) in 2002 and ISO/IEC (ISO/IEC 23270 and 20619[c]) in 2003. Microsoft introduced C# along with .NET Framework and Visual Studio, both of which were closed-source. At the time, Microsoft had no open-source products. Four years later, in 2004, a free and open-source project called Mono began, providing a cross-platform compiler and runtime environment for the C# programming language. A decade later, Microsoft released Visual Studio Code (code editor), Roslyn (compiler), and the unified .NET platform (software framework), all of which support C# and are free, open-source, and cross-platform. Mono also joined Microsoft but was not merged into .NET. ";

		W0.Character.Attributes = 0x0003;
		W1.Character.Attributes = 0x004f;
		W2.Character.Attributes = 0x002f;
		W3.Character.Attributes = 0x006f;

		WinConsole.AddWindow(W0);
		WinConsole.AddWindow(W1);
		WinConsole.AddWindow(W2);
		WinConsole.AddWindow(W3);

		W0.Refresh();
		W1.Refresh();
		W2.Refresh();
		W3.Refresh();
		WinConsole.SetActiveWindow(W1);



		W0.Start();
		W1.Start();
		W2.Start();
		W3.Start();
		WinConsole.Start();

		//while (true)
		//{
		//	W0.BackgroundAttr = 0x002f;
		//	W0.Refresh();
		//	Thread.Sleep(1000);
		//	W0.BackgroundAttr = 0x007f;
		//	W0.Refresh();
		//	Thread.Sleep(1000);
		//}
		while (true) Thread.Sleep(1000);
		Console.WriteLine("ENDE.");
	}
}


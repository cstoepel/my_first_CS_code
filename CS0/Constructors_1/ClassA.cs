﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constructors_1
{
	internal class ClassA
	{
		public ClassA() { Console.Write("Konstruktor: |ClassA()|\n"); }
		public ClassA(int a, int b)
		{
			Console.Write("Konstruktor: |ClassA(int a, int b)|\n");
			this.a = a;
			this.b = b;
		}

		public int a { get; set; }
		public int b { get; set; }
		public void PrintFields()
		{
			Console.Write($"Object of ClassA: a = {a} b = {b}\n");
		}
		
	}


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Methoden_1
{
	internal class ClassA : BaseClass
	{
		public ClassA() { Console.Write("Konstruktor: |ClassA()|\n"); }
		public ClassA(int a, int b)
		{
			Console.Write("Konstruktor: |ClassA(int a, int b)|\n");
			this.a = a;
			this.b = b;
		}
		public string Name;
		public int a { get; set; }
		public int b { get; set; }
		public void PrintFields()
		{
			Console.Write($"Object of ClassA: name = {Name} basis = {basis} a = {a} b = {b}\n");
		}
		// Überladene Methoden
		// Locale Vars
		public double Add(double a, double b) { return a + b; }
		public double Mul(double a, double b) { return a * b; }
		// Class Felder
		public double Add() { return this.a + this.b; }
		public double Mul() { return this.a * this.b; }
		// ohne this pointer
		public double Sum() { return a + b + basis;}

	}
}

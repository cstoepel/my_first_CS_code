using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screen_3;

internal class Shape
{
	public Shape() { }

	internal ConsoleColor color = ConsoleColor.Gray;
	internal virtual void Draw()
	{
		Console.Write("Shape.Draw()\n");
	}
	internal virtual void Move(int dx, int dy)
	{
		Console.Write("Shape.Move()");
	}

}

internal class Shapes
{
	public Shapes() { }
	internal List<Shape> ShapeList = new List<Shape>();

	internal Shape GetShapeByIndex(int index) { return ShapeList[index]; }

	internal void Add(Shape s)
	{
		ShapeList.Add(s);
	}

	internal void DrawAll()
	{
		foreach (Shape s in ShapeList) { s.Draw(); }
	}
}

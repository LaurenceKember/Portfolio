using System;
namespace Shapes
{
	public class Circle
	{
		private Point centre;
		private double radius;

		public Circle(Point c, double r)
		{
			centre = c;
			radius = r;
		}

		public void Display()
		{
			Console.WriteLine("Centre: ");
			centre.Display();
			Console.WriteLine($"Radius: {radius}");
		}

		public double Circumference()
		{
			return (2 * Math.PI * radius);
		}

		public double Area()
		{
			return (Math.PI * radius * radius);
		}
	}
}


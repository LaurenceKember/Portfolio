using System;
namespace Shapes
{
    public class Point
    {
        protected int x;
        protected int y;

        public Point(int xarg,int yarg)
        {
            x = xarg;
            y = yarg;
        }

        public void Display()
        {
            Console.WriteLine($"[{x}, {y}]");
        }

        public double DistanceFrom(Point p2)
        {
            return (Math.Sqrt(Math.Pow(p2.x - x, 2) + Math.Pow(p2.y - y, 2)));
        }
    }
}


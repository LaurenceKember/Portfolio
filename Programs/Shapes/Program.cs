using System;

namespace Shapes
{
    class Program
    {
        static void Main(string[] args)
        {
            int x;
            int y;
            double radius;

            Console.WriteLine("Calculate the distance between two points:");

            Console.WriteLine("Please enter the coordinates for p1");
            x = Convert.ToInt32(Console.ReadLine());
            y = Convert.ToInt32(Console.ReadLine());

            Point p1 = new Point(x, y);

            Console.WriteLine("Please enter the coordinates for p2");
            x = Convert.ToInt32(Console.ReadLine());
            y = Convert.ToInt32(Console.ReadLine());

            Point p2 = new Point(x, y);

            p1.Display();
            p2.Display();

            Console.WriteLine($"Distance between the points is: {p1.DistanceFrom(p2)}");

            Console.WriteLine();

            Console.WriteLine("Calculate the circumference and area of a circle around each point:");

            Console.WriteLine("Please enter the radius of c1");
            radius = Convert.ToInt32(Console.ReadLine());

            Circle c1 = new Circle(p1, radius);

            Console.WriteLine("Please enter the radius of c2");
            radius = Convert.ToInt32(Console.ReadLine());

            Circle c2 = new Circle(p2, radius);

            Console.WriteLine($"c1 circumference: {c1.Circumference()}");
            Console.WriteLine($"c2 circumference: {c2.Circumference()}");

            Console.WriteLine($"c1 area: {c1.Area()}");
            Console.WriteLine($"c2 area: {c2.Area()}");
        }
    }
}


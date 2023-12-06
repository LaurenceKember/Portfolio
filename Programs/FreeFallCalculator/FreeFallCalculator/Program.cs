using System;

namespace FreeFallCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the distance calculator");
            Console.WriteLine("Please enter the initial velocity");
            string velocityasstring = Console.ReadLine();
            double velocity = Convert.ToDouble(velocityasstring);

            Console.WriteLine("Please enter the initial position");
            string initialpositionasstring = Console.ReadLine();
            double initialposition = Convert.ToDouble(initialpositionasstring);

            Console.WriteLine("Please confirm the time in seconds which the object is moving");
            string timeasstring = Console.ReadLine();
            double time = Convert.ToDouble(timeasstring);

            double a = 9.81;

            double distance = (0.5 * a * time * time) + (velocity * time) + initialposition;

            Console.WriteLine($"The distance travelled by the object is {distance}");
            Console.ReadLine();
        }
    }
}

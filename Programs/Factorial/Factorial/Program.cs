using System;

namespace Factorial
{
    class Program
    {
        static void Main(string[] args)
        {
            // read n as input from the keyboard
            Console.WriteLine("Please enter the number for which you would like to calculate the factorial:");
            int n = Convert.ToInt32(Console.ReadLine());
            int factorial = 1;
            // count used to represent the factors in the loop
            int count = n;
            while (count > 1)
            {
                // properly update factorial and count
                factorial *= count;
                count--;
            }
            Console.WriteLine($"The answer is: {factorial}");
            Console.ReadLine();
        }
    }
}


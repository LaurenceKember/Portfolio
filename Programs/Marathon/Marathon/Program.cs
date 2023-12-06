using System;

namespace Marathon
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            //read the number of contestants as input
            Console.WriteLine("Please enter the number of contestants:");
            int contestants = Convert.ToInt32(Console.ReadLine());

            //declare an array of timings
            double[] timings = new double[contestants];

            //generate the timings randomly
            for(int i = 0; i < timings.Length; i++)
            {
                timings[i] = 80 * r.NextDouble() + 20.0;
                Console.WriteLine(timings[i]);
            }
            double first = timings[0];
            double second = timings[1];

            for(int i = 1; i < timings.Length; i++)
            {
                if (timings[i] < first)
                {
                    second = first;
                    first = timings[i];
                }
            }
            Console.WriteLine($"The best time in minutes is: {first}");
            Console.WriteLine($"The second best time in minutes is: {second}");
        }
    }
}


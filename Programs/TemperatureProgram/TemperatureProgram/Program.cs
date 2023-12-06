using System;

namespace TemperatureProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            int invalid = 0;
            int critical = 0;
            int warning = 0;

            while (invalid < 3)
            {
                Console.WriteLine("Please enter the current temperature");
                string tempasstring = Console.ReadLine();
                int temp = Convert.ToInt32(tempasstring);
                switch (temp)
                {
                    case > 100:
                        Console.WriteLine("Invalid temperature");
                        invalid += 1;
                        break;

                    case < 0:
                        Console.WriteLine("Invalid temperature");
                        invalid += 1;
                        break;

                    case > 30:
                    
                        Console.WriteLine("Critical");
                        critical += 1;
                        break;

                    case > 24:
                    
                        Console.WriteLine("Warning");
                        warning += 1;
                        break;

                    default:
                    
                        Console.WriteLine("Normal");
                        break;
                    
                }
                    
                
            }
            Console.WriteLine("Sensor is broken!");
            Console.WriteLine($"Critical: {critical}, Warning: {warning}, Invalid: {invalid}");
            Console.ReadLine();
        }
    }
}


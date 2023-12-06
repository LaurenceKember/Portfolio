using System;

namespace TwoDice
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();

            int dice1;
            int dice2;
            int total;
            bool cont = true;

            while(cont == true)
            {
                dice1 = r.Next(1, 7);
                dice2 = r.Next(1, 7);
                total = dice1 + dice2;

                Console.WriteLine($"Dice 1: {dice1}, Dice 2: {dice2}");
                if (total == 12)
                {
                    cont = false;
                }
            }
        }
    }
}


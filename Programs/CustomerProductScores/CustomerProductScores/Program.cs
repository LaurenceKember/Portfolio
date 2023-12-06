using System;

namespace CustomerProductScores
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] custscores = {1.1, 2.2, 3.3, 4.4, 4.9};
            double[] revscores = new double[custscores.Length];
            int j = custscores.Length - 1;

            for (int i = 0; i < custscores.Length; i++, j--)
            {
                revscores[j] = custscores[i];
            }
            foreach (double c in custscores)
            {
                Console.WriteLine(c);
            }

            Console.WriteLine();

            foreach (double r in revscores)
            {
                Console.WriteLine(r);
            }

        }
    }
}


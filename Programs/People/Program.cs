using System;

namespace Tutorial5Ex4
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p1 = new Person("Laurence", "Jones", 1991);
            Person p2 = new Person("Veronika", "Jones", 1990);

            p1.SetAddress("Merton Hall Road");
            p2.SetAddress("Liberty Avenue");

            p1.Display();
            p2.Display();

        }
    }
}


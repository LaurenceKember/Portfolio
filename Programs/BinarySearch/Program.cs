using System;

namespace BinarySearch;
class Program
{
    static void Main(string[] args)
    {

        int[] array = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100};

        int start = 0;
        int end = array.Length -1;
        int valueToFind;
        bool notFound = true;

        Console.WriteLine("Please enter the value you are searching for: ");
        valueToFind = Convert.ToInt32(Console.ReadLine());

        while (start <= end)
        {
            int middle = (start + end) / 2;

            if (valueToFind == array[middle])
            {
                Console.WriteLine("The value is at position: " + middle);
                start = end + 1;
                notFound = false;
            }
            else
            {
                if(valueToFind < array[middle])
                {
                    end = middle - 1;
                }
                else
                {
                    if(valueToFind > array[middle])
                    {
                        start = middle + 1;
                    }
                }
            }
        }

        if(notFound)
        {
            Console.WriteLine("Value not found");
        }
    }
}


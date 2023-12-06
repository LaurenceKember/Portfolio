using System;
namespace BubbleSort
{
	public class BubbleSort
	{

		private int[] array;
		private int ArrayLength;
		private int temp = 0;

		public BubbleSort(int[] array)
		{
			this.array = array;
            ArrayLength = array.Length - 1;
        }

		public void doBubbleSort(int[] array)
		{

			Console.WriteLine("Unsorted array:");

            foreach (int value in array)
            {
                Console.WriteLine(value);
            }

            int i = 0;
			int j = 0;

			for (i = 0; i < ArrayLength; i++)
			{
				for (j = 0; j < ArrayLength; j++)
				{
					int k = j + 1;
					if (array[j] > array[k])
					{
						temp = array[k];
						array[k] = array[j];
						array[j] = temp;
					}
				}
			}

			Console.WriteLine("Sorted array:");

			foreach(int value in array)
			{
				Console.WriteLine(value);
			}
		}
	}
}


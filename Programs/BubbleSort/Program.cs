namespace BubbleSort;
class Program
{
    static void Main(string[] args)
    {
        int[] array1 = { 1, 4, 78, 43, 33, 99, 13 };
        int[] array2 = { 1,2,3,4,5,6};
        int[] array3 = { 6,5,4,3,2,1};

        BubbleSort sort1 = new BubbleSort(array1);
        BubbleSort sort2 = new BubbleSort(array2);
        BubbleSort sort3 = new BubbleSort(array3);

        sort1.doBubbleSort(array1);
        sort2.doBubbleSort(array2);
        sort3.doBubbleSort(array3);
    }
}


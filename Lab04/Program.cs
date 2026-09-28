using System;

class Program
{
    static int[] data = { 42, 8, 60, 19, 3, 55, 12, 31, 68, 24, 49, 37, 71, 5, 27 };
    static int[] sortedData = SortAscending((int[])data.Clone());

    static int comparisons = 0; // лічильник порівнянь, обнуляється перед кожним пошуком


    static void Main(string[] args)
    {
        Console.WriteLine("sortedData = [" + string.Join(", ", sortedData) + "]");
        Console.WriteLine();

        var cases = new (string Name, int[] Items, int Target)[]
        {
            ("1. першим",           sortedData,      3),
            ("2. останнім",         sortedData,      71),
            ("3. посередині",       sortedData,      31),
            ("4. менше за всі",     sortedData,      1),
            ("5. більше за всі",    sortedData,      99),
            ("6. між сусідніми",    sortedData,      50),
            ("7. один елемент",     new[] { 42 },    42),
            ("8. порожній масив",   new int[0],      42),
        };

        foreach (var c in cases)
        {
            Console.WriteLine(c.Name);
            Report("linear", c.Items, c.Target);
            Report("binary", c.Items, c.Target);
        }

        Console.WriteLine();
        Console.WriteLine("Експеримент: бінарний пошук на НЕвідсортованому data, шукаємо 55");
        Report("binary", data, 55);
    
    }
    static int[] SortAscending(int[] a)
    {
        Array.Sort(a);
        return a;
    }



    static int LinearSearch(int[] items, int target) 
    { 
    for (int i = 0; i < items.Length; i++) 
    { 
        comparisons++; 
        if (items[i] == target) 
            return i; 
    } 
        return -1; 
    } 
    static int BinarySearch(int[] items, int target) 
    { 
    int low = 0; 
    int high = items.Length - 1; 
    while (low <= high) 
    { 
        int mid = low + (high - low) / 2; 
        comparisons++;                       
        int cmp = items[mid].CompareTo(target); 
        if (cmp == 0) 
            return mid; 
        if (cmp < 0)
            low = mid + 1;                   // шукане правіше 
        else 
            high = mid - 1;                  // шукане лівіше 
    } 
        return -1; 
    } 
     static void Report(string name, int[] items, int target)
    {
        comparisons = 0;
        int index = name == "linear" ? LinearSearch(items, target)
                                     : BinarySearch(items, target);
        Console.WriteLine($"{name,-7} target={target,-3} index={index,-3} comparisons={comparisons}");
    }

}
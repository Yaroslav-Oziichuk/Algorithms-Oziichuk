using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<string> TopoSort(Dictionary<string, List<string>> before,
                                 Dictionary<string, List<string>> after)
    {
        var incoming = new Dictionary<string, int>();

        // TODO 1
        foreach (var kv in before)
            incoming[kv.Key] = kv.Value.Count;

        var queue = new Queue<string>();

        // TODO 2
        foreach (var kv in incoming)
            if (kv.Value == 0)
                queue.Enqueue(kv.Key);

        var order = new List<string>();

        while (queue.Count > 0)
        {
            string v = queue.Dequeue();
            order.Add(v);

            foreach (string next in after[v])
            {
                // TODO 3
                incoming[next]--;
                if (incoming[next] == 0)
                    queue.Enqueue(next);
            }
        }
        return order;
    }

    // TODO 4
    static bool HasCycle(Dictionary<string, List<string>> before,
                         Dictionary<string, List<string>> after)
    {
        return TopoSort(before, after).Count < before.Count;
    }

    static Dictionary<string, List<string>> Copy(Dictionary<string, List<string>> d)
    {
        var r = new Dictionary<string, List<string>>();
        foreach (var kv in d) r[kv.Key] = new List<string>(kv.Value);
        return r;
    }

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var beforeA = new Dictionary<string, List<string>>
        {
            { "Вступ", new List<string>() },
            { "Математика", new List<string>() },
            { "ООП", new List<string> { "Вступ" } },
            { "Бази даних", new List<string> { "Вступ" } },
            { "Алгоритми", new List<string> { "Вступ", "Математика" } },
            { "Вебтехнології", new List<string> { "ООП", "Бази даних" } },
            { "Курсовий", new List<string> { "Алгоритми", "Вебтехнології" } }
        };
        var afterA = new Dictionary<string, List<string>>
        {
            { "Вступ", new List<string> { "ООП", "Бази даних", "Алгоритми" } },
            { "Математика", new List<string> { "Алгоритми" } },
            { "ООП", new List<string> { "Вебтехнології" } },
            { "Бази даних", new List<string> { "Вебтехнології" } },
            { "Алгоритми", new List<string> { "Курсовий" } },
            { "Вебтехнології", new List<string> { "Курсовий" } },
            { "Курсовий", new List<string>() }
        };

        var beforeB = Copy(beforeA);
        beforeB["Бази даних"] = new List<string> { "Вступ", "Вебтехнології" };
        var afterB = Copy(afterA);
        afterB["Вебтехнології"] = new List<string> { "Курсовий", "Бази даних" };

        // Набір А 
        var orderA = TopoSort(beforeA, afterA);
        Console.WriteLine("topoSort(А): " + orderA.Count + " предметів");
        Console.WriteLine("Порядок А:   " + string.Join(" → ", orderA));
        Console.WriteLine("Останній:    " + orderA.Last() + ", перший: " + orderA.First());
        Console.WriteLine("Вступ раніше Вебтехнологій: " + (orderA.IndexOf("Вступ") < orderA.IndexOf("Вебтехнології")));
        Console.WriteLine("hasCycle(А): " + HasCycle(beforeA, afterA));

        // Набір Б 
        var orderB = TopoSort(beforeB, afterB);
        Console.WriteLine("topoSort(Б): " + orderB.Count + " предметів: " + string.Join(" → ", orderB));
        Console.WriteLine("hasCycle(Б): " + HasCycle(beforeB, afterB));
        var missing = beforeB.Keys.Where(k => !orderB.Contains(k)).ToList();
        Console.WriteLine("Не потрапили в Б: " + string.Join(", ", missing));
    }
}


using System;
using System.Collections.Generic;

class Request
{
    public string Name { get; set; }
    public int Priority { get; set; }
    public int Seq { get; set; }

    public Request(string name, int priority, int seq)
    {
        Name = name;
        Priority = priority;
        Seq = seq;
    }

    public override string ToString()
    {
        return $"[Пріоритет: {Priority}, Seq: {Seq}] {Name}";
    }
}

class Program
{
    static void Main()
    {
        Request[] incoming = {
            new Request("Оновити підпис у листі", 3, 1),
            new Request("Сервер не відповідає", 1, 2),
            new Request("Не друкує принтер", 2, 3),
            new Request("Не працює термінал оплати", 1, 4),
            new Request("Змінити шпалери", 3, 5)
        };

        Request late = new Request("Відвалився інтернет у відділі", 1, 6);

        Console.WriteLine("ЗАДАЧА 1");
        ProcessAll(incoming);

        Console.WriteLine("\nЗАДАЧА 2");
        ProcessStable(incoming);

        Console.WriteLine("\nЗАДАЧА 3");
        ProcessWithLate(incoming, late);
    }

    // Задача 1
    static void ProcessAll(Request[] incoming)
    {
        PriorityQueue<Request, int> pq = new PriorityQueue<Request, int>();

        foreach (var req in incoming)
        {
            pq.Enqueue(req, req.Priority);
        }

        int count = 1;
        while (pq.Count > 0)
        {
            var req = pq.Dequeue();
            Console.WriteLine($"{count}. Назва: {req.Name}, Пріоритет: {req.Priority}, Seq: {req.Seq}");
            count++;
        }
    }

    // Задача 2
    static void ProcessStable(Request[] incoming)
    {
        PriorityQueue<Request, (int Priority, int Seq)> pq = new PriorityQueue<Request, (int, int)>();

        foreach (var req in incoming)
        {
            pq.Enqueue(req, (req.Priority, req.Seq));
        }

        int count = 1;
        while (pq.Count > 0)
        {
            var req = pq.Dequeue();
            Console.WriteLine($"{count}. Назва: {req.Name}, Пріоритет: {req.Priority}, Seq: {req.Seq}");
            count++;
        }
    }

    // Задача 3
    static void ProcessWithLate(Request[] incoming, Request late)
    {
        PriorityQueue<Request, (int Priority, int Seq)> pq = new PriorityQueue<Request, (int, int)>();

        int processedCount = 0;
        int count = 1;

        foreach (var req in incoming)
        {
            pq.Enqueue(req, (req.Priority, req.Seq));
        }

        while (pq.Count > 0)
        {
            var req = pq.Dequeue();
            Console.WriteLine($"{count}. Назва: {req.Name}, Пріоритет: {req.Priority}, Seq: {req.Seq}");
            processedCount++;
            count++;

            if (processedCount == 2)
            {
                pq.Enqueue(late, (late.Priority, late.Seq));
            }
        }
    }
}

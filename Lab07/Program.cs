using System;
using System.Collections.Generic;

class Program
{
    class Request
    {
        public string Name { get; set; }
        public int Minutes { get; set; }

        public Request(string name, int minutes)
        {
            Name = name;
            Minutes = minutes;
        }
    }

    static void Main()
    {
        string[] lines = new string[]
        {
            "",
            "a + b",
            "{[a]+(b)}",
            "([)]",
            "a)",
            "(a[b]",
            "f(x[1])*{y+(z)}"
        };

        Request[] requests = new Request[]
        {
            new Request("Довідка про навчання", 4),
            new Request("Академічна відпустка", 7),
            new Request("Копія наказу", 2),
            new Request("Виписка з оцінками", 6),
            new Request("Перескладання", 9),
            new Request("Студентський квиток", 3)
        };

        Console.WriteLine("Задача 1: Перевірка дужок ");
        foreach (var line in lines)
        {
            string res = IsBalanced(line) ? "так" : "ні";
            string dispLine = string.IsNullOrEmpty(line) ? "(порожній рядок)" : line;
            Console.WriteLine($"{dispLine,-20} -> {res}");
        }

        Console.WriteLine("\nЗадача 2: Обробка заявок ");
        ProcessQueue(requests);
    }

    // TODO 1
    static bool IsBalanced(string line)
    {
        Stack<char> stack = new Stack<char>();

        foreach (char ch in line)
        {
            if (ch == '(' || ch == '[' || ch == '{')
            {
                stack.Push(ch);
            }
            else if (ch == ')' || ch == ']' || ch == '}')
            {
                if (stack.Count == 0)
                {
                    return false;
                }

                char top = stack.Pop();
                if ((ch == ')' && top != '(') ||
                    (ch == ']' && top != '[') ||
                    (ch == '}' && top != '{'))
                {
                    return false;
                }
            }
        }

        return stack.Count == 0;
    }

    // TODO 2
    static int ProcessQueue(IEnumerable<Request> requests)
    {
        if (requests == null)
        {
            Console.WriteLine("Черга порожня. Обробляти нічого.");
            return 0;
        }

        Queue<Request> queue = new Queue<Request>(requests);

        if (queue.Count == 0)
        {
            Console.WriteLine("Черга порожня. Обробляти нічого.");
            return 0;
        }

        int currentTime = 0;
        int order = 1;
        int totalRequests = queue.Count;

        while (queue.Count > 0)
        {
            if (queue.Count == 0)
            {
                break;
            }

            Request req = queue.Dequeue();
            int startTime = currentTime;
            int endTime = startTime + req.Minutes;

            Console.WriteLine($"{order}. {req.Name} з {startTime} до {endTime} хв");

            currentTime = endTime;
            order++;
        }

        Console.WriteLine($"Оброблено заявок: {totalRequests}, загальний час: {currentTime} хв");
        return currentTime;
    }
}


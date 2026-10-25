using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void print(string message)
    {
        Console.WriteLine(message);
    }

    static void Main(string[] args)
    {
        var clients = new List<Client>
        {
            new Client(101, "Коваленко"),
            new Client(102, "Мельник"),
            new Client(103, "Шевчук"),
            new Client(104, "Бондар"),
            new Client(105, "Ткачук"),
            new Client(106, "Гриценко")
        };

        var orders = new List<Order>
        {
            new Order(1, 103, 450),
            new Order(2, 101, 120),
            new Order(3, 105, 980),
            new Order(4, 103, 300),
            new Order(5, 102, 75),
            new Order(6, 106, 1500),
            new Order(7, 101, 220),
            new Order(8, 103, 60)
        };

        var blocked = new List<int> { 102, 104 };

        // Фрагмент 1. Ім'я клієнта для кожного замовлення
        var clientDict = clients.ToDictionary(c => c.id, c => c.name);
        foreach (var order in orders)
        {
            if (clientDict.TryGetValue(order.clientId, out var clientName))
            {
                print("Замовлення " + order.number + " - " + clientName);
            }
        }

       /*  foreach (var order in orders) 
            foreach (var client in clients) 
                if (client.id == order.clientId) 
                    print("Замовлення " + order.number + " — " + client.name)  */ 

        // Фрагмент 2. Сума замовлень, крім заблокованих клієнтів
        var blockedSet = new HashSet<int>(blocked);
        int total = 0;
        foreach (var order in orders)
        {
            if (!blockedSet.Contains(order.clientId))
            {
                total += order.sum;
            }
        }
        print("Сума прийнятих замовлень: " + total);

        /* int totalOld = 0;
        foreach (var order in orders)
            if (!blocked.Contains(order.clientId))
                totalOld += order.sum;
        print("Сума прийнятих замовлень: " + totalOld); */ 

        // Фрагмент 3. Скільки замовлень у кожного клієнта
        var counts = new Dictionary<int, int>();
        foreach (var order in orders)
        {
            if (counts.ContainsKey(order.clientId))
            {
                counts[order.clientId]++;
            }
            else
            {
                counts[order.clientId] = 1;
            }
        }
        foreach (var pair in counts)
        {
            print(pair.Key + " : " + pair.Value);
        }

        /* var counted = new List<int>();
        foreach (var order in orders) {
            if (counted.Contains(order.clientId)) continue;
            int count = 0;
            foreach (var other in orders)
                if (other.clientId == order.clientId)
                    count++;
            counted.Add(order.clientId);
            print(order.clientId + ":" + count);
        } */

        // Фрагмент 4. Найбільше замовлення
        var best = orders[0];
        foreach (var order in orders)
        {
            if (order.sum > best.sum)
            {
                best = order;
            }
        }
        print("Найбільше замовлення: " + best.sum);

        // Фрагмент 5
        foreach (var order in orders)
        {
            var byId = new Dictionary<int, Client>();
            foreach (var client in clients)
            {
                byId[client.id] = client;
            }
            print("Замовлення " + order.number + " - " + byId[order.clientId].name);
        }
    }
}

public record Client(int id, string name);
public record Order(int number, int clientId, int sum);
using System;
using System.Collections.Generic;

// Клас вузла дерева меню
class Node
{
    public string Title { get; set; }
    public List<Node> Children { get; set; }

    public Node(string title, List<Node> children = null)
    {
        Title = title;
        Children = children ?? new List<Node>();
    }
}

class Program
{
    public static int callCount = 0;

    // TODO 1
    public static void printMenu(Node node, int level)
    {
        if (node == null) return;
        
        // Виводимо назву з відступом
        Console.WriteLine(new string(' ', level * 2) + node.Title);
        
        // Рекурсивно обходимо всіх дітей
        foreach (var child in node.Children)
        {
            printMenu(child, level + 1);
        }
    }

    // TODO 2
    public static int countItems(Node node)
    {
        if (node == null) return 0;
        
        callCount++; // Збільшуємо лічильник викликів на початку
        int count = 1; // Рахуємо сам поточний вузол
        
        foreach (var child in node.Children)
        {
            count += countItems(child);
        }
        
        return count;
    }

    // TODO 3
    public static bool find(Node node, string name)
    {
        if (node == null) return false;
        
        // Перевіряємо поточний вузол
        if (node.Title == name) return true;
        
        // Шукаємо серед дочірніх вузлів
        foreach (var child in node.Children)
        {
            if (find(child, name)) return true;
        }
        
        return false;
    }

    static void Main(string[] args)
    {
        // Задане дерево меню
        Node menu = new Node("Головне меню", new List<Node> {
            new Node("Файл", new List<Node> {
                new Node("Новий"),
                new Node("Зберегти")
            }),
            new Node("Правка", new List<Node> {
                new Node("Копіювати")
            }),
            new Node("Вигляд", new List<Node> {
                new Node("Масштаб", new List<Node> {
                    new Node("Збільшити"),
                    new Node("Зменшити")
                })
            }),
            new Node("Довідка")
        });

        Console.WriteLine("=== 1. Виведення ієрархії (printMenu) ===");
        printMenu(menu, 0);

        callCount = 0;
        int total = countItems(menu);

        Console.WriteLine($"\n=== 2. Підрахунок та пошук ===");
        Console.WriteLine($"Кількість пунктів (countItems): {total}");
        Console.WriteLine($"Кількість викликів countItems: {callCount}");
        Console.WriteLine($"find(\"Зберегти\"): {find(menu, "Зберегти")}");
        Console.WriteLine($"find(\"Масштаб\"): {find(menu, "Масштаб")}");
        Console.WriteLine($"find(\"Довідка\"): {find(menu, "Довідка")}");
        Console.WriteLine($"find(\"Експорт\"): {find(menu, "Експорт")}");
    }
}


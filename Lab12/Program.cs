using System;

class Node
{
    public int Value { get; set; }
    public Node? Left { get; set; }
    public Node? Right { get; set; }

    public Node(int value)
    {
        Value = value;
        Left = null;
        Right = null;
    }
}

class Program
{
    // Лічильник порівнянь для search
    public static int compareCount = 0;

    // TODO 1
    public static Node? insert(Node? node, int key)
    {
        if (node == null)
        {
            return new Node(key);
        }

        if (key < node.Value)
        {
            node.Left = insert(node.Left, key);
        }
        else if (key > node.Value)
        {
            node.Right = insert(node.Right, key);
        }
        // Якщо key == node.Value, нічого не робимо

        return node;
    }

    // TODO 2
    public static bool search(Node? node, int key)
    {
        if (node == null)
        {
            return false;
        }

        compareCount++; // Рахуємо одне порівняння на кожен відвіданий вузол

        if (key == node.Value)
        {
            return true;
        }
        else if (key < node.Value)
        {
            return search(node.Left, key);
        }
        else
        {
            return search(node.Right, key);
        }
    }

    // TODO 3
    public static void inorder(Node? node)
    {
        if (node == null) return;

        inorder(node.Left);
        Console.Write(node.Value + " ");
        inorder(node.Right);
    }

    static void Main(string[] args)
    {
        Node? root = null;
        int[] values = { 42, 18, 65, 9, 27, 55, 90, 33 };

        foreach (int val in values)
        {
            root = insert(root, val);
        }

        Console.WriteLine("=== 1. Симетричний обхід (inorder) ===");
        inorder(root);
        Console.WriteLine();

        Console.WriteLine("\n=== 2. Перевірка пошуку та лічильників ===");

        compareCount = 0;
        bool found55 = search(root, 55);
        Console.WriteLine($"search(root, 55): {found55}, порівнянь: {compareCount}");

        compareCount = 0;
        bool found30 = search(root, 30);
        Console.WriteLine($"search(root, 30): {found30}, порівнянь: {compareCount}");

        // Повторна вставка числа 27
        root = insert(root, 27);

        // Підрахунок загальної кількості вузлів
        Func<Node?, int> countAll = null!;
        countAll = (n) => n == null ? 0 : 1 + countAll(n.Left) + countAll(n.Right);
        int countNodesAfterDuplicate = countAll(root);
        
        Console.WriteLine($"Вузлів після повторної вставки 27: {countNodesAfterDuplicate}");
    }
}



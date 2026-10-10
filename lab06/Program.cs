// ЛАБОРАТОРНА 6. Рекурсивний обхід
using System;
using System.Collections.Generic;
using System.Linq;

class Category
{
    public string Name;
    public int Products;               // товарів безпосередньо в цій категорії
    public List<Category> Children;

    public Category(string name, int products, List<Category> children)
    {
        Name = name;
        Products = products;
        Children = children;
    }
}

class Program
{
    static int callCount = 0;          // лічильник викликів

    static Category catalog = new Category("Каталог", 0, new List<Category>
    {
        new Category("Одяг", 0, new List<Category>
        {
            new Category("Верхній одяг", 0, new List<Category>
            {
                new Category("Куртки", 12, new List<Category>()),
                new Category("Пальта", 7,  new List<Category>()),
            }),
            new Category("Светри", 15, new List<Category>()),
        }),
        new Category("Взуття", 4, new List<Category>
        {
            new Category("Кросівки", 23, new List<Category>()),
            new Category("Чоботи",   9,  new List<Category>()),
        }),
        new Category("Аксесуари", 0, new List<Category>
        {
            new Category("Сумки",  18, new List<Category>()),
            new Category("Ремені", 5,  new List<Category>()),
        }),
    });

    // TODO 1: вивести всю ієрархію з відступом за рівнем вкладеності
    static void PrintTree(Category node, int level)
    {
        callCount++;
        Console.WriteLine(new string(' ', level * 2) + node.Name);
        foreach (Category child in node.Children)
            PrintTree(child, level + 1);   // рекурсивний виклик для піддерева
    }

    // TODO 2: загальна кількість товарів у вузлі та всіх його підкатегоріях
    static int CountProducts(Category node)
    {
        callCount++;
        if (node.Children.Count == 0)      // базовий випадок: листок
            return node.Products;

        int total = node.Products;
        foreach (Category child in node.Children)
            total += CountProducts(child);
        return total;
    }

    // TODO 3: глибина найдовшої гілки (Каталог сам по собі — глибина 1)
    static int MaxDepth(Category node)
    {
        callCount++;
        if (node.Children.Count == 0)      // базовий випадок: листок
            return 1;

        return 1 + node.Children.Max(child => MaxDepth(child));
    }

    static void Main()
    {
        callCount = 0;
        PrintTree(catalog, 0);
        Console.WriteLine($"[printTree викликів: {callCount}]");

        callCount = 0;
        int total = CountProducts(catalog);
        Console.WriteLine($"Загальна кількість товарів: {total}");
        Console.WriteLine($"countProducts викликів: {callCount}");

        callCount = 0;
        int depth = MaxDepth(catalog);
        Console.WriteLine($"Глибина найдовшої гілки: {depth}");
        Console.WriteLine($"maxDepth викликів: {callCount}");
    }
}


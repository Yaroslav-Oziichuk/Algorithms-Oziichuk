using System;

namespace Lab05
{
    internal class Program
    {
        static void PrintAll(string title, IEnumerable<Student> items)
        {
        Console.WriteLine(title);
        foreach (var s in items)
        {
            Console.WriteLine($"{s.Surname} {s.Group} {s.Grade} {s.Year}");
        }
        Console.WriteLine();
        }

        static void Main()
        {
        List<Student> students = new List<Student>
        {
            new Student("Ткаченко",  "ІПЗ-3/1", 85, 2024),
            new Student("Бондар",    "ІПЗ-3/2", 92, 2023),
            new Student("Іваненко",  "ІПЗ-3/1", 85, 2024),
            new Student("Коваль",    "ІПЗ-3/2", 78, 2024),
            new Student("Сидоренко", "ІПЗ-3/1", 85, 2023),
            new Student("Мельник",   "ІПЗ-3/2", 92, 2024),
            new Student("Гриценко",  "ІПЗ-3/1", 78, 2023),
            new Student("Дяченко",   "ІПЗ-3/2", 85, 2023)
        };

        var sorted1 = students.OrderBy(s => s.Surname).ToList();
        PrintAll("1. За прізвищем (за абеткою):", sorted1);

        var sorted2 = students.OrderByDescending(s => s.Grade).ToList();
        PrintAll("2. За балом (від вищого до нижчого):", sorted2);

        var sorted3 = students.OrderBy(s => s.Group)
                              .ThenByDescending(s => s.Grade)
                              .ToList();
        PrintAll("3. За групою, а всередині групи за балом від вищого:", sorted3);

        var sorted2WithSurname = students.OrderByDescending(s => s.Grade)
                                         .ThenBy(s => s.Surname)
                                         .ToList();
        PrintAll("Частина 2. За балом та прізвищем за абеткою:", sorted2WithSurname);
        }
    }
}

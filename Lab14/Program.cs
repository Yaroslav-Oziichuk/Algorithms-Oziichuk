using System;
using System.Collections.Generic;
using System.Linq;

class Program
{

    // TODO 1
    static HashSet<string> VisitAll(Dictionary<string, List<string>> graph, string start)
    {
        Dictionary<string, string> parent;
        return Bfs(graph, start, out parent);
    }

    static HashSet<string> Bfs(Dictionary<string, List<string>> graph, string start,
                               out Dictionary<string, string> parent)
    {
        var visited = new HashSet<string>();
        parent = new Dictionary<string, string>();
        if (!graph.ContainsKey(start)) return visited;

        var queue = new Queue<string>();
        visited.Add(start);
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            string v = queue.Dequeue();
            foreach (string next in graph[v])
            {
                if (!visited.Contains(next))
                {
                    visited.Add(next);
                    parent[next] = v;      
                    queue.Enqueue(next);
                }
            }
        }
        return visited;
    }

    // TODO 2
    static bool Reachable(Dictionary<string, List<string>> graph, string from, string to)
    {
        return VisitAll(graph, from).Contains(to);
    }

    // TODO 3
    static List<string> FindPath(Dictionary<string, List<string>> graph, string from, string to)
    {
        Dictionary<string, string> parent;
        HashSet<string> visited = Bfs(graph, from, out parent);

        var path = new List<string>();
        if (!visited.Contains(to)) return path;   

        string current = to;
        path.Add(current);
        while (current != from)                   
        {
            current = parent[current];
            path.Add(current);
        }
        path.Reverse();
        return path;
    }

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var graph = new Dictionary<string, List<string>>();
        graph["Центр"] = new List<string> { "Парк", "Ринок" };
        graph["Парк"] = new List<string> { "Центр", "Вокзал" };
        graph["Ринок"] = new List<string> { "Центр", "Вокзал", "Стадіон" };
        graph["Вокзал"] = new List<string> { "Парк", "Ринок", "Озеро" };
        graph["Стадіон"] = new List<string> { "Ринок" };
        graph["Озеро"] = new List<string> { "Вокзал" };
        graph["Гай"] = new List<string> { "Міст" };
        graph["Міст"] = new List<string> { "Гай" };

        var all = VisitAll(graph, "Центр");
        Console.WriteLine("visitAll(\"Центр\") = " + all.Count + " вершин: " + string.Join(", ", all));
        Console.WriteLine("reachable(Центр, Озеро)  = " + Reachable(graph, "Центр", "Озеро"));
        Console.WriteLine("reachable(Центр, Гай)    = " + Reachable(graph, "Центр", "Гай"));
        Console.WriteLine("reachable(Гай, Міст)     = " + Reachable(graph, "Гай", "Міст"));

        var p1 = FindPath(graph, "Центр", "Озеро");
        Console.WriteLine("findPath(Центр, Озеро)   = " + string.Join(" → ", p1) + "  (ребер: " + (p1.Count - 1) + ")");
        var p2 = FindPath(graph, "Центр", "Стадіон");
        Console.WriteLine("findPath(Центр, Стадіон) = " + string.Join(" → ", p2) + "  (ребер: " + (p2.Count - 1) + ")");
        var p3 = FindPath(graph, "Центр", "Міст");
        Console.WriteLine("findPath(Центр, Міст)    = [" + string.Join(", ", p3) + "]  (елементів: " + p3.Count + ")");
        Console.WriteLine("недосяжних з Центру      = " + (graph.Count - all.Count));
    }
}

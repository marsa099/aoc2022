using System.Linq;

namespace Task12;

public class Node
{
    public int Id { get; set; }
    public int XPos { get; set; }
    public int YPos { get; set; }
    public int Value { get; set; }
    public bool Visited { get; set; }
    public int Distance { get; set; }
    public bool Start { get; set; }
    public bool Goal { get; set; }
}

public static class Task12
{
    private static (int, int)[] Adjacent = new[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
    private static int YMax = 0;
    private static int XMax = 0;
    private static Node Goal = new Node();
    private static List<Node> Nodes = new List<Node>();
    private static IEnumerable<Node> ParseData()
    {
        var input = File.ReadAllLines("inputs/input-12.txt");

        int id = 0;
        for (int yPos = 0; yPos < input.Count(); yPos++)
        {
            YMax++;
            for (int xPos = 0; xPos < input[yPos].Count(); xPos++)
            {
                XMax++;
                var value = input[yPos][xPos];
                var newValue = (int)value;
                if (value == 'S')
                {
                    newValue = 100;
                }
                else
                {
                    newValue = newValue - 97;
                }
                yield return new Node
                {
                    Id = id++,
                    YPos = yPos,
                    XPos = xPos,
                    Value = newValue,  // -97 will transform a into 0 and b into 1 etc...
                    Start = value == 'S',
                    Goal = value == 'E',
                    Distance = value == 'S' ? 0 : Int32.MaxValue
                };
            }
        }
    }
    // private static List<Node> AddNeighbours(Node node, IEnumerable<Node> nodes)
    // {

    //     var neighbours = new List<Node>();
    //     foreach ((int x, int y) in Adjacent)
    //     {
    //         var child = nodes.FirstOrDefault(neighbour => neighbour.XPos == node.XPos + x &&
    //                                                        neighbour.YPos == node.YPos + y); //&&
    //                                                                                          //node.Value <= node.Value + 1);
    //                                                                                          //9 <= 8 + 1 = true
    //                                                                                          //9 <= 7 + 1 = false
    //                                                                                          //9 <= 999 = true

    //         if (child != null)
    //             neighbours.Add(child);
    //     }
    //     return neighbours;
    // }

    public static void Execute()
    {
        Nodes = ParseData().ToList(); ;
        var goal = Nodes.First(x => x.Goal);
        var start = Nodes.First(x => x.Start);
        var distance = FindShortestRoute(goal, start);
        Console.WriteLine(distance);
    }
    public static int FindShortestRoute(Node goal, Node start)
    {
        var queue = new PriorityQueue<Node, int>();
        var adjacent = new[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
        queue.Enqueue(start, 0);
        while (queue.TryDequeue(out var current, out int priority))
        {
            // Console.WriteLine("Distances: " + Nodes.Count(x => x.Distance > 0));
            // Console.WriteLine("Visited: " + Nodes.Count(x => x.Visited));
            if (current.Visited)
                continue;
            Console.WriteLine($"{current.XPos}-{current.YPos}");

            current.Visited = true;
            Nodes[current.Id] = current;

            if (current.Id == goal.Id)
                return goal.Distance;
            // Console.WriteLine("Current distance: " + current.Distance);
            // Console.WriteLine("Current queue: " + queue.Count);
            foreach ((int x, int y) in adjacent)
            {
                var neighbour = Nodes.FirstOrDefault(neighbour => neighbour.XPos == current.XPos + x &&
                                                  neighbour.YPos == current.YPos + y && neighbour.Start == false);
                if (neighbour != null && neighbour.Value <= current.Value + 1)
                {
                    // if (neighbour.Visited)
                    //     continue;
                    var newDistance = current.Distance + 1;

                    if (newDistance < neighbour.Distance)
                    {
                        neighbour.Distance = newDistance;
                    }
                    if (neighbour.Goal)
                    {
                        var tt = "";
                    }
                    Nodes[neighbour.Id] = neighbour;
                    if (neighbour.Distance != Int32.MaxValue)
                    {
                        queue.Enqueue(neighbour, newDistance);
                    }



                }
            }
        }
        return goal.Distance;

    }
}
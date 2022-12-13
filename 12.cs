using System.Linq;

namespace Task12;

public class Node
{
    public int Id { get; set; }
    public int XPos { get; set; }
    public int YPos { get; set; }
    public char Value { get; set; }
    public bool Visited { get; set; }
    public int Distance { get; set; }
    public bool Start { get; set; }
    public bool Goal { get; set; }

    public Node? Parent { get; set; }
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
                var newValue = value;
                if (value == 'S')
                {
                    newValue = 'a';
                }
                else if (value == 'E')
                {
                    newValue = 'z';
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
        Dictionary<Node, int> distanceToNode = new();
        Dictionary<Node, Node> previous = new();
        distanceToNode[start] = 0;
        queue.Enqueue(start, 0);
        var list = new List<Node>();
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current.Id == goal.Id)
                break;

            foreach ((int x, int y) in adjacent)
            {
                var neighbour = Nodes.FirstOrDefault(neighbour => neighbour.XPos == current.XPos + x &&
                                                  neighbour.YPos == current.YPos + y && neighbour.Start == false);
                if (neighbour != null && neighbour.Value <= current.Value + 1)
                {
                    var newDistance = distanceToNode[current] + 1;

                    if (newDistance < (distanceToNode.ContainsKey(neighbour) ? distanceToNode[neighbour] : int.MaxValue))
                    {
                        distanceToNode[neighbour] = newDistance;
                        previous[neighbour] = current;
                        queue.Enqueue(neighbour, newDistance);
                    }
                }
            }
        }


        while (goal != null)
        {
            list.Add(goal);
            goal = previous.ContainsKey(goal) ? previous[goal] : null;
        }

        return list.Count() - 1;

    }
}
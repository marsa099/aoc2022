using System.Linq;

namespace Task12;

public class Node
{
    public int Id { get; set; }
    public int XPos { get; set; }
    public int YPos { get; set; }
    public int Value { get; set; }
    public List<Node> Neighbours { get; set; } = new List<Node>();
}

public static class Task12
{
    private static (int, int)[] Adjacent = new[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
    private static int YMax = 0;
    private static int XMax = 0;
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
                yield return new Node
                {
                    Id = id++,
                    YPos = yPos,
                    XPos = xPos,
                    Value = (int)input[yPos][xPos] - 97 // -97 will transform a into 0 and b into 1 etc...
                };
            }
        }
    }
    private static List<Node> AddNeighbours(Node node, IEnumerable<Node> nodes)
    {
        var neighbours = new List<Node>();
        int count = 0;
        foreach ((int x, int y) in Adjacent)
        {
            var child = nodes.FirstOrDefault(neighbour => neighbour.XPos == node.XPos + x &&
                                                           neighbour.YPos == node.YPos + y); //&&
                                                                                             //node.Value <= node.Value + 1);
                                                                                             //9 <= 8 + 1 = true
                                                                                             //9 <= 7 + 1 = false
                                                                                             //9 <= 999 = true

            if (child != null)
                neighbours.Add(child);
        }
        return neighbours;
    }

    public static void Execute()
    {


        // foreach(int d in "abcdefghijklmnopqrstuvwxyz".Select(x => (int)x))
        //     Console.WriteLine(d);

        // 1. Read and parse the values
        // 2. Add the neigbours
        // 3. Do the magic
        Console.Write("Parsing ...");
        var nodes = ParseData();
        Console.WriteLine(" DONE");

        var neighbours = AddNeighbours(nodes.First(), nodes);
        
        foreach (var node in neighbours)
            Console.WriteLine($"{node.Value} {string.Join(", ", node.Neighbours.Select(x => x.Value))}");


    }

}
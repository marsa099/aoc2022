using System.Linq;

namespace Task12;

public class Node
{
    public int Id { get; set; }
    public int XPos { get; set; }
    public int YPos { get; set; }
    public int Value { get; set; }
}

public static class Task12
{
    public static void Execute()
    {
        var input = File.ReadAllLines("inputs/input-12.txt");

        // foreach(int d in "abcdefghijklmnopqrstuvwxyz".Select(x => (int)x))
        //     Console.WriteLine(d);

        // 1. Read and parse the values
        // 2.

        var nodes = new List<Node>();

        int id = 0;
        for (int yPos = 0; yPos < input.Count(); yPos++)
        {
            for (int xPos = 0; xPos < input[yPos].Count(); xPos++)
            {
                nodes.Add(new Node 
                { 
                    Id = id++,
                    YPos = yPos,
                    XPos = xPos,
                    Value = (int)input[yPos][xPos] - 97 // -97 will transform a into 0 and b into 1 etc...
                });
            }
        }

        // foreach(var row in nodes.Chunk(162))
        // {
        //     Console.WriteLine(string.Join("", row.Select(x => x.Value)));
        // }


    }
}
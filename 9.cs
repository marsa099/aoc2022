using System.Drawing;

public static class Task9
{

    // TODO: Take into account the positions the tail travels. Not only the final position for each move......
    public static void Execute()
    {
        var input = File.ReadAllLines("inputs/input-9.txt");

        List<Point> visitedPoints = new List<Point>();
        Point previousHeadPosition = new Point(0, 0);
        Point head = new Point(0, 0);
        Point tail = new Point(0, 0);
        List<Point> tailMoves;

        int steps = 0;

        foreach (var line in input)
        {
            tailMoves = new List<Point>();
            steps = int.Parse(line.Split(' ')[1].ToString());
            previousHeadPosition = head;
            Console.Write($"Instruction: {line}: ");

            switch (line.Split(' ')[0])
            {
                case "U":
                    // 
                    for (int i = head.Y -1; i > head.Y - steps; i--)
                        tailMoves.Add(new Point(head.X, i));
                    head.Y -= steps;
                    break;
                case "R":
                    for (int i = head.X +1; i < head.X + steps; i++)
                        tailMoves.Add(new Point(i, head.Y));
                    head.X += steps;
                    break;
                case "L":
                    for (int i = head.X -1; i > head.X - steps; i--)
                        tailMoves.Add(new Point(i, head.Y));
                    head.X -= steps; ;
                    break;
                case "D":
                    for (int i = head.Y +1; i < head.Y + steps; i++)
                        tailMoves.Add(new Point(head.X, i));
                    head.Y += steps;
                    break;
                default:
                    Console.WriteLine("This should not happen");
                    break;
            }
            // Move the tail if needed
            if (NeedToMoveTheTail(head, tail))
            {
                if(tailMoves.Any())
                {
                    visitedPoints.AddRange(tailMoves);
                    tail = tailMoves.Last();
                }
                else
                {
                    visitedPoints.Add(previousHeadPosition);
                    tail = previousHeadPosition;
                }
                Console.WriteLine($" {tailMoves.Count()}");
            }
        }
        Console.WriteLine("Visisted points: " + visitedPoints.Count());
        Console.WriteLine("Visisted points distinct " + visitedPoints.Distinct().Count());
    }

    private static bool NeedToMoveTheTail(Point head, Point tail)
    {
        int xDiff = 0;
        int yDiff = 0;

        Console.Write($"head: ({head.X},{head.Y}) tail: ({tail.X},{tail.Y}) ");

        xDiff = head.X - tail.X;
        yDiff = head.Y - tail.Y;

        Console.WriteLine("");
        return Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)) > 1;
    }
}

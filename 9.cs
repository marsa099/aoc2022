using System.Drawing;

public static class Task9
{

    // TODO: Take into account the positions the tail travels. Not only the final position for each move......
    public static void Execute()
    {
        var input = File.ReadAllLines("inputs/input-9 copy.txt");

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
            switch (line.Split(' ')[0])
            {
                case "U":
                    // 
                    for (int i = head.Y; i > head.Y - steps; i--)
                        tailMoves.Add(new Point(head.X, i));
                    head.Y -= steps;
                    break;
                case "R":
                    for (int i = head.X; i < head.X + steps; i++)
                        tailMoves.Add(new Point(i, head.Y));
                    head.X += steps;
                    break;
                case "L":
                    for (int i = head.X; i > head.X - steps; i--)
                        tailMoves.Add(new Point(i, head.Y));
                    head.X -= steps; ;
                    break;
                case "D":
                    for (int i = head.Y; i < head.Y + steps; i++)
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
                tail = tailMoves.Last();
                visitedPoints.AddRange(tailMoves);
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

        // Case 1: Both points positive
        if (head.X >= 0 && tail.X >= 0)
            xDiff = Math.Abs(head.X - tail.X);
        // Case 2: Both points negative
        else if (head.X < 0 && tail.X < 0)
            xDiff = Math.Abs(Math.Abs(head.X) - Math.Abs(tail.X));
        // Case 3: Head positive, tail negative
        else if (head.X >= 0 && tail.X < 0) // 7--3 = 10
            xDiff = Math.Abs(head.X - tail.X);
        // Case 4  Head negative, tail positive
        else if (head.X < 0 && tail.X >= 0)
            xDiff = Math.Abs(tail.X - head.X);
        else
            Console.WriteLine($"Scenario not covered: head.X: {head.X} tail.X: {tail.X}");


        // Case 1: Both points positive
        if (head.Y >= 0 && tail.Y >= 0)
            yDiff = Math.Abs(head.Y - tail.Y);
        // Case 2: Both points negative
        else if (head.Y < 0 && tail.Y < 0)
            yDiff = Math.Abs(Math.Abs(head.Y) - Math.Abs(tail.Y));
        // Case 3: Head positive, tail negative
        else if (head.Y >= 0 && tail.Y < 0) // 7--3 = 10
            yDiff = Math.Abs(head.Y - tail.Y);
        // Case 4  Head negative, tail positive
        else if (head.Y < 0 && tail.Y >= 0)
            yDiff = Math.Abs(tail.Y - head.Y);
        else
            Console.WriteLine($"Scenario not covered: head.Y: {head.Y} tail.Y: {tail.Y}");

        if (xDiff > 1 || yDiff > 1)
        {
            Console.WriteLine(" MOVE");
            return true;
        }
        Console.WriteLine("");
        return false;
    }
}

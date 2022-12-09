using System.Drawing;

public static class Task9
{
    public static void Execute()
    {
        var input = File.ReadAllLines("inputs/input-9.txt");

        List<Point> visitedPoints = new List<Point>();
        Point previousHeadPosition = new Point(0, 0);
        Point head = new Point(0, 0);
        Point tail = new Point(0, 0);

        foreach (var line in input)
        {
            previousHeadPosition = head;

            switch (line.Split(' ')[0])
            {
                case "U":
                    --head.Y;
                    break;
                case "R":
                    ++head.X;
                    break;
                case "L":
                    --head.X;
                    break;
                case "D":
                    ++head.Y;
                    break;
                default:
                    Console.WriteLine("This should not happen");
                    break;
            }
            // Move the tail if needed
            if (NeedToMoveTheTail(head, tail))
            {
                tail = previousHeadPosition;
                visitedPoints.Add(tail);
            }
        }
        Console.WriteLine("Visisted points: " + visitedPoints.Count());
        Console.WriteLine("Visisted points distinct " + visitedPoints.Distinct().Count());
    }

    private static bool NeedToMoveTheTail(Point head, Point tail)
    {
        var xDiff = Math.Abs(head.X - tail.X);
        var yDiff = Math.Abs(head.Y - tail.Y);

        // TODO: This is probably not rights
        if (xDiff > 1 || yDiff > 1 || xDiff + yDiff > 2)
            return true;
        return false;
    }
}

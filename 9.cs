using System.Drawing;

public static class Task9
{
    public static List<Point> knots = Enumerable.Repeat(0, 10).Select(x => new Point(0, 0)).ToList();
    public static List<Point> visitedPointsForFirstTail = new List<Point> { new Point(0, 0) };
    public static List<Point> visitedPointsForLastTail = new List<Point> { new Point(0, 0) };

    public static void Execute()
    {
        var input = File.ReadAllLines("inputs/input-9.txt");
        int steps = 0;
        foreach (var line in input)
        {
            steps = int.Parse(line.Split(' ')[1].ToString());

            for (int i = 0; i < steps; i++)
            {
                switch (line.Split(' ')[0])
                {
                    case "U":
                        knots[0] = new Point(knots[0].X, knots[0].Y - 1);
                        break;
                    case "D":
                        knots[0] = new Point(knots[0].X, knots[0].Y + 1);
                        break;
                    case "R":
                        knots[0] = new Point(knots[0].X + 1, knots[0].Y);
                        break;
                    case "L":
                        knots[0] = new Point(knots[0].X - 1, knots[0].Y);
                        break;
                    default:
                        Console.WriteLine("This should not happen");
                        break;
                }
                MoveTail();
            }
        }
        Console.WriteLine("Visisted points first tail: " + visitedPointsForFirstTail.Distinct().Count());
        Console.WriteLine("Visisted points last tail " + visitedPointsForLastTail.Distinct().Count());
    }

    private static void MoveTail()
    {
        for (int j = 1; j < knots.Count(); j++)
        {
            if (NeedToMoveTheTail(knots[j - 1], knots[j]))
            {
                var knotX = knots[j].X;
                if (knots[j - 1].X < knots[j].X)
                    knotX--;
                else if (knots[j - 1].X > knots[j].X)
                    knotX++;

                var knotY = knots[j].Y;
                if (knots[j - 1].Y < knots[j].Y)
                    knotY--;
                else if (knots[j - 1].Y > knots[j].Y)
                    knotY++;

                knots[j] = new Point(knotX, knotY);
                if (j == 1)
                    visitedPointsForFirstTail.Add(knots[j]);

                if (j == 9)
                    visitedPointsForLastTail.Add(knots[j]);
            }
        }
    }

    private static bool NeedToMoveTheTail(Point head, Point tail)
    {
        if (Math.Abs(head.X - tail.X) > 1 || Math.Abs(head.Y - tail.Y) > 1)
            return true;

        return false;
    }
}

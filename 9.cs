using System.Drawing;

public static class Task9
{

    // TODO: Take into account the positions the tail travels. Not only the final position for each move......
    public static void Execute()
    {
        var input = File.ReadAllLines("inputs/input-9.txt");

        List<Point> knots = new List<Point>
        {
            new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 0),
            new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 0)
        };
        List<Point> visitedPoints = new List<Point> { new Point(0, 0) };
        List<Point> visitedPointsForFirstTail = new List<Point> { new Point(0, 0) };

        int steps = 0;

        foreach (var line in input)
        {
            steps = int.Parse(line.Split(' ')[1].ToString());
            Console.Write($"Instruction: {line}: ");

            for (int i = 0; i < steps; i++)
            {
                switch (line.Split(' ')[0])
                {
                    case "U":
                        knots[0] = new Point(knots[0].X, knots[0].Y - 1);

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
                                    visitedPoints.Add(knots[j]);
                            }
                        }
                        break;
                    case "D":
                        knots[0] = new Point(knots[0].X, knots[0].Y + 1);

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
                                    visitedPoints.Add(knots[j]);
                            }
                        }
                        break;
                    case "R":
                        knots[0] = new Point(knots[0].X + 1, knots[0].Y);

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
                                    visitedPoints.Add(knots[j]);
                            }
                        }
                        break;
                    case "L":
                        knots[0] = new Point(knots[0].X - 1, knots[0].Y);

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
                                    visitedPoints.Add(knots[j]);
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("This should not happen");
                        break;
                }
            }
        }
        Console.WriteLine("Visisted points: " + visitedPoints.Count());
        Console.WriteLine("Visisted points distinct " + visitedPoints.Distinct().Count());
        Console.WriteLine("Visisted points tail1 distinct " + visitedPointsForFirstTail.Distinct().Count());
    }

    private static bool NeedToMoveTheTail(Point head, Point tail)
    {
        Console.Write($"head: ({head.X},{head.Y}) tail: ({tail.X},{tail.Y}) ");

        double xDiff = head.X - tail.X;
        double yDiff = head.Y - tail.Y;

        Console.WriteLine("");

        if (Math.Abs(xDiff) > 1 || Math.Abs(yDiff) > 1)
            return true;

        return false;

        //return Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)) >= 2;
    }
}

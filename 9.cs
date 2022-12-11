using System.Drawing;

public static class Task9
{

    // TODO: Take into account the positions the tail travels. Not only the final position for each move......
    public static void Execute()
    {
        var input = File.ReadAllLines("inputs/input-9.txt");

        Point previousHeadPosition = new Point(0, 0);
        Point head = new Point(0, 0);
        List<Point> tails = new List<Point>
        {
            new Point(0, 0), new Point(0, 0), new Point(0, 0),
            new Point(0, 0), new Point(0, 0), new Point(0, 0),
            new Point(0, 0), new Point(0, 0), new Point(0, 0)
        };
        List<Point> visitedPoints = new List<Point> { new Point(0, 0) };
        List<Point> visitedPointsForFirstTail = new List<Point> { new Point(0, 0) };

        int steps = 0;

        foreach (var line in input)
        {
            steps = int.Parse(line.Split(' ')[1].ToString());
            previousHeadPosition = head;
            Console.Write($"Instruction: {line}: ");

            switch (line.Split(' ')[0])
            {
                case "U":
                    for (int i = head.Y; i >= previousHeadPosition.Y - steps; i--)
                    {
                        Console.WriteLine("For loop #1");
                        head.Y = i;
                        if (NeedToMoveTheTail(head, tails[0]))
                        {
                            tails[0] = new Point(head.X, head.Y + 1);
                            visitedPointsForFirstTail.Add(tails[0]);

                            for (int j = 1; j < tails.Count(); j++)
                            {
                                if (NeedToMoveTheTail(tails[j - 1], tails[j]))
                                {
                                    tails[j] = new Point(tails[j - 1].X, tails[j - 1].Y + 1);

                                    if (j == 8)
                                        visitedPoints.Add(tails[8]);
                                }
                                else
                                    break;

                            }
                        }
                    }
                    break;
                case "D":
                    for (int i = head.Y; i <= previousHeadPosition.Y + steps; i++)
                    {
                        Console.WriteLine("For loop #2");
                        head.Y = i;
                        if (NeedToMoveTheTail(head, tails[0]))
                        {
                            tails[0] = new Point(head.X, head.Y - 1);
                            visitedPointsForFirstTail.Add(tails[0]);

                            for (int j = 1; j < tails.Count(); j++)
                            {
                                if (NeedToMoveTheTail(tails[j - 1], tails[j]))
                                {
                                    tails[j] = new Point(tails[j - 1].X, tails[j - 1].Y - 1);
                                    if (j == 8)
                                        visitedPoints.Add(tails[8]);
                                }
                                else
                                    break;
                            }
                        }
                    }
                    break;
                case "R":
                    for (int i = head.X; i <= previousHeadPosition.X + steps; i++)
                    {
                        Console.WriteLine("For loop #3");
                        head.X = i;
                        if (NeedToMoveTheTail(head, tails[0]))
                        {
                            tails[0] = new Point(head.X - 1, head.Y);
                            visitedPointsForFirstTail.Add(tails[0]);

                            for (int j = 1; j < tails.Count(); j++)
                            {
                                if (NeedToMoveTheTail(tails[j - 1], tails[j]))
                                {
                                    tails[j] = new Point(tails[j - 1].X - 1, tails[j - 1].Y);

                                    if (j == 8)
                                        visitedPoints.Add(tails[8]);
                                }
                                else
                                    break;
                            }
                        }
                    }
                    break;
                case "L":
                    for (int i = head.X; i >= previousHeadPosition.X - steps; i--)
                    {
                        head.X = i;
                        if (NeedToMoveTheTail(head, tails[0]))
                        {
                            tails[0] = new Point(head.X + 1, head.Y);
                            visitedPointsForFirstTail.Add(tails[0]);

                            for (int j = 1; j < tails.Count(); j++)
                            {
                                if (NeedToMoveTheTail(tails[j - 1], tails[j]))
                                {
                                    tails[j] = new Point(tails[j - 1].X + 1, tails[j - 1].Y);

                                    if (j == 8)
                                        visitedPoints.Add(tails[8]);
                                }
                                else
                                    break;
                            }
                        }
                    }

                    break;
                default:
                    Console.WriteLine("This should not happen");
                    break;
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

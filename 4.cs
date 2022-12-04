using System.Text.RegularExpressions;

public static class Task4
{
    public static Regex re = new Regex(@"(\d+)-(\d+),(\d+)-(\d+)");

    public static void Execute()
    {
        var input = File.ReadAllLines("inputs/input-4.txt");
        int total = 0;

        foreach (var line in input)
        {
            var parsedLine = re.Match(line);
            var firstSection = Enumerable.Range(int.Parse(parsedLine.Groups[1].Value), 
                                                 int.Parse(parsedLine.Groups[2].Value) - int.Parse(parsedLine.Groups[1].Value) + 1);
            var secondSection = Enumerable.Range(int.Parse(parsedLine.Groups[3].Value), 
                                                 int.Parse(parsedLine.Groups[4].Value) - int.Parse(parsedLine.Groups[3].Value) + 1);
            var notFullyOverlap1 = firstSection.Any(x => !secondSection.Contains(x));
            var notFullyOverlap2 = secondSection.Any(x => !firstSection.Contains(x));

            if (!notFullyOverlap1 || !notFullyOverlap2)
                total += 1;
        }
        Console.WriteLine("4a: " + total);
    }
}

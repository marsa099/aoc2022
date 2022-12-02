public static class Task1
{
    public static void Execute()
    {
        var input = File.ReadAllLines("inputs/input-1.txt");

        var allBackpackTotal = new List<int>();

        int currentBackpack = 0;

        foreach (var line in input)
        {
            if (string.IsNullOrEmpty(line))
            {
                allBackpackTotal.Add(currentBackpack);
                currentBackpack = 0;
                continue;
            }
            currentBackpack += int.Parse(line);
        }

        // 1a
        Console.WriteLine(allBackpackTotal.Max());

        // 1b
        allBackpackTotal.Sort();
        allBackpackTotal.Reverse();
        Console.WriteLine(string.Join(",", allBackpackTotal.Take(3).Sum()));
    }
}

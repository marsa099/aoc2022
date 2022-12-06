public static class Task6
{
    
    public static void Execute()
    {
        var input = File.ReadAllText("inputs/input-6.txt");
        int total = 0;

        int skipCount = 0;
        int takeCount = 4;

        var complete = false;
        while (!complete)
        {
            var next = input.Skip(skipCount).Take(takeCount);
            if (next.Distinct().Count() == next.Count())
            {
                complete = true;
                break;
            }
            skipCount++;
        }

        Console.WriteLine($"6a: {skipCount + takeCount}");
    }

}

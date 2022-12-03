public static class Task3
{
    public static void Execute()
    {
        var input = File.ReadAllLines("inputs/input-3.txt");

        int total = 0;

        foreach (var line in input)
        {
            // Split the line in half
            var chunks = line.Chunk(line.Length / 2).ToList();
            var first = chunks[0];
            var second = chunks[1];

            // Find the letter that exists in both halfs
            var sameChar = first.First(x => second.Any(y => x == y));

            if (sameChar >= 97)
                total += sameChar - 96;
            else if (sameChar < 97)
                total += sameChar - 38;
        }

        Console.WriteLine(total);

    }
}

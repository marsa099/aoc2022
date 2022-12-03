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
        Console.WriteLine("3a: " + total);
        total = 0;


        foreach (var group in input.Chunk(3))
        {
            char c = group[0].First(x =>
                (group[0].Count(a => x == a) >= 1) &&
                (group[1].Count(y => x == y) >= 1) &&
                (group[2].Count(z => x == z) >= 1)
            );

            if (c >= 97)
                total += c - 96;
            else if (c < 97)
                total += c - 38;
        }

        Console.WriteLine("3b: " + total);

        total = input.Chunk(3).Sum(x =>
            {
                int value =  x[0].First(y =>
                    (x[0].Count(a => y == a) >= 1) &&
                    (x[1].Count(b => y == b) >= 1) &&
                    (x[2].Count(c => y == c) >= 1));

                return value >= 97 ? value - 96 : value - 38;
            }
        );
        Console.WriteLine("3b (option 2): " + total);
    }
}

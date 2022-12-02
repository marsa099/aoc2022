public static class Task2
{

    public static int calcScore(char opponent, char me)
    {
        int roundScore = 0;

        if (me == 'X') // Sten
        {
            roundScore += 1;
            if (opponent == 'A') //Sten = draw
                roundScore += 3;
            if (opponent == 'B') // Påse = loose
                roundScore += 0;
            if (opponent == 'C') // Sax = win
                roundScore += 6;
        }
        else if (me == 'Y') // Påse
        {
            roundScore += 2;
            if (opponent == 'A') // Sten = win
                roundScore += 6;
            if (opponent == 'B') // Påse = draw
                roundScore += 3;
            if (opponent == 'C') // Sax = loose
                roundScore += 0;
        }
        else if (me == 'Z') // Sax
        {
            roundScore += 3;
            if (opponent == 'A') // Sten = loose
                roundScore += 0;
            if (opponent == 'B') // Påse = win
                roundScore += 6;
            if (opponent == 'C') // Sax = draw
                roundScore += 3;
        }

        return roundScore;
    }

    public static void Execute()
    {
        var input = File.ReadAllLines("inputs/input-2.txt");
        int total = 0;

        foreach (var line in input)
        {
            var opponent = line[0];
            var me = line[2];

            total += calcScore(opponent, me);
        }

        // 1a
        Console.WriteLine($"Total {total}");

    }
}

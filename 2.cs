public static class Task2
{

    public static int calcScore1(char opponent, char me)
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

    public static int calcScore2(char opponent, char result)
    {
        int roundScore = 0;

        if (result == 'X') // Loose
        {
            roundScore += 0;
            if (opponent == 'A') // Sten. Loose = Sax = 3
                roundScore += 3;
            if (opponent == 'B') // Påse. Loose = Sten = 1
                roundScore += 1;
            if (opponent == 'C') // Sax. Loose = Påse = 2
                roundScore += 2;
        }
        else if (result == 'Y') // Draw
        {
            roundScore += 3;
            if (opponent == 'A') // Sten. Draw = Sten = 1
                roundScore += 1;
            if (opponent == 'B') // Påse. Draw = Påse = 2
                roundScore += 2;
            if (opponent == 'C') // Sax. Draw = Sax = 3
                roundScore += 3;
        }
        else if (result == 'Z') // Win
        {
            roundScore += 6;
            if (opponent == 'A') // Sten. Win = Påse = 2
                roundScore += 2;
            if (opponent == 'B') // Påse. Win = Sax = 3
                roundScore += 3;
            if (opponent == 'C') // Sax. Win = Sten = 1
                roundScore += 1;
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

            total += calcScore2(opponent, me);
        }

        // 1a
        Console.WriteLine($"Total {total}");

    }
}

public static class Task8
{
    public static void Execute()
    {
        var input = File.ReadAllLines("inputs/input-8.txt");

        int visibleCount = 0;
        int scenicScore = 0;

        for (int row = 0; row < input.Count(); row++)
        {
            for (int col = 0; col < input[row].Count(); col++)
            {
                var length = int.Parse(input[row][col].ToString());

                if (row == 0 || row == input.Count() - 1 || col == 0 || col == input[row].Count() - 1)
                {
                    visibleCount++;
                }
                else
                {
                    if (GetVisibility(input, length, row, col))
                        visibleCount++;
                    scenicScore = Math.Max(scenicScore, GetScenicScore(input, length, row, col));
                }
            }
        }

        Console.WriteLine("8a " + visibleCount);
        Console.WriteLine("8b " + scenicScore);
    }

    private static int GetScenicScore(string[] input, int height, int row, int col)
    {
        // Score from left (also include the blocking tree (+1))
        int leftScore = input[row].Take(col).Select(x => int.Parse(x.ToString())).Reverse().TakeWhile(x => x < height).Count() + 1;

        // However, if we have free sight all the way we shouldnt take +1 since thats outside the grid
        if (leftScore > input[row].Take(col).Count())
        {
            leftScore--;
        }

        // Score from right (also include the blocking tree (+1))
        int rightScore = input[row].Skip(col + 1).Select(x => int.Parse(x.ToString())).TakeWhile(x => x < height).Count() + 1;

        // However, if we have free sight all the way we shouldnt take +1 since thats outside the grid
        if (rightScore > input[row].Take(col).Count())
        {
            rightScore--;
        }

        // Score from top
        int topScore = 0;
        for (int i = row - 1; i >= 0; i--)
        {
            var value = int.Parse(input[i][col].ToString());

            // Increment first since we want to include the blocking tree
            topScore++;
            // A taller tree was found = not visible from top
            if (value >= height)
                break;
        }

        // Score from bottom
        int bottomScore = 0;
        for (int i = row + 1; i < input.Count(); i++)
        {
            var value = int.Parse(input[i][col].ToString());

            // Increment first since we want to include the blocking tree
            bottomScore++;

            // A taller tree was found = not visible from top
            if (value >= height)
                break;
        }

        return leftScore * rightScore * topScore * bottomScore;
    }

    private static bool GetVisibility(string[] input, int height, int row, int col)
    {
        // Visible from left?
        if (input[row].Take(col).Select(x => int.Parse(x.ToString())).Any(x => x >= height) == false)
            return true;

        // Visible from right?
        if (input[row].Skip(col + 1).Select(x => int.Parse(x.ToString())).Any(x => x >= height) == false)
            return true;

        // Visible from top?
        for (int i = row - 1; i >= 0; i--)
        {
            var value = int.Parse(input[i][col].ToString());

            // A taller tree was found = not visible from top
            if (value >= height)
                break;

            // If we reach this line, we have never reached the break statement = no taller tree was found
            else if (value < height && i == 0)
                return true;
        }

        // Visible from bottom?
        for (int i = row + 1; i < input.Count(); i++)
        {
            var value = int.Parse(input[i][col].ToString());

            // A taller tree was found = not visible from top
            if (value >= height)
                break;

            // If we reach this line, we have never reached the break statement = no taller tree was found
            else if (value < height && i == input.Count() - 1)
                return true;
        }

        return false;
    }
}

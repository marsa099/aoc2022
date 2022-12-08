public class Tree
{
    public int Row { get; set; }
    public int Column { get; set; }
    public int Length { get; set; }
    // public bool VisibleFromTop { get; set; } = false;
    // public bool VisibleFromRight { get; set; } = false;
    // public bool VisibleFromLeft { get; set; } = false;
    // public bool VisibleFromBottom { get; set; } = false;

    // public bool IsVisible => VisibleFromBottom || VisibleFromLeft || Vis
    public bool IsVisible { get; set; } = false;
}

public static class Task8
{
    public static void Execute()
    {
        var input = File.ReadAllLines("inputs/input-8.txt");

        var trees = new List<Tree>();

        // 0 shortest, 9 = tallest
        // visible if trees up/down/left/right are shorter

        int visibleCount = 0;
        int scenicScore = 0;


        for (int row = 0; row < input.Count(); row++)
        {
            for (int col = 0; col < input[row].Count(); col++)
            {
                var length = int.Parse(input[row][col].ToString());

                if (row == 0 || row == input.Count() - 1 || col == 0 || col == input[row].Count() - 1)
                {
                    //trees.Add(new Tree { Row = row, Column = col, Length = length, IsVisible = true });
                    visibleCount++;
                }
                else
                {
                    if (GetVisibility(input, length, row, col))
                        visibleCount++;
                    scenicScore = Math.Max(scenicScore, GetScenicScore(input, length, row, col));
                }
            }

            // Save each tree with an ID since we dont want to count each tree twice if its visible from 2 directions

            // Parse line from left to right
            // Parse line from right to left
        }

        // 8a
        Console.WriteLine("8a " + visibleCount);
        Console.WriteLine("8b " + scenicScore);


    }


    private static int GetHighestPossibleScenicScore(string[] input, int height, int row, int col)
    {
        // Score from left
        int leftScore = input[row].Take(col).Select(x => int.Parse(x.ToString())).Reverse().Count();

        // Score from right
        int rightScore = input[row].Skip(col + 1).Select(x => int.Parse(x.ToString())).Count();

        // Score from top
        int topScore = 0;
        for (int i = row - 1; i >= 0; i--)
        {
            topScore++;
        }

        // Score from bottom
        int bottomScore = 0;
        for (int i = row + 1; i < input.Count(); i++)
        {
            bottomScore++;
        }

        return leftScore * rightScore * topScore * bottomScore;
    }

    private static int GetScenicScore(string[] input, int height, int row, int col)
    {
        // TODO: Count the actual blocking tree as well
        // The problem will be when there is a non blocking tree at the edge. So we cant just +1 on all scores :(

        // Score from left
        int leftScore = input[row].Take(col).Select(x => int.Parse(x.ToString())).Reverse().TakeWhile(x => x < height).Count();

        // Score from right
        int rightScore = input[row].Skip(col + 1).Select(x => int.Parse(x.ToString())).TakeWhile(x => x < height).Count();

        // Score from top
        int topScore = 0;
        for (int i = row - 1; i >= 0; i--)
        {
            var value = int.Parse(input[i][col].ToString());

            // A taller tree was found = not visible from top
            if (value >= height)
                break;

            else if (value < height)
                topScore++;
        }

        // Score from bottom
        int bottomScore = 0;
        for (int i = row + 1; i < input.Count(); i++)
        {
            var value = int.Parse(input[i][col].ToString());

            // A taller tree was found = not visible from top
            if (value >= height)
                break;

            else if (value < height)
                bottomScore++;
        }

        if(row == 1 && col == 7)
            Console.WriteLine($"{leftScore}, {rightScore}, {topScore}, {bottomScore}, {leftScore * rightScore * topScore * bottomScore}");

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

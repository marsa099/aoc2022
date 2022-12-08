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
    public bool IsVisible {get; set; } = false;
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
        int edgeCount = 0;

        
        for (int row = 0; row < input.Count(); row++)
        {
            for (int col = 0; col < input[row].Count(); col++)
            {
                var length = int.Parse(input[row][col].ToString());
                
                if(row == 0 || row == input.Count() -1 || col == 0 || col == input[row].Count() -1)
                {
                    //trees.Add(new Tree { Row = row, Column = col, Length = length, IsVisible = true });
                    visibleCount++;
                    edgeCount++;
                }
                else
                {
                    if (GetVisibility(input, length, row, col))
                        visibleCount++;
                    //trees.Add(new Tree { Row = row, Column = col, Length = length, IsVisible = GetVisibility(input, length, row, col)});
                }
            }

           // Save each tree with an ID since we dont want to count each tree twice if its visible from 2 directions

           // Parse line from left to right
           // Parse line from right to left
        }

        // 8a
        Console.WriteLine("8a " + visibleCount);
        Console.WriteLine("8a " + edgeCount);

        
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

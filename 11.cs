public class Monkey
{
    public int Id;
    public List<int> Items = new List<int>();
    public Func<int, int> Operation;
}

public static class Task11
{


    public static void Execute()
    {
        var input = File.ReadAllLines("inputs/input-11.txt");
        
        var x = new Monkey { Id = 1, Operation = (oldValue) => { return oldValue * 19; }};

        foreach(var metadata in input.Chunk(7))
        {
            int monkeyId = int.Parse(metadata[0].Replace("Monkey ", "").Replace(":", ""));   
            List<int> items = metadata[1].Replace("  Starting items: ", "").Split(", ").Select(x => int.Parse(x)).ToList(); // Kolla om vi behöver göra reverse här?
            Func<int, int> operation = ParseOperation(metadata[2].Replace("  Operation: new = ", ""));
            Console.WriteLine($"'{monkeyId}' '{string.Join(", ", items)}' operation on 3 {operation(3)}");
        }
        Console.WriteLine("11a: ");
    }

    private static Func<int, int> ParseOperation(string operation)
    {
        Console.WriteLine(operation);
        var formula = operation.Split(" ");
        bool numberFirst = true;
        int value = 0;
        if (!int.TryParse(formula[0], out value))
        {
            numberFirst = false;
            _ = int.TryParse(formula[2], out value);
        }

        switch(formula[1])
        {
            case "+":
                if (value != 0)
                    return new Func<int, int>((oldValue) => value + oldValue );
                else
                    return new Func<int, int>((oldValue) => oldValue + oldValue );
            case "-":
                if (value != 0)
                    return numberFirst ? new Func<int, int>((oldValue) => value - oldValue ) : new Func<int, int>((oldValue) => oldValue - value );
                else
                    return new Func<int, int>((oldValue) => oldValue - oldValue );
            case "*":
                if (value != 0)
                    return new Func<int, int>((oldValue) => value * oldValue );
                else
                    return new Func<int, int>((oldValue) => oldValue * oldValue );
            default:
                Console.WriteLine("This should not happen");
                break;            
        }

        return new Func<int, int>((x) => 0);



    }
}

public class Monkey
{
    public int Id;
    public Queue<int> Items = new Queue<int>();
    public Func<int, int> Operation;
    public int TestDivision;
    public bool Test(int x) => x % TestDivision == 0;
    public int ThrowTo(int x) => Test(x) ? SendToIfTrue : SendToIfFalse;
    public int SendToIfTrue;
    public int SendToIfFalse;
}

public static class Task11
{


    public static void Execute()
    {
        var input = File.ReadAllLines("inputs/input-11-testdata.txt");

        var x = new Monkey { Id = 1, Operation = (oldValue) => { return oldValue * 19; } };

        var monkeys = new List<Monkey>();

        foreach (var metadata in input.Chunk(7))
        {
            var monkeyId = int.Parse(metadata[0].Replace("Monkey ", "").Replace(":", ""));
            var items = metadata[1].Replace("  Starting items: ", "").Split(", ").Select(x => int.Parse(x)); // Kolla om vi behöver göra reverse här?
            var operation = ParseOperation(metadata[2].Replace("  Operation: new = ", ""));
            var testDivision = int.Parse(metadata[3].Replace("  Test: divisible by ", ""));
            var sendToIfTrue = int.Parse(metadata[4].Replace("    If true: throw to monkey ", ""));
            var sendToIfFalse = int.Parse(metadata[5].Replace("    If false: throw to monkey ", ""));

            var parsedMonkey = new Monkey
            {
                Id = monkeyId,
                Operation = operation,
                Items = new Queue<int>(items),
                TestDivision = testDivision,
                SendToIfTrue = sendToIfTrue,
                SendToIfFalse = sendToIfFalse
            };
            monkeys.Add(parsedMonkey);

            foreach(var monkey in monkeys)
            {
                if (!monkey.Items.TryDequeue(out int itemToThrow))
                    continue;

                // Increase worryLevel on item
                Console.WriteLine($"Before inspect: {itemToThrow}");
                itemToThrow = monkey.Operation(itemToThrow);
                Console.WriteLine($"Inspect: {itemToThrow}");
                // No worries, decrease worry level
                itemToThrow /= 3;
                Console.WriteLine($"No worries: {itemToThrow}");

                monkey.Test(itemToThrow);

                Console.WriteLine($"Will throw to {monkey.ThrowTo(itemToThrow)}");

            }

            //Console.WriteLine($"'{monkeyId}' '{string.Join(", ", items)}' operation on 3 {operation(3)}");
        }
        Console.WriteLine("11a: ");
    }






    private static Func<int, int> ParseOperation(string operation)
    {
        //Console.WriteLine(operation);
        var formula = operation.Split(" ");
        bool numberFirst = true;
        int value = 0;
        if (!int.TryParse(formula[0], out value))
        {
            numberFirst = false;
            _ = int.TryParse(formula[2], out value);
        }

        switch (formula[1])
        {
            case "+":
                if (value != 0)
                    return new Func<int, int>((oldValue) => value + oldValue);
                else
                    return new Func<int, int>((oldValue) => oldValue + oldValue);
            case "-":
                if (value != 0)
                    return numberFirst ? new Func<int, int>((oldValue) => value - oldValue) : new Func<int, int>((oldValue) => oldValue - value);
                else
                    return new Func<int, int>((oldValue) => oldValue - oldValue);
            case "*":
                if (value != 0)
                    return new Func<int, int>((oldValue) => value * oldValue);
                else
                    return new Func<int, int>((oldValue) => oldValue * oldValue);
            default:
                Console.WriteLine("This should not happen");
                break;
        }

        return new Func<int, int>((x) => 0);
    }
}

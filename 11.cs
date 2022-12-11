using System.Linq;

public class Monkey
{
    public int Id;
    public Queue<long> Items = new Queue<long>();
    public Func<long, long> Operation;
    public int TestDivision;
    public bool Test(long x) => x % TestDivision == 0;
    public long ThrowTo(long x) => Test(x) ? SendToIfTrue : SendToIfFalse;
    public int SendToIfTrue;
    public int SendToIfFalse;
    public int InspectionCount;
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
            var items = metadata[1].Replace("  Starting items: ", "").Split(", ").Select(x => long.Parse(x)); // Kolla om vi behöver göra reverse här?
            var operation = ParseOperation(metadata[2].Replace("  Operation: new = ", ""));
            var testDivision = int.Parse(metadata[3].Replace("  Test: divisible by ", ""));
            var sendToIfTrue = int.Parse(metadata[4].Replace("    If true: throw to monkey ", ""));
            var sendToIfFalse = int.Parse(metadata[5].Replace("    If false: throw to monkey ", ""));

            var parsedMonkey = new Monkey
            {
                Id = monkeyId,
                Operation = operation,
                Items = new Queue<long>(items),
                TestDivision = testDivision,
                SendToIfTrue = sendToIfTrue,
                SendToIfFalse = sendToIfFalse,
                InspectionCount = 0
            };
            monkeys.Add(parsedMonkey);
        }

        foreach (var monkey in monkeys)
            Console.WriteLine($"Monkey {monkey.Id}: SendToIfTrue: {monkey.SendToIfTrue}, SendToIfFalse: {monkey.SendToIfFalse}");

        List<long> inspectionCounts = new List<long>();

        int rounds = 10000;
        for (int i = 0; i < rounds; i++)
        {
            foreach (var monkey in monkeys)
            {
                Console.WriteLine($"MONKEY {monkey.Id}");
                while (monkey.Items.TryDequeue(out long itemToThrow))
                {
                    // Increase worryLevel on item
                    //Console.WriteLine($"Before inspect: {itemToThrow}");
                    itemToThrow = monkey.Operation(itemToThrow);
                    //Console.WriteLine($"Inspect: {itemToThrow}");
                    monkey.InspectionCount++;
                    // No worries, decrease worry level
                    //itemToThrow /= 3;
                    //Console.WriteLine($"No worries: {itemToThrow}");

                    //monkey.Test(itemToThrow);

                    //Console.WriteLine($"Will throw to {monkey.ThrowTo(itemToThrow)}");

                    itemToThrow %= 96577;
                    
                    var receiver = monkeys.Single(x => x.Id == monkey.ThrowTo(itemToThrow));
                    //receiver.InspectionCount++;
                    
                    receiver.Items.Enqueue(itemToThrow);
                }
            }

            inspectionCounts = new List<long>();

            Console.WriteLine($"=== After round {i + 1} ===");
            foreach (var monkey in monkeys)
            {
                Console.WriteLine($"Monkey {monkey.Id} inspected items {monkey.InspectionCount} times, items: {string.Join(",", monkey.Items)}");
                inspectionCounts.Add(monkey.InspectionCount);
            }
        }

        //Console.WriteLine($"'{monkeyId}' '{string.Join(", ", items)}' operation on 3 {operation(3)}");

        inspectionCounts.Sort();

        Console.WriteLine(inspectionCounts[inspectionCounts.Count - 1]);

        long esteban = inspectionCounts[inspectionCounts.Count - 1] * inspectionCounts[inspectionCounts.Count - 2];

        Console.WriteLine($"Monkey business: {esteban}");

    }

    private static Func<long, long> ParseOperation(string operation)
    {
        //Console.WriteLine(operation);
        var formula = operation.Split(" ");
        bool numberFirst = true;
        long value = 0;
        if (!long.TryParse(formula[0], out value))
        {
            numberFirst = false;
            _ = long.TryParse(formula[2], out value);
        }

        switch (formula[1])
        {
            // 96 577
            case "+":
                if (value != 0)
                    return new Func<long, long>((oldValue) => value + oldValue);
                else
                    return new Func<long, long>((oldValue) => oldValue + oldValue);
            case "-":
                throw new UnauthorizedAccessException("Disco!");
                if (value != 0)
                    return numberFirst ? new Func<long, long>((oldValue) => value - oldValue) : new Func<long, long>((oldValue) => oldValue - value);
                else
                    return new Func<long, long>((oldValue) => oldValue - oldValue);
            case "*":
                if (value != 0)
                    return new Func<long, long>((oldValue) => value * oldValue);
                else
                    return new Func<long, long>((oldValue) => oldValue * oldValue);
            default:
                Console.WriteLine("This should not happen");
                break;
        }

        return new Func<long, long>((x) => 0);
    }
}

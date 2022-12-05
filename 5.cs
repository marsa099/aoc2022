
using System.Collections;
using System.Text.RegularExpressions;

public class StackObject
{
    public int Id { get; set; }
    public Stack<char> Stack { get; set; } = new Stack<char>();
}

public static class Task5
{
    public static Regex re = new Regex("^(.{3}) (.{3}) (.{3}) (.{3}) (.{3}) (.{3}) (.{3}) (.{3}) (.{3})");
    public static Regex reInstructions = new Regex(@"^move (\d+) from (\d+) to (\d+)$");

    public static string TryParseChar(Match parsedLine, int index)
    {
        var value = parsedLine.Groups[index].Value.Trim('[', ']', ' ');
        return value;
    }
    public static void Execute()
    {
        var input = File.ReadAllLines("inputs/input-5.txt");
        int total = 0;

        var savedLines = new List<Match>();
        List<StackObject> stacks = new List<StackObject>();

        var stacksComplete = false;

        foreach (var line in input)
        {
            if (!stacksComplete)
            {
                var parsedLine = re.Match(line);
                var firstChar = TryParseChar(parsedLine, 1);
                if (firstChar != string.Empty && Char.IsDigit(char.Parse(firstChar)))
                {
                    stacks = createStacks(parsedLine, savedLines);
                    stacksComplete = true;
                    //printStacks(stacks);
                }
                else
                {
                    savedLines.Add(parsedLine);
                }
            }
            else if (line.Trim() == string.Empty) continue;
            else
            {
                var parsedInstructions = reInstructions.Match(line);
                RearrangeStack5b(parsedInstructions, stacks);
            }
        }
        Console.WriteLine("5a: ....");
        printStacks(stacks);

        total = 0;


    }

    private static void RearrangeStack5a(Match parsedInstructions, List<StackObject> stacks)
    {
        var fromStack = int.Parse(parsedInstructions.Groups[2].Value);
        var toStack = int.Parse(parsedInstructions.Groups[3].Value);
        var amount = int.Parse(parsedInstructions.Groups[1].Value);

        var stackObjToMoveFrom = stacks.Single(x => x.Id == fromStack);
        var stackObjToMoveTo = stacks.Single(x => x.Id == toStack);

        for (int i = 0; i < amount; i++)
            stackObjToMoveTo.Stack.Push(stackObjToMoveFrom.Stack.Pop());

    }

    private static void RearrangeStack5b(Match parsedInstructions, List<StackObject> stacks)
    {
        var fromStack = int.Parse(parsedInstructions.Groups[2].Value);
        var toStack = int.Parse(parsedInstructions.Groups[3].Value);
        var amount = int.Parse(parsedInstructions.Groups[1].Value);

        var stackObjToMoveFrom = stacks.Single(x => x.Id == fromStack);
        var stackObjToMoveTo = stacks.Single(x => x.Id == toStack);

        var tempStack = new Stack<char>();

        for (int i = 0; i < amount; i++)
        {
            tempStack.Push(stackObjToMoveFrom.Stack.Pop());
        }
        for (int i = 0; i < amount; i++)
        {
            stackObjToMoveTo.Stack.Push(tempStack.Pop());
        }

    }


    private static void printStacks(List<StackObject> stacks)
    {
        foreach (var stack in stacks)
        {
            Console.WriteLine($"{stack.Id}: {string.Join(", ", stack.Stack)}");
        }
    }

    private static List<StackObject> createStacks(Match parsedLine, List<Match> savedLines)
    {
        var stacks = new List<StackObject>();

        for (int i = 1; i < parsedLine.Groups.Count; i++)
        {
            var myStack = new Stack<char>();
            for (int j = savedLines.Count() - 1; j >= 0; j--)
            {
                var value = TryParseChar(savedLines[j], i);
                if (value == string.Empty) break;
                myStack.Push(char.Parse(value));
            }
            stacks.Add(new StackObject { Id = i, Stack = myStack });
        }
        return stacks;
    }
}

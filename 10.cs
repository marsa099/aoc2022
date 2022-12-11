public static class Task10
{
    public static int cycles = 0;
    public static int register = 1;
    public static int sum = 0;

    public static void Execute()
    {
        var input = File.ReadAllLines("inputs/input-10.txt");

        foreach (var line in input)
        {
            var instruction = line.Split(' ')[0];

            switch (instruction)
            {
                case "noop": // Takes 1 cycle
                    CycleCount(1);
                    break;
                case "addx": // Takes 2 cycles
                    CycleCount(2);
                    int value = int.Parse(line.Split(' ')[1]);
                    register += value;
                    break;
            }
        }
        Console.WriteLine("10a: " + sum);
    }

    private static void CycleCount(int increaseCount)
    {
        for (int i = 0; i < increaseCount; i++)
        {
            ++cycles;
            PrintToScreen(((cycles-1) % 40), register);
        }
    }

    private static void PrintToScreen(int crtPos, int register)
    {       
        char value = '.';
        
        if (crtPos == register -1 || crtPos == register || crtPos == register +1)
            value = '#';

        if(crtPos == 39)
            Console.WriteLine(value);
        else
            Console.Write(value);
    }
}

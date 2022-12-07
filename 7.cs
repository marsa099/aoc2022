public class Node
{
    public Node Parent { get; set; } = null!;
    public bool IsDirectory { get; set; } = false;
    public string Name { get; set; } = string.Empty;
    public int Size { get; set; } = 0;
    public List<Node> Children { get; set; } = null!;
    public bool HasChildren => Children?.Any() ?? false;
    public bool HasParent => Parent == null ? false : true;
}


public static class Task7
{

    public static void Execute()
    {
        var input = File.ReadAllLines("inputs/input-7.txt");

        Node startNode = null!;
        Node currentNode = null!;

        bool lsMode = false;

        foreach (var line in input)
        {
            // Parse instruction
            if (line.StartsWith('$'))
            {
                lsMode = false;
                var command = line.Split(' ')[1];
                switch (command)
                {
                    case "cd":
                        var argument = line.Split(' ')[2];
                        //Console.WriteLine("cd: " + argument);
                        if (argument == "..")
                        {
                            currentNode = currentNode.Parent;
                        }
                        else if (startNode == null && argument == "/")
                        {
                            startNode = new Node { Name = "/", IsDirectory = true };
                            currentNode = startNode;
                        }
                        else
                        {
                            currentNode = currentNode.Children.Single(x => x.Name == argument);
                        }
                        break;
                    case "ls":
                        lsMode = true;
                        break;
                }
            }

            // Parse files
            else if (lsMode)
            {
                //Console.WriteLine("ls: " + line);
                if (currentNode.Children == null) currentNode.Children = new List<Node>();

                if (line.StartsWith("dir"))
                {
                    var dirName = line.Split(' ')[1];
                    currentNode.Children.Add(new Node { Name = dirName, Size = 0, IsDirectory = true, Parent = currentNode });
                }
                else
                {
                    var fileSize = int.Parse(line.Split(' ')[0]);
                    var fileName = line.Split(' ')[1];
                    currentNode.Children.Add(new Node { Name = fileName, Size = fileSize, IsDirectory = false, Parent = currentNode });

                    SetParentNodeSizes(currentNode, fileSize);
                }
            }
        }

        var result = GetDirectories(startNode).Sum(x => x.Size);
        Console.WriteLine("7a: " + result);

        int spaceNeeded = 30000000 - (70000000 - startNode.Size);
        var dirToDelete = GetDirectories7b(startNode).Where(x => x.Size > spaceNeeded).OrderBy(x => x.Size).First();
        Console.WriteLine($"7b: {dirToDelete.Size}");
    }


    private static IEnumerable<Node> GetDirectories7b(Node currentNode)
    {
        if (currentNode.HasChildren)
        {
            foreach (var dir in currentNode.Children.Where(x => x.IsDirectory))
            {
                yield return dir;
                foreach (var subdir in GetDirectories7b(dir))
                    yield return subdir;
            }
        }
    }

    private static IEnumerable<Node> GetDirectories(Node currentNode)
    {
        if (currentNode.HasChildren)
        {
            foreach (var dir in currentNode.Children.Where(x => x.IsDirectory))
            {
                if (dir.Size <= 100000)
                {
                    yield return dir;
                }
                foreach (var subdir in GetDirectories(dir))
                    yield return subdir;
            }
        }
    }

    private static void SetParentNodeSizes(Node currentNode, int fileSize)
    {
        currentNode.Size += fileSize;

        if (currentNode.HasParent)
            SetParentNodeSizes(currentNode.Parent, fileSize);
    }
}

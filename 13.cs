using System.Linq;

namespace Task13;

public class PacketValue
{
    public PacketValue(PacketType type, string value)
    {
        Type = type;
        Value = value;
    }
    public PacketType Type { get; set; }
    public string Value { get; set; } = string.Empty;
}

public enum PacketType
{
    Int,
    List,
}
public static class Task13
{
    public static void Execute()
    {
        var input = File.ReadAllLines("inputs/input-13-testdata.txt");
        var packets = input.Chunk(3).Select(chunk => ParsePacketPair(chunk.Take(2).ToList()));

    }

    private static IEnumerable<(string[] item1, string[] item2)> ParsePacketPair(List<string> chunk)
    {
        var first = UnzipPacket(chunk[0]);
        var s = first.Split(",[")
        var second = UnzipPacket(chunk[1]);

        return (first, second);
    }

    // [20:29] Erik Dellby split(new[] {​​ ",[", "],", ",1". ",2"... )



    // Ska returnera lite mojänger
    // [1,[2,[3,[4,[5,6,7]]]],8,9]
    // Ska returnera
    // PacketType.Int
    // PacketType.List
    // PacketType.Int
    // PacketType.Int
    private static PacketValue UnzipPacket(string packet)
    {
        if(packet[0] == '[')
            return new PacketValue(PacketType.List, packet.Remove(0, 1).Remove(packet.Length - 1, 1));
        return new PacketValue(PacketType.Int, packet);
    }
}
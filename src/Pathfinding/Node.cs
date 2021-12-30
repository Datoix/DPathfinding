
namespace Pathfinding;
/// <summary>
/// Node class
/// </summary>
public class Node {
    public Coord Coord { get; }
    public NodeType Type { get; set; }
    public bool IsDiscovered { get; set; }
    public Node? Parent { get; set; }

    public float GScore { get; set; } = float.MaxValue;
    public float HScore { get; set; }
    public float FScore => GScore + HScore;

    public Node(Coord coord, NodeType type) {
        Coord = coord;
        Type = type;
    }
}

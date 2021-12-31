
namespace DPathfinding.Example;
public class Cell {
    private static readonly ConsoleColor DefaultColor = ConsoleColor.Gray;

    public char Symbol { get; set; }

    private ConsoleColor? _color;
    public ConsoleColor Color {
        get => _color ?? DefaultColor;
        set => _color = value;
    }

    public Cell(NodeType type) {
        Symbol = type switch {
            NodeType.Empty => ' ',
            NodeType.Wall => '#',
            NodeType.Start => 'S',
            NodeType.End => 'E',
            _ => '\0'
        };
    }

    public void Print() {
        Console.ForegroundColor = Color;
        Console.Write(Symbol);
        Console.ForegroundColor = DefaultColor;
    }
}

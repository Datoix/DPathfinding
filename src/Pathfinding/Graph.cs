
using System.Collections;
using System.Numerics;

namespace Pathfinding;
/// <summary>
/// Graph class
/// </summary>
public class Graph : IEnumerable<Node> {
    private readonly Node[,] _grid;
    private static readonly Random _rnd = new();

    public Node Start { get; private set; }
    public Node End { get; private set; }

    public int Rows => _grid.GetLength(0);
    public int Columns => _grid.GetLength(1);

    private Graph(Node[,] grid, Node start, Node end) {
        _grid = grid;
        Start = start;
        End = end;
    }

    /// <summary>
    /// Creates a new instance of <see cref="Graph"/> class, automaticly finding star and end node
    /// </summary>
    /// <param name="grid">The array of nodes used to initialize grid</param>
    /// <returns>An instance of <see cref="Graph"/></returns>
    public static Graph Create(Node[,] grid) {
        Node? start = null;
        Node? end = null;
        // determine start and end
        foreach (var node in grid) {
            if (node.Type == NodeType.Start) {
                start = node;
            } else if (node.Type == NodeType.End) {
                end = node;
            }
        }

        ArgumentNullException.ThrowIfNull(start, "No start node specified");
        ArgumentNullException.ThrowIfNull(end, "No end node specified");

        return new Graph(grid, start, end);
    }

    /// <summary>
    /// Creates a new instance of <see cref="Graph"/> class
    /// </summary>
    /// <param name="grid">The array of nodes used to initialize grid</param>
    /// <param name="start">The start node</param>
    /// <param name="end">The end node</param>
    /// <returns>An instance of <see cref="Graph"/></returns>
    public static Graph Create(Node[,] grid, Node start, Node end) {
        ArgumentNullException.ThrowIfNull(start, "No start node specified");
        ArgumentNullException.ThrowIfNull(end, "No end node specified");

        return new Graph(grid, start, end);
    }

    /// <summary>
    /// Creates a new instance of <see cref="Graph"/> class, automaticly finding star and end node
    /// </summary>
    /// <param name="grid">The array of integers used to initialize grid</param>
    /// <returns>An instance of <see cref="Graph"/></returns>
    /// <exception cref="ArgumentOutOfRangeException">array element not compatible with enum<exception>
    public static Graph Create(int[,] grid) {
        var (rows, columns) = (grid.GetLength(0), grid.GetLength(1));
        var nodes = new Node[rows, columns];

        Node? start = null;
        Node? end = null;

        for (int i = 0; i < rows; ++i) {
            for (int j = 0; j < columns; ++j) {
                // for each cell create node with specific type
                var nodeType = grid[i, j] switch {
                    0 => NodeType.Empty,
                    1 => NodeType.Wall,
                    2 => NodeType.Start,
                    3 => NodeType.End,
                    _ => throw new ArgumentOutOfRangeException("Invalid value in input")
                };

                var node = new Node(new(i, j), nodeType);

                if (node.Type == NodeType.Start) {
                    start = node;
                } else if (node.Type == NodeType.End) {
                    end = node;
                }

                nodes[i, j] = node;
            }
        }


        ArgumentNullException.ThrowIfNull(start, "No start node specified");
        ArgumentNullException.ThrowIfNull(end, "No end node specified");

        return Create(nodes, start, end);
    }

    /// <summary>
    /// Creates a new instance of <see cref="Graph"/> class, automaticly finding star and end node
    /// </summary>
    /// <param name="content">The string used to initialize grid</param>
    /// <returns>An instance of <see cref="Graph"/></returns>
    public static Graph Create(string content) {
        var lines = content.Replace("\r", "").Trim().Split("\n");
        var (rows, columns) = (lines.Length, lines[0].Length);
        var grid = new int[rows, columns];

        for (int i = 0; i < rows; ++i) {
            for (int j = 0; j < columns; ++j) {
                grid[i, j] = Convert.ToInt32(lines[i][j].ToString());
            }
        }

        return Create(grid);
    }

    /// <summary>
    /// Gets node in grid at specific position
    /// </summary>
    /// <param name="coord">Node position</param>
    /// <returns>Node if found, <see langword="null"/> if not</returns>
    public Node? GetNode(Coord coord)
        => Coord.Get(_grid, coord);

    /// <summary>
    /// Gets vector distance between origin and destination
    /// </summary>
    /// <param name="origin">The first node</param>
    /// <param name="destination">The second node</param>
    /// <returns>The distance between nodes</returns>
    public float GetDistance(Node origin, Node destination)
        => Vector2.Distance(
            new(origin.Coord.Row, origin.Coord.Column),
            new(destination.Coord.Row, destination.Coord.Column)
        );

    /// <summary>
    /// Gets all neighbors of node
    /// </summary>
    /// <param name="node">Node to find neighbors</param>
    /// <param name="node">Distance between node and neighbors</param>
    /// <returns>A <see cref="IEnumerable{T}"/> of neighbors</returns>
    public IEnumerable<Node> GetNeighbors(Node node, int d = 1)
        => new List<Node?>() {
            GetNode(new Coord(node.Coord.Row - d, node.Coord.Column)),
            GetNode(new Coord(node.Coord.Row + d, node.Coord.Column)),
            GetNode(new Coord(node.Coord.Row, node.Coord.Column + d)),
            GetNode(new Coord(node.Coord.Row, node.Coord.Column - d)),
            GetNode(new Coord(node.Coord.Row + d, node.Coord.Column + d)),
            GetNode(new Coord(node.Coord.Row + d, node.Coord.Column - d)),
            GetNode(new Coord(node.Coord.Row - d, node.Coord.Column - d)),
            GetNode(new Coord(node.Coord.Row - d, node.Coord.Column + d)),
        }.Where(n => n != null).Select(n => n!);

    public IEnumerator<Node> GetEnumerator() {
        foreach (var node in _grid) {
            yield return node;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}
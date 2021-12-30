
namespace Pathfinding.Algorithms;
/// <summary>
/// A* algorithm implementation
/// <see href="https://en.wikipedia.org/wiki/A*_search_algorithm">
/// [more]
/// </see>
/// </summary>
public class AStar : AlgorithmBase {
    public override string Name => "A* (AStar)";

    public AStar(Graph graph) : base(graph) { }

    public override IEnumerable<Coord>? GetPath() {
        // create an empty open set
        var open = new HashSet<Node>();
        open.Add(Graph.Start);

        // set start gScore and hScore

        Graph.Start.GScore = 0;
        Graph.Start.HScore = GetManhattanDistance(Graph.Start);

        while (open.Count > 0) {
            // node with smallest FScore (fScore = gScore + hScore)
            var current = open.MinBy(n => n.FScore)!;

            if (current == Graph.End) break; // if target reached, break the loop

            open.Remove(current);

            foreach (var neighbor in Graph.GetNeighbors(current)) {
                if (neighbor.Type != NodeType.Wall) {
                    // gScore = parent gScore + distance between parent and neighbor
                    var gScore = current.GScore + Graph.GetDistance(current, neighbor);

                    if (gScore < neighbor.GScore) {
                        // set neighbor data
                        neighbor.Parent = current;
                        neighbor.GScore = gScore;
                        neighbor.HScore = GetManhattanDistance(neighbor);

                        if (!open.Contains(neighbor)) {
                            open.Add(neighbor);
                        }
                    }
                }
            }
        }

        // resolve path
        // NOTE: can return null
        return ResolvePath();
    }

    /// <summary>
    /// Estimates cost of cheapest path from origin to target
    /// </summary>
    /// <param name="origin">Node to check</param>
    /// <returns>Distance between nodes</returns>
    private int GetManhattanDistance(Node origin)
        => Math.Abs(origin.Coord.Row - Graph.End.Coord.Row)
        + Math.Abs(origin.Coord.Column - Graph.End.Coord.Column);
}

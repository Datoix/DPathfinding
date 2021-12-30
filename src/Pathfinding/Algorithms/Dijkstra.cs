
namespace Pathfinding.Algorithms;
/// <summary>
/// Dijkstra's algorithm implementation
/// <see href="https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm">
/// [more]
/// </see>
/// </summary>
public class Dijkstra : AlgorithmBase {
    public override string Name => "Dijkstra";

    public Dijkstra(Graph graph) : base(graph) { }

    public override IEnumerable<Coord>? GetPath() {
        // create an empty open set
        var s = new HashSet<Node>();

        foreach (var node in Graph) {
            s.Add(node);
        }

        Graph.Start.GScore = 0; // set default to 0

        while (s.Count > 0) {
            var current = s.MinBy(node => node.GScore)!; // node with smallest distance value

            s.Remove(current);

            if (current == Graph.End) break; // if target reached, break the loop

            foreach (var neighbor in Graph.GetNeighbors(current)) {
                if (s.Contains(neighbor) && neighbor.Type != NodeType.Wall) {
                    // alt = parent gScore + distance between parent and neighbor
                    var alt = current.GScore + Graph.GetDistance(current, neighbor);

                    if (alt < neighbor.GScore) {
                        neighbor.GScore = alt;
                        // set neighbor parent
                        neighbor.Parent = current;
                    }
                }
            }
        }

        // resolve path
        // NOTE: can return null
        return ResolvePath();
    }
}

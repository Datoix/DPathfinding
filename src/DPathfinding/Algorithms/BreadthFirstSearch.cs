
namespace DPathfinding.Algorithms;
/// <summary>
/// BFS algorithm implementation
/// <see href="https://en.wikipedia.org/wiki/Breadth-first_search">
/// [more]
/// </see>
/// </summary>
public class BreadthFirstSearch : AlgorithmBase {
    public override string Name => "BFS";

    public BreadthFirstSearch(Graph graph) : base(graph) { }

    public override IEnumerable<Coord>? GetPath() {
        var current = Graph.Start;
        // create an empty queue
        var q = new Queue<Node>();
        current.IsDiscovered = true;
        q.Enqueue(current);

        while (q.Count > 0) {
            current = q.Dequeue();
            if (current.Type == NodeType.End) break; // if target reached, break the loop

            foreach (var neighbor in Graph.GetNeighbors(current)) {
                if (!neighbor.IsDiscovered && neighbor.Type != NodeType.Wall) {
                    // set neighbor data
                    neighbor.Parent = current;
                    neighbor.IsDiscovered = true;

                    q.Enqueue(neighbor); // add neighbor to queue
                }
            }
        }

        // resolve path
        // NOTE: can return null
        return ResolvePath();
    }
}

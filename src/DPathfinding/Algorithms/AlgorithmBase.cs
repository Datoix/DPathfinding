
namespace DPathfinding.Algorithms;
/// <summary>
/// Base class for all algorithms
/// </summary>
public abstract class AlgorithmBase {
    protected Graph Graph { get; }

    public abstract string Name { get; }

    public AlgorithmBase(Graph graph) {
        Graph = graph;
    }

    /// <summary>
    /// Finds path in graph, unique for every algorithm
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> of coordinates or <see langword="null"/> if no path</returns>
    public abstract IEnumerable<Coord>? GetPath();

    /// <summary>
    /// Resolves path from last node
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> of coordinates or <see langword="null"/> if no path</returns>
    protected IEnumerable<Coord>? ResolvePath() {
        var current = Graph.End;

        if (current.Parent != null || current == Graph.Start) {
            var path = new List<Coord>() { current.Coord };

            while (current.Parent != null) {
                current = current.Parent;
                path.Add(current.Coord);
            }

            return path;
        }

        return null;
    }
}

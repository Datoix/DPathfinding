
using Pathfinding.Algorithms;

namespace Pathfinding.Example;
public class Grid {
    private readonly Graph _graph;
    private readonly Cell[,] _grid;

    public Grid(string path) {
        _graph = Graph.Create(File.ReadAllText("example.txt")); // initializing graph
        
        var (rows, columns) = (_graph.Rows, _graph.Columns);
        _grid = new Cell[rows, columns];

        // initializing cells (cell has the same type as node)
        for (int i = 0; i < rows; ++i) {
            for (int j = 0; j < columns; ++j) {
                _grid[i, j] = new Cell(_graph.GetNode(new(i, j))!.Type);
            }
        }
    }

    public void FindPath() {
        var aStar = new AStar(_graph);
        var path = aStar.GetPath();

        // set color to blue (later visible in console)
        _grid[_graph.Start.Coord.Row, _graph.Start.Coord.Column].Color = ConsoleColor.Blue;
        _grid[_graph.End.Coord.Row, _graph.End.Coord.Column].Color = ConsoleColor.Blue;

        if (path != null) {
            // change cells colors and symbols
            // excluding 1st (start) and last (end)
            foreach (var coord in path.Take(path.Count() - 1).Skip(1)) {
                var cell = _grid[coord.Row, coord.Column];
                cell.Color = ConsoleColor.Red;
                cell.Symbol = 'o';
            }
        }
    }

    public void Print() {
        for (int i = 0; i < _graph.Rows; ++i) {
            for (int j = 0; j < _graph.Columns; ++j) {
                _grid[i, j].Print(); // print every cell
            }
            Console.WriteLine();
        }
    }
}


namespace DPathfinding.Example;
public class Program {
    public static void Main(string[] args) {
        var grid = new Grid("example.txt");
        grid.FindPath();
        grid.Print();
    }
}
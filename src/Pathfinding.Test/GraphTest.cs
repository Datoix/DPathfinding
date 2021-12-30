
using NUnit.Framework;

namespace Pathfinding.Test;
public class GraphTest {
    private Graph _graph;

    [SetUp]
    public void Setup() {
        var text = File.ReadAllText("./Resources/small.txt");
        _graph = Graph.Create(File.ReadAllText("./Resources/sample.txt"));
    }

    [Test]
    public void FileLoading() {
        var expected = new int[,] {
            { 0, 3, 0, 0 },
            { 0, 1, 2, 0 }
        };

        for (int i = 0; i < 2; ++i) {
            for (int j = 0; j < 4; ++j) {
                var node = _graph.GetNode(new(i, j));
                var type = node?.Type;
                Assert.AreEqual((NodeType)expected[i, j], type);
            }
        }
    }
}

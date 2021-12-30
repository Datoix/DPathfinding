
using NUnit.Framework;
using Pathfinding.Algorithms;

namespace Pathfinding.Test.Algorithms;
public class DijkstraTest {
    private Graph _small;
    private Graph _large;
    private Graph _noPath;

    [SetUp]
    public void Setup() {
        _small = Graph.Create(File.ReadAllText("./Resources/small.txt"));
        _large = Graph.Create(File.ReadAllText("./Resources/large.txt"));
        _noPath = Graph.Create(File.ReadAllText("./Resources/nopath.txt"));
    }

    [Test]
    public void Small() {
        var dijkstra = new Dijkstra(_small);
        var path = dijkstra.GetPath();

        Assert.AreEqual(7, path?.Count());
    }

    [Test]
    public void Large() {
        var dijkstra = new Dijkstra(_large);
        var path = dijkstra.GetPath();

        Assert.IsTrue(path?.Count() > 0);
    }

    [Test]
    public void NoPath() {
        var dijkstra = new Dijkstra(_noPath);
        var path = dijkstra.GetPath();

        Assert.IsNull(path);
    }
}


using NUnit.Framework;
using DPathfinding.Algorithms;

namespace DPathfinding.Test.Algorithms;
public class BreadthFirstSearchTest {
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
        var bfs = new BreadthFirstSearch(_small);
        var path = bfs.GetPath();

        Assert.AreEqual(7, path?.Count());
    }

    [Test]
    public void Large() {
        var dfs = new BreadthFirstSearch(_large);
        var path = dfs.GetPath();

        Assert.IsTrue(path?.Count() > 0);
    }

    [Test]
    public void NoPath() {
        var bfs = new BreadthFirstSearch(_noPath);
        var path = bfs.GetPath();

        Assert.IsNull(path);
    }
}

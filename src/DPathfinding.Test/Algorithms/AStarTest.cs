
using NUnit.Framework;
using DPathfinding.Algorithms;

namespace DPathfinding.Test.Algorithms;
public class AStarTest {
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
        var aStar = new AStar(_small);
        var path = aStar.GetPath();

        Assert.AreEqual(7, path?.Count());
    }

    [Test]
    public void Large() {
        var aStar = new AStar(_large);
        var path = aStar.GetPath();

        Assert.IsTrue(path?.Count() > 0);
    }

    [Test]
    public void NoPath() {
        var aStar = new AStar(_noPath);
        var path = aStar.GetPath();

        Assert.IsNull(path);
    }
}

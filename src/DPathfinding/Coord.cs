
namespace DPathfinding;
/// <summary>
/// Coord struct representing position in grid
/// </summary>
public struct Coord {
    public int Row { get; }
    public int Column { get; }

    public Coord(int x, int y) {
        Row = x;
        Column = y;
    }

    /// <summary>
    /// Determines is coord valid
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="arr">The array to check position</param>
    /// <param name="coord">The position to check</param>
    /// <returns><see langword="true"/> if valid, <see langword="false"/> if not</returns>
    public static bool IsValid<T>(T[,] arr, Coord coord)
        => coord.Row >= 0 && coord.Row < arr.GetLength(0)
        && coord.Column >= 0 && coord.Column < arr.GetLength(1);

    /// <summary>
    /// Gets element in array at specific position
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="arr">The array to get element</param>
    /// <param name="coord">The element position</param>
    /// <returns>Element if valid, <see langword="null"/> if not</returns>
    public static T? Get<T>(T[,] arr, Coord coord)
        => IsValid(arr, coord) ? arr[coord.Row, coord.Column] : default(T);
}

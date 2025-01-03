using KTANE_helper.Solvers;
using Shouldly;

namespace SolverTests;

public class MazeSolverTests
{
    [Theory, MemberData(nameof(GetAllMazes))]
    public void NeighboursAreSymmetrical(Maze maze)
    {
        for (int i = 0; i < Maze.Size; ++i)
        {
            foreach (var neighbour in maze.GetNeighbours(i))
            {
                maze.GetNeighbours(neighbour).ShouldContain(i);
            }
        }
    }

    public static IEnumerable<object[]> GetAllMazes()
    {
        return Maze.AllMazes.Select(m => new object[] { m });
    }
}
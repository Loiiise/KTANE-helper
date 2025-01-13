using KTANE_helper.Logic.IO;
using KTANE_helper.Logic.IO.Tests;
using Shouldly;

namespace KTANE_helper.Logic.Solvers.Tests;
public class WireSolverTests
{
    /// <summary>
    /// This is a dummy test. No real tests should be implemented like this.
    /// We should first make a testing attribute that will handle the first section dynamically.
    /// Furthermore we should work with `Response`s and `Answer`s before actually testing it so we don't have to do all the magic with strings.
    /// Other than that some things here should be generalized before mass implementation.
    /// </summary>
    [Fact]
    public void DummyTestToShowHowIIOHandlerWorks()
    {
        // Initialize objects
        MockIOHandler ioHandlerMock = new();
        BombKnowledge bombKnowledge = new(ioHandlerMock);
        var solver = WireSolver.GetInstance(ioHandlerMock);

        // Prepare input
        // Three wire, no red ones will always cut the second one
        ioHandlerMock.EnqueueInputLine("BBB");

        // Solve the puzzle
        solver.Solve(bombKnowledge);

        // Assert correct output
        var answer = ioHandlerMock.GetAnswer() as WireAnswer;
        answer.ShouldBeOfType<WireAnswer>();
        answer.Value.ShouldBe(2);
    }
}

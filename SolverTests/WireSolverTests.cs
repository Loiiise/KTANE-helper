using KTANE_helper;
using KTANE_helper.Solvers;
using Shouldly;

namespace SolverTests;
public class WireSolverTests
{
    /// <summary>
    /// This is a dummy test. No real tests should be implemented like this.
    /// We should first make a testing attribute that will handle the first section dynamically.
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

        // Assert correct output
        ioHandlerMock.ReadOutputLine().ShouldBe("Cut the second wire");
    }
}

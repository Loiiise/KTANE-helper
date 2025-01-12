using KTANE_helper;
using KTANE_helper.IOHandler;
using KTANE_helper.Solvers;

namespace SolverTests;
public class WireSolverTests
{
    [Fact]
    public void DummyTestToShowHowIIOHandlerWorks()
    {
        IIOHandler ioHandlerMock = new IIOHandler();
        var bombKnowledge = new BombKnowledge(ioHandlerMock);
        WireSolver.GetInstance(ioHandlerMock).Solve(bombKnowledge);

        throw new NotImplementedException();
    }
}

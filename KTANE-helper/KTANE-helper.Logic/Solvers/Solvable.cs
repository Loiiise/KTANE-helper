#nullable disable
namespace KTANE_helper.Logic.Solvers;

public abstract class Solvable<Solver> where Solver : new()
{
    protected static Solver _instance;
    protected static IIOHandler _ioHandler;

    public static Solver GetInstance(IIOHandler ioHandler)
    {
        _ioHandler ??= ioHandler;
        _instance ??= new Solver();

        return _instance;
    }

    public abstract void Solve(BombKnowledge bk);
}

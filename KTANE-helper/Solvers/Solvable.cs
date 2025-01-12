using KTANE_helper.IOHandler;

namespace KTANE_helper.Solvers
{
    internal abstract class Solvable<Solver> where Solver : new()
    {
        protected static Solver instance;
        protected static IIOHandler _ioHandler;

        internal static Solver GetInstance(IIOHandler ioHandler)
        {
            _ioHandler = ioHandler;

            if (instance is null) instance = new Solver();

            return instance;
        }

        internal abstract void Solve(BombKnowledge bk);
    }
}
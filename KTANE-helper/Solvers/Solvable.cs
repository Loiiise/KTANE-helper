using KTANE_helper.IOHandler;

namespace KTANE_helper.Solvers
{
    public abstract class Solvable<Solver> where Solver : new()
    {
        protected static Solver instance;
        protected static IIOHandler _ioHandler;

        public static Solver GetInstance(IIOHandler ioHandler)
        {
            _ioHandler = ioHandler;

            if (instance is null) instance = new Solver();

            return instance;
        }

        public abstract void Solve(BombKnowledge bk);
    }
}
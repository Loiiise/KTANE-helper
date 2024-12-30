namespace KTANE_helper.Solvers
{
    internal abstract class Solvable<Solver> where Solver : new()
    {
        protected static Solver instance;
        internal static Solver GetInstance()
        {
            if (instance is null) instance = new Solver();

            return instance;
        }

        internal abstract void Solve(BombKnowledge bk);
    }
}

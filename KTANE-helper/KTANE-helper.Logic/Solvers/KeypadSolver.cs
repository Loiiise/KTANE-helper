using KTANE_helper.Logic.IO;

namespace KTANE_helper.Logic.Solvers;

public class KeypadSolver : Solvable<KeypadSolver>
{
    public override void Solve(BombKnowledge bk)
    {
        _ioHandler.Answer(new KeypadAnswer { Value = GetColumn() });
    }

    private static IOTypes.KeypadSymbol[] GetColumn()
    {
        var stringKeys = IOTypes.AllKeypadSymbols.Select(IOTypes.GetSymbolString);
        var keyString = _ioHandler.Query("What symbol do you see?", stringKeys);
        var candidates = symbolToColumn[IOTypes.GetSymbolFromString(keyString)];

        if (candidates.Length == 1) return columnToSymbol[candidates.First()];

        int a = candidates.First();
        int b = candidates.Last();
        return IsColumn(a, b) ? columnToSymbol[a] : columnToSymbol[b];
    }

    private static bool IsColumn(int a, int b)
    {
        if (a == b) return true;

        var setA = new HashSet<IOTypes.KeypadSymbol>(columnToSymbol[a]);
        var uniqueA = setA.Except(columnToSymbol[b]);

        return SeeAny(uniqueA);
    }

    private static bool SeeAny(IEnumerable<IOTypes.KeypadSymbol> symbols) => _ioHandler.Ask($"Do you see any of {DisplaySymbols(symbols)}\n");

    public static string DisplaySymbols(IEnumerable<IOTypes.KeypadSymbol> symbols) => string.Join("", symbols.Select(s => "\n    " + s.GetSymbolString()));

    private static readonly Dictionary<IOTypes.KeypadSymbol, int[]> symbolToColumn = new()
    {
        { IOTypes.KeypadSymbol.O, [ 0, 1 ] },
        { IOTypes.KeypadSymbol.A, [ 0 ] },
        { IOTypes.KeypadSymbol.Lambda, [ 0, 2 ] },
        { IOTypes.KeypadSymbol.N, [ 0 ] },
        { IOTypes.KeypadSymbol.Spin, [ 0, 3 ] },
        { IOTypes.KeypadSymbol.H, [ 0, 1 ] },
        { IOTypes.KeypadSymbol.CMirrored, [ 0, 1 ] },
        { IOTypes.KeypadSymbol.E, [ 1, 5 ] },
        { IOTypes.KeypadSymbol.Krul, [ 1, 2 ] },
        { IOTypes.KeypadSymbol.Ster, [ 1, 2 ] },
        { IOTypes.KeypadSymbol.Questionmark, [ 1, 3 ] },
        { IOTypes.KeypadSymbol.Copyright, [ 2 ] },
        { IOTypes.KeypadSymbol.W, [ 2 ] },
        { IOTypes.KeypadSymbol.KK, [ 2, 3 ] },
        { IOTypes.KeypadSymbol.R, [ 2 ] },
        { IOTypes.KeypadSymbol.Zes, [ 3, 5 ] },
        { IOTypes.KeypadSymbol.P, [ 3, 4 ] },
        { IOTypes.KeypadSymbol.B, [ 3, 4 ] },
        { IOTypes.KeypadSymbol.Smiley, [ 3, 4 ] },
        { IOTypes.KeypadSymbol.Psi, [ 4, 5 ] },
        { IOTypes.KeypadSymbol.C, [ 4 ] },
        { IOTypes.KeypadSymbol.Slang, [ 4 ] },
        { IOTypes.KeypadSymbol.SterGevuld, [ 4 ] },
        { IOTypes.KeypadSymbol.Equals, [ 5 ] },
        { IOTypes.KeypadSymbol.Ae, [ 5 ] },
        { IOTypes.KeypadSymbol.GroteN, [ 5 ] },
        { IOTypes.KeypadSymbol.Omega, [ 5 ] },
    };

    private static readonly Dictionary<int, IOTypes.KeypadSymbol[]> columnToSymbol = new()
    {
        { 0, [ IOTypes.KeypadSymbol.O, IOTypes.KeypadSymbol.A, IOTypes.KeypadSymbol.Lambda, IOTypes.KeypadSymbol.N, IOTypes.KeypadSymbol.Spin, IOTypes.KeypadSymbol.H, IOTypes.KeypadSymbol.CMirrored ] },
        { 1, [ IOTypes.KeypadSymbol.E, IOTypes.KeypadSymbol.O, IOTypes.KeypadSymbol.CMirrored, IOTypes.KeypadSymbol.Krul, IOTypes.KeypadSymbol.Ster, IOTypes.KeypadSymbol.H, IOTypes.KeypadSymbol.Questionmark ] },
        { 2, [ IOTypes.KeypadSymbol.Copyright, IOTypes.KeypadSymbol.W, IOTypes.KeypadSymbol.Krul, IOTypes.KeypadSymbol.KK, IOTypes.KeypadSymbol.R, IOTypes.KeypadSymbol.Lambda, IOTypes.KeypadSymbol.Ster ] },
        { 3, [ IOTypes.KeypadSymbol.Zes, IOTypes.KeypadSymbol.P, IOTypes.KeypadSymbol.B, IOTypes.KeypadSymbol.Spin, IOTypes.KeypadSymbol.KK, IOTypes.KeypadSymbol.Questionmark, IOTypes.KeypadSymbol.Smiley ] },
        { 4, [ IOTypes.KeypadSymbol.Psi, IOTypes.KeypadSymbol.Smiley, IOTypes.KeypadSymbol.B, IOTypes.KeypadSymbol.C, IOTypes.KeypadSymbol.P, IOTypes.KeypadSymbol.Slang, IOTypes.KeypadSymbol.SterGevuld ] },
        { 5, [ IOTypes.KeypadSymbol.Zes, IOTypes.KeypadSymbol.E, IOTypes.KeypadSymbol.Equals, IOTypes.KeypadSymbol.Ae, IOTypes.KeypadSymbol.Psi, IOTypes.KeypadSymbol.GroteN, IOTypes.KeypadSymbol.Omega ] },
    };
}

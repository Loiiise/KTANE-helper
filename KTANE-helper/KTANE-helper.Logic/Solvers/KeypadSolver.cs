using KTANE_helper.Logic.IO;

namespace KTANE_helper.Logic.Solvers;

public class KeypadSolver : Solvable<KeypadSolver>
{
    public override void Solve(BombKnowledge bk)
    {
        _ioHandler.Answer(new KeypadAnswer { Value = GetColumn() });
    }

    private static KeypadSymbol[] GetColumn()
    {
        var stringKeys = allSymbols.Select(GetSymbolString);
        var keyString = _ioHandler.Query("What symbol do you see?", stringKeys);
        var candidates = symbolToColumn[GetSymbolFromString(keyString)];

        if (candidates.Count() == 1) return columnToSymbol[candidates.First()];

        int a = candidates.First();
        int b = candidates.Last();
        return IsColumn(a, b) ? columnToSymbol[a] : columnToSymbol[b];
    }

    private static bool IsColumn(int a, int b)
    {
        if (a == b) return true;

        var setA = new HashSet<KeypadSymbol>(columnToSymbol[a]);
        var uniqueA = setA.Except(columnToSymbol[b]);

        return SeeAny(uniqueA);
    }

    private static bool SeeAny(IEnumerable<KeypadSymbol> symbols) => _ioHandler.Ask($"Do you see any of {DisplaySymbols(symbols)}\n");

    public static string DisplaySymbols(IEnumerable<KeypadSymbol> symbols) => string.Join("", symbols.Select(s => "\n    " + GetSymbolString(s)));

    private static IEnumerable<KeypadSymbol> allSymbols = Enum.GetValues(typeof(KeypadSymbol)).Cast<KeypadSymbol>();

    private static string GetSymbolString(KeypadSymbol symbol) => symbol switch
    {
        KeypadSymbol.O => "o",
        KeypadSymbol.A => "a",
        KeypadSymbol.Lambda => "lambda",
        KeypadSymbol.N => "n",
        KeypadSymbol.Spin => "spin",
        KeypadSymbol.H => "h",
        KeypadSymbol.CMirrored => "cc",
        KeypadSymbol.E => "\"e",
        KeypadSymbol.Krul => "krul",
        KeypadSymbol.Ster => "ster",
        KeypadSymbol.Questionmark => "?",
        KeypadSymbol.Copyright => "cr",
        KeypadSymbol.W => "w",
        KeypadSymbol.KK => "kk",
        KeypadSymbol.R => "r",
        KeypadSymbol.Zes => "6",
        KeypadSymbol.P => "p",
        KeypadSymbol.B => "b",
        KeypadSymbol.Smiley => ":)",
        KeypadSymbol.Psi => "psi",
        KeypadSymbol.C => "c",
        KeypadSymbol.Slang => "slang",
        KeypadSymbol.SterGevuld => "gevulde ster",
        KeypadSymbol.Equals => "=",
        KeypadSymbol.Ae => "ae",
        KeypadSymbol.GroteN => "grote n",
        KeypadSymbol.Omega => "omega",
        _ => throw new ArgumentOutOfRangeException(nameof(KeypadSymbol)),
    };
    private static KeypadSymbol GetSymbolFromString(string stringSymbol) => allSymbols.First(k => GetSymbolString(k) == stringSymbol);

    private static readonly Dictionary<KeypadSymbol, int[]> symbolToColumn = new()
    {
        { KeypadSymbol.O, [ 0, 1 ] },
        { KeypadSymbol.A, [ 0 ] },
        { KeypadSymbol.Lambda, [ 0, 2 ] },
        { KeypadSymbol.N, [ 0 ] },
        { KeypadSymbol.Spin, [ 0, 3 ] },
        { KeypadSymbol.H, [ 0, 1 ] },
        { KeypadSymbol.CMirrored, [ 0, 1 ] },
        { KeypadSymbol.E, [ 1, 5 ] },
        { KeypadSymbol.Krul, [ 1, 2 ] },
        { KeypadSymbol.Ster, [ 1, 2 ] },
        { KeypadSymbol.Questionmark, [ 1, 3 ] },
        { KeypadSymbol.Copyright, [ 2 ] },
        { KeypadSymbol.W, [ 2 ] },
        { KeypadSymbol.KK, [ 2, 3 ] },
        { KeypadSymbol.R, [ 2 ] },
        { KeypadSymbol.Zes, [ 3, 5 ] },
        { KeypadSymbol.P, [ 3, 4 ] },
        { KeypadSymbol.B, [ 3, 4 ] },
        { KeypadSymbol.Smiley, [ 3, 4 ] },
        { KeypadSymbol.Psi, [ 4, 5 ] },
        { KeypadSymbol.C, [ 4 ] },
        { KeypadSymbol.Slang, [ 4 ] },
        { KeypadSymbol.SterGevuld, [ 4 ] },
        { KeypadSymbol.Equals, [ 5 ] },
        { KeypadSymbol.Ae, [ 5 ] },
        { KeypadSymbol.GroteN, [ 5 ] },
        { KeypadSymbol.Omega, [ 5 ] },
    };

    private static readonly Dictionary<int, KeypadSymbol[]> columnToSymbol = new()
    {
        { 0, [ KeypadSymbol.O, KeypadSymbol.A, KeypadSymbol.Lambda, KeypadSymbol.N, KeypadSymbol.Spin, KeypadSymbol.H, KeypadSymbol.CMirrored ] },
        { 1, [ KeypadSymbol.E, KeypadSymbol.O, KeypadSymbol.CMirrored, KeypadSymbol.Krul, KeypadSymbol.Ster, KeypadSymbol.H, KeypadSymbol.Questionmark ] },
        { 2, [ KeypadSymbol.Copyright, KeypadSymbol.W, KeypadSymbol.Krul, KeypadSymbol.KK, KeypadSymbol.R, KeypadSymbol.Lambda, KeypadSymbol.Ster ] },
        { 3, [ KeypadSymbol.Zes, KeypadSymbol.P, KeypadSymbol.B, KeypadSymbol.Spin, KeypadSymbol.KK, KeypadSymbol.Questionmark, KeypadSymbol.Smiley ] },
        { 4, [ KeypadSymbol.Psi, KeypadSymbol.Smiley, KeypadSymbol.B, KeypadSymbol.C, KeypadSymbol.P, KeypadSymbol.Slang, KeypadSymbol.SterGevuld ] },
        { 5, [ KeypadSymbol.Zes, KeypadSymbol.E, KeypadSymbol.Equals, KeypadSymbol.Ae, KeypadSymbol.Psi, KeypadSymbol.GroteN, KeypadSymbol.Omega ] },
    };
}

public enum KeypadSymbol { O, A, Lambda, N, Spin, H, CMirrored, E, Krul, Ster, Questionmark, Copyright, W, KK, R, Zes, P, B, Smiley, Psi, C, Slang, SterGevuld, Equals, Ae, GroteN, Omega };

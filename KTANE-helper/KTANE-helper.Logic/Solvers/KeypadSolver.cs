using System;
using System.Collections.Generic;
using System.Linq;

namespace KTANE_helper.Logic.Solvers;

public class KeypadSolver : Solvable<KeypadSolver>
{
    public override void Solve(BombKnowledge bk)
    {
        var resultColumn = GetColumn();
        _ioHandler.ShowLine($"Solution found! Your result column is: {DisplaySymbols(resultColumn)}");
    }

    private static List<Symbol> GetColumn()
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

        var setA = new HashSet<Symbol>(columnToSymbol[a]);
        var uniqueA = setA.Except(columnToSymbol[b]);

        return SeeAny(uniqueA);
    }

    private static bool SeeAny(IEnumerable<Symbol> symbols) => _ioHandler.Ask($"Do you see any of {DisplaySymbols(symbols)}\n");

    private static string DisplaySymbols(IEnumerable<Symbol> symbols) => string.Join("", symbols.Select(s => "\n    " + GetSymbolString(s))); // todo make general

    private enum Symbol { O, A, Lambda, N, Spin, H, CMirrored, E, Krul, Ster, Questionmark, Copyright, W, KK, R, Zes, P, B, Smiley, Psi, C, Slang, SterGevuld, Equals, Ae, GroteN, Omega };

    private static IEnumerable<Symbol> allSymbols = Enum.GetValues(typeof(Symbol)).Cast<Symbol>();

    private static string GetSymbolString(Symbol symbol) => symbol switch
    {
        Symbol.O => "o",
        Symbol.A => "a",
        Symbol.Lambda => "lambda",
        Symbol.N => "n",
        Symbol.Spin => "spin",
        Symbol.H => "h",
        Symbol.CMirrored => "cc",
        Symbol.E => "\"e",
        Symbol.Krul => "krul",
        Symbol.Ster => "ster",
        Symbol.Questionmark => "?",
        Symbol.Copyright => "cr",
        Symbol.W => "w",
        Symbol.KK => "kk",
        Symbol.R => "r",
        Symbol.Zes => "6",
        Symbol.P => "p",
        Symbol.B => "b",
        Symbol.Smiley => ":)",
        Symbol.Psi => "psi",
        Symbol.C => "c",
        Symbol.Slang => "slang",
        Symbol.SterGevuld => "gevulde ster",
        Symbol.Equals => "=",
        Symbol.Ae => "ae",
        Symbol.GroteN => "grote n",
        Symbol.Omega => "omega",
    };
    private static Symbol GetSymbolFromString(string stringSymbol) => allSymbols.First(k => GetSymbolString(k) == stringSymbol);

    private static readonly Dictionary<Symbol, List<int>> symbolToColumn = new()
    {
        { Symbol.O, new() { 0, 1 } },
        { Symbol.A, new() { 0 } },
        { Symbol.Lambda, new() { 0, 2 } },
        { Symbol.N, new() { 0 } },
        { Symbol.Spin, new() { 0, 3 } },
        { Symbol.H, new() { 0, 1 } },
        { Symbol.CMirrored, new() { 0, 1 } },
        { Symbol.E, new() { 1, 5 } },
        { Symbol.Krul, new() { 1, 2 } },
        { Symbol.Ster, new() { 1, 2 } },
        { Symbol.Questionmark, new() { 1, 3 } },
        { Symbol.Copyright, new() { 2 } },
        { Symbol.W, new() { 2 } },
        { Symbol.KK, new() { 2, 3 } },
        { Symbol.R, new() { 2 } },
        { Symbol.Zes, new() { 3, 5 } },
        { Symbol.P, new() { 3, 4 } },
        { Symbol.B, new() { 3, 4 } },
        { Symbol.Smiley, new() { 3, 4 } },
        { Symbol.Psi, new() { 4, 5 } },
        { Symbol.C, new() { 4 } },
        { Symbol.Slang, new() { 4 } },
        { Symbol.SterGevuld, new() { 4 } },
        { Symbol.Equals, new() { 5 } },
        { Symbol.Ae, new() { 5 } },
        { Symbol.GroteN, new() { 5 } },
        { Symbol.Omega, new() { 5 } },
    };

    private static readonly Dictionary<int, List<Symbol>> columnToSymbol = new()
    {
        { 0, new() { Symbol.O, Symbol.A, Symbol.Lambda, Symbol.N, Symbol.Spin, Symbol.H, Symbol.CMirrored } },
        { 1, new() { Symbol.E, Symbol.O, Symbol.CMirrored, Symbol.Krul, Symbol.Ster, Symbol.H, Symbol.Questionmark } },
        { 2, new() { Symbol.Copyright, Symbol.W, Symbol.Krul, Symbol.KK, Symbol.R, Symbol.Lambda, Symbol.Ster } },
        { 3, new() { Symbol.Zes, Symbol.P, Symbol.B, Symbol.Spin, Symbol.KK, Symbol.Questionmark, Symbol.Smiley } },
        { 4, new() { Symbol.Psi, Symbol.Smiley, Symbol.B, Symbol.C, Symbol.P, Symbol.Slang, Symbol.SterGevuld } },
        { 5, new() { Symbol.Zes, Symbol.E, Symbol.Equals, Symbol.Ae, Symbol.Psi, Symbol.GroteN, Symbol.Omega } },
    };
}
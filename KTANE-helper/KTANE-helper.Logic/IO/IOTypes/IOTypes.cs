namespace KTANE_helper.Logic.IO;

public abstract record Response;

public record ResponseQuit : Response;

public record ResponseContent(
    IEnumerable<int>                  Integers,
    IEnumerable<char>                 Characters,
    IEnumerable<string>               Strings,
    IEnumerable<IOTypes.Colour>       Colours,
    IEnumerable<IOTypes.KeypadSymbol> KeypadSymbols,
    IEnumerable<IOTypes.Coordinate>   Coordinates
    ) : Response;

public static class IOTypes
{
    [Flags]
    public enum InputRequest
    {
        None         = 0,
        Integer      = 1 << 0,
        Character    = 1 << 1,
        String       = 1 << 2,
        Colour       = 1 << 3,
        KeypadSymbol = 1 << 4,
        Coordinate   = 1 << 5,
    }

    #region Type Colour
    public enum Colour 
    {
        Black,
        Blue,
        Green,
        Red,
        White,
        Yellow,
    }

    public const string BlackStringRepresentation = "z";
    public const string BlueStringRepresentation = "b";
    public const string GreenStringRepresentation = "g";
    public const string RedStringRepresentation = "r";
    public const string WhiteStringRepresentation = "w";
    public const string YellowStringRepresentation = "y";
    #endregion

    #region Type Keypad Symbol
    public enum KeypadSymbol { O, A, Lambda, N, Spin, H, CMirrored, E, Krul, Ster, Questionmark, Copyright, W, KK, R, Zes, P, B, Smiley, Psi, C, Slang, SterGevuld, Equals, Ae, GroteN, Omega };

    public static IEnumerable<KeypadSymbol> AllKeypadSymbols = Enum.GetValues<KeypadSymbol>().Cast<KeypadSymbol>();

    public static string GetSymbolString(this KeypadSymbol symbol) => symbol switch
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
        _ => throw new ArgumentOutOfRangeException(nameof(symbol)),
    };

    public static KeypadSymbol GetSymbolFromString(string stringSymbol) => AllKeypadSymbols.First(k => GetSymbolString(k) == stringSymbol);
    #endregion

    #region Type Coordinate
    // Coordinates are 1-based

    public record Coordinate(int X, int Y);

    public static int LinearCoordinate(this Coordinate coordinate, int xSize)
        => (coordinate.Y - 1) * xSize + (coordinate.X - 1);
    #endregion
}

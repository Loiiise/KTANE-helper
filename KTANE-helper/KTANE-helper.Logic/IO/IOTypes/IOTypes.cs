namespace KTANE_helper.Logic.IO;

public abstract record UserInput;

public record InputQuit : UserInput;

public record InputContent<T1>(
    IEnumerable<T1> Type1
    ) : UserInput;

public record InputContent<T1, T2>(
    IEnumerable<T1> Type1,
    IEnumerable<T2> Type2
    ) : UserInput;

public static class IOTypes
{
    [Flags]
    public enum InputRequest
    {
        None                    = 0,
        Integer                 = 1 << 0,
        Character               = 1 << 1,
        String                  = 1 << 2,
        Colour                  = 1 << 3,
        ComplicatedWireProperty = 1 << 4,
        WireSequenceEndpoint    = 1 << 5,
        KeypadSymbol            = 1 << 6,
        Coordinate              = 1 << 7,
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

    #region Complicated Wire Property
    [Flags]
    public enum ComplicatedWireProperty
    {
        None = 0,
        Red  = 1 << 0,
        Blue = 1 << 1,
        Star = 1 << 2,
        Led  = 1 << 3,
    }
    #endregion

    #region Wire Sequence Endpoint
    [Flags]
    public enum WireSequenceEndpoint
    {
        A = 1 << 0,
        B = 1 << 1,
        C = 1 << 2,
    }
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

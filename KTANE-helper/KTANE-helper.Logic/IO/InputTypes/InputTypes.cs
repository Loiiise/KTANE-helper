using KTANE_helper.Logic.Solvers;

namespace KTANE_helper.Logic.IO;

public class InputTypes
{
    [Flags]
    public enum QueryRequest
    {
        None         = 0,
        Integer      = 1 << 0,
        Character    = 1 << 1,
        String       = 1 << 2,
        Colour       = 1 << 3,
        KeypadSymbol = 1 << 4,
        Coordinate   = 1 << 5,
    }

    public abstract record QueryResponse;

    public record QueryResponseQuit : QueryResponse;

    public record QueryResponseContent(
        IEnumerable<int>          Integers,
        IEnumerable<char>         Characters,
        IEnumerable<string>       Strings,
        IEnumerable<Colour>       Colours,
        IEnumerable<KeypadSymbol> KeypadSymbols,
        IEnumerable<(int, int)>   Coordinates
        ) : QueryResponse;

    public enum Colour 
    {
        Black,
        Blue,
        Green,
        Red,
        White,
        Yellow,
    }

    public const string BlackStringRepresentation  = "z";
    public const string BlueStringRepresentation   = "b";
    public const string GreenStringRepresentation  = "g";
    public const string RedStringRepresentation    = "r";
    public const string WhiteStringRepresentation  = "w";
    public const string YellowStringRepresentation = "y";
}

namespace KTANE_helper.Logic.IO;
public static class IOExtensions
{
    public static string PositionWord(this int p) => p switch
    {
        0 => "zeroeth",
        1 => "first",
        2 => "second",
        3 => "third",
        4 => "fourth",
        5 => "fifth",
        6 => "sixth",
        _ => $"{p}th"
    };

    public static bool HasIllegalCharacters(this string input, params char[] legalCharacters)
    {
        foreach (char c in input)
        {
            if (!legalCharacters.Contains(c))
            {
                return true;
            }
        }

        return false;
    }
}

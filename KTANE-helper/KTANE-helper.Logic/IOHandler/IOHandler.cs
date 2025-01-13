using System.Collections.Generic;
using System.Linq;

namespace KTANE_helper.Logic;

public abstract class IOHandler : IIOHandler
{
    private Stack<string> scopeStack = new();
    public void Prompt()
    {
        var scope = scopeStack.Any() ? scopeStack.Peek() : ">";

        Show($"{scope}> ");
    }
    public void PromptScopeAdd(string str) => scopeStack.Push(scopeStack.Peek() + $" ({str})");
    public void PromptScopeUp(string sc) => scopeStack.Push(sc);
    public void PromptScopeDown()
    {
        if (scopeStack.Count > 0) scopeStack.Pop();
    }

    public abstract void Show(string message);
    public abstract void ShowLine(string message);
    public abstract string ReadLine();

    public string Query(string message)
    {
        ShowLine(message);
        Prompt();
        return ReadLine();
    }

    public string Query(string message, IEnumerable<string> allowedValues)
    {
        string result;
        var lowerAllowed = allowedValues.Select(x => x.ToLower());
        while (!lowerAllowed.Contains((result = Query(message)).ToLower())) ShowLine("This value is not allowed!");
        return result;
    }

    public IEnumerable<string> QueryMultiple(string message, int n)
    {
        ShowLine(message);
        return GetLines(n);
    }

    public int IntQuery(string message)
    {
        int result;

        while (!int.TryParse(Query(message), out result))
            ShowLine("That is not a number!");

        return result;
    }

    public int IntQuery(string message, IEnumerable<int> allowedValues)
    {
        int result;
        while (!allowedValues.Contains(result = IntQuery(message))) ShowLine("This value is not allowed!");
        return result;
    }

    public (int, int) CoordinateQuery(string message) => CoordinateQuery(message, int.MinValue, int.MaxValue);
    public (int, int) CoordinateQuery(string message, int minimalValue, int maximalValue)
    {
        while (true)
        {
            var line = Query(message);

            if (IsCoordinates(line, out int x, out int y))
            {
                if (x >= minimalValue && x <= maximalValue &&
                    y >= minimalValue && y <= maximalValue)
                {
                    return (x, y);
                }
                else
                {
                    ShowLine($"Both coordinates must be at least {minimalValue} and at most {maximalValue}");
                }
            }

            ShowLine("Those are not valid coordinates. Make sure to separate them with a comma or a space.");
        }

        bool IsCoordinates(string line, out int x, out int y)
        {
            string[] parts;
            if (line.Contains(','))
            {
                parts = line.Split(',');
            }
            else
            {
                parts = line.Split(' ');
            }

            x = -1;
            y = -1;

            return
                parts.Length >= 2 &&
                int.TryParse(parts[0], out x) &&
                int.TryParse(parts[1], out y);
        }
    }

    public bool Ask(string question)
        => Query(question + " ((y)es/(n)o)", new string[] { "y", "yes", "yessir!", "n", "no", "fuck off", "" }).Contains('y');

    public IEnumerable<string> GetLines(int lines)
    {
        for (int i = 0; i < lines; ++i)
        {
            Prompt();
            yield return ReadLine();
        }
    }

    public string PositionWord(int p) => p switch
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

    public bool HasIllegalCharacters(string input, params char[] legalCharacters)
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

using System;
using System.Collections.Generic;
using System.Linq;

namespace KTANE_helper
{
    internal static class IOHandler
    {
        private static Stack<string> scopeStack = new();
        internal static void Prompt()
        {
            var scope = scopeStack.Any() ? scopeStack.Peek() : ">";

            Console.Write($"{scope}> ");
        }
        internal static void PromptScopeAdd(string str) => scopeStack.Push(scopeStack.Peek() + $" ({str})");
        internal static void PromptScopeUp(string sc) => scopeStack.Push(sc);
        internal static void PromptScopeDown()
        {
            if (scopeStack.Count > 0) scopeStack.Pop();
        }

        internal static void Show(string message) => Console.WriteLine(message);

        internal static string Query(string message)
        {
            Show(message);
            Prompt();
            return Console.ReadLine();
        }

        internal static string Query(string message, IEnumerable<string> allowedValues)
        {
            string result;
            var lowerAllowed = allowedValues.Select(x => x.ToLower());
            while (!lowerAllowed.Contains((result = Query(message)).ToLower())) Console.WriteLine("This value is not allowed!");
            return result;
        }

        internal static IEnumerable<string> QueryMultiple(string message, int n)
        {
            Show(message);
            return GetLines(n);
        }

        internal static int IntQuery(string message)
        {
            int result;

            while (!int.TryParse(Query(message), out result))
                Console.WriteLine("That is not a number!");

            return result;
        }

        internal static int IntQuery(string message, IEnumerable<int> allowedValues)
        {
            int result;
            while (!allowedValues.Contains(result = IntQuery(message))) Console.WriteLine("This value is not allowed!");
            return result;
        }

        internal static (int, int) CoordinateQuery(string message) => CoordinateQuery(message, int.MinValue, int.MaxValue);
        internal static (int, int) CoordinateQuery(string message, int minimalValue, int maximalValue)
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
                        Show($"Both coordinates must be at least {minimalValue} and at most {maximalValue}");
                    }
                }

                Show("Those are not valid coordinates. Make sure to separate them with a comma or a space.");
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

        internal static bool Ask(string question)
            => Query(question + " ((y)es/(n)o)", new string[] { "y", "yes", "yessir!", "n", "no", "fuck off", "" }).Contains('y');

        internal static IEnumerable<string> GetLines(int lines)
        {
            for (int i = 0; i < lines; ++i)
            {
                Prompt();
                yield return Console.ReadLine();
            }
        }

        internal static string PositionWord(int p) => p switch
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

        internal static bool HasIllegalCharacters(string input, params char[] legalCharacters)
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
}

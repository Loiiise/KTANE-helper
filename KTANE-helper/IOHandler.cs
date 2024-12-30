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

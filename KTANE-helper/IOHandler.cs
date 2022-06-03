using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTANE_helper
{
    internal static class IOHandler
    {
        private static string scope = ">";
        internal static void Prompt() => Console.Write($"{scope}> ");
        internal static void SetPromptScope(string sc) => scope = sc;
        internal static void ResetPromptScope() => SetPromptScope(">");

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
            while (!allowedValues.Contains(result = Query(message))) Console.WriteLine("This value is not allowed!");
            return result;
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

        internal static string PositionWord(int p)
        {
            return p switch
            {
                0 => "zeroeth",
                1 => "first",
                2 => "second",
                3 => "third",
                4 => "fourth",
                5 => "fifth",
                6 => "sixth",
                _ => "i cannot count this high, wtf"
            };
        }

    }
}

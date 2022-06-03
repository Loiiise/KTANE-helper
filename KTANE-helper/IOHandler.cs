using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTANE_helper
{
    internal static class IOHandler
    {
        internal static void Prompt() => Console.Write($"{scope}> ");

        internal static string Query(string message)
        {
            Console.WriteLine(message);
            Prompt();
            return Console.ReadLine();
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

        internal static IEnumerable<string> GetLines(int lines)
        {
            for (int i = 0; i < lines; ++i)
            {
                Console.Write(">> ");
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

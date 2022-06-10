using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KTANE_helper.IOHandler;

namespace KTANE_helper
{
    internal static class SimonSays // todo add interface ISolvable
    {
        internal static void Solve(BombKnowledge bk)
        {
            bool noVowel = !bk.SerialNumberContainsVowel();
            int strikes = IntQuery("How many strikes do you have?", new List<int> { 0, 1, 2 });

            var mapping = GetMapping(noVowel, strikes);
            var sequence = new List<Colour>();

            while (AskNextColour(out Colour currentColour))
            {
                sequence.Add(mapping[currentColour]);
                Show($"Enter the following sequence: {DisplayColours(sequence)}");
            }
        }

        private static bool AskNextColour(out Colour colour)
        {
            colour = Query("What colour is the new flash?", new List<string> { "r", "g", "b", "y", "q", "s" }) switch
            {
                "r" => Colour.Red,
                "g" => Colour.Green,
                "b" => Colour.Blue,
                "y" => Colour.Yellow,
                _   => Colour.Nothing,
            };

            return colour != Colour.Nothing;
        }

        private static string DisplayColours(IEnumerable<Colour> colours) => string.Join("", colours.Select(c => "\n    " + c.ToString()));

        private static Dictionary<Colour, Colour> GetMapping(bool noVowel, int strikes)
        {
            int index = 3 * (noVowel ? 1 : 0) + strikes;

            var dict = new Dictionary<Colour, Colour>();
            var keys = new List<Colour> { Colour.Red, Colour.Blue, Colour.Green, Colour.Yellow };
            int i = 0;

            foreach (var value in mappings[index]) dict[keys[i++]] = value;

            return dict;
        }

        private static readonly List<IEnumerable<Colour>> mappings = new()
        {
            new List<Colour>() { Colour.Blue, Colour.Red, Colour.Yellow, Colour.Green },
            new List<Colour>() { Colour.Yellow, Colour.Green, Colour.Blue, Colour.Red },
            new List<Colour>() { Colour.Green, Colour.Red, Colour.Yellow, Colour.Blue },
            new List<Colour>() { Colour.Blue, Colour.Yellow, Colour.Green, Colour.Red },
            new List<Colour>() { Colour.Red, Colour.Blue, Colour.Yellow, Colour.Green },
            new List<Colour>() { Colour.Yellow, Colour.Green, Colour.Blue, Colour.Red },
        };

        private enum Colour { Red, Blue, Green, Yellow, Nothing };
    }
}

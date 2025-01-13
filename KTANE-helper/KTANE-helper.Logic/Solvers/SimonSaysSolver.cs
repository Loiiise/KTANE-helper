using KTANE_helper.Logic.IO;
using System.Collections.Generic;
using System.Linq;

namespace KTANE_helper.Logic.Solvers;

public class SimonSaysSolver : Solvable<SimonSaysSolver>
{
    public override void Solve(BombKnowledge bk)
    {
        bool noVowel = !bk.SerialNumberContainsVowel();
        int strikes = _ioHandler.IntQuery("How many strikes do you have?", new List<int> { 0, 1, 2 });

        var mapping = GetMapping(noVowel, strikes);
        var sequence = new List<SimonSaysColour>();

        while (AskNextColour(out SimonSaysColour currentColour))
        {
            sequence.Add(mapping[currentColour]);
            _ioHandler.Answer(new SimonSaysAnswer { Value = sequence.ToArray() });
        }
    }

    private static bool AskNextColour(out SimonSaysColour colour)
    {
        colour = _ioHandler.Query("What colour is the new flash?", new List<string> { "r", "g", "b", "y", "q", "s" }) switch
        {
            "r" => SimonSaysColour.Red,
            "g" => SimonSaysColour.Green,
            "b" => SimonSaysColour.Blue,
            "y" => SimonSaysColour.Yellow,
            _ => SimonSaysColour.Nothing,
        };

        return colour != SimonSaysColour.Nothing;
    }

    private static Dictionary<SimonSaysColour, SimonSaysColour> GetMapping(bool noVowel, int strikes)
    {
        int index = 3 * (noVowel ? 1 : 0) + strikes;

        var dict = new Dictionary<SimonSaysColour, SimonSaysColour>();
        var keys = new List<SimonSaysColour> { SimonSaysColour.Red, SimonSaysColour.Blue, SimonSaysColour.Green, SimonSaysColour.Yellow };
        int i = 0;

        foreach (var value in mappings[index]) dict[keys[i++]] = value;

        return dict;
    }

    private static readonly List<IEnumerable<SimonSaysColour>> mappings = new()
    {
        new List<SimonSaysColour>() { SimonSaysColour.Blue, SimonSaysColour.Red, SimonSaysColour.Yellow, SimonSaysColour.Green },
        new List<SimonSaysColour>() { SimonSaysColour.Yellow, SimonSaysColour.Green, SimonSaysColour.Blue, SimonSaysColour.Red },
        new List<SimonSaysColour>() { SimonSaysColour.Green, SimonSaysColour.Red, SimonSaysColour.Yellow, SimonSaysColour.Blue },
        new List<SimonSaysColour>() { SimonSaysColour.Blue, SimonSaysColour.Yellow, SimonSaysColour.Green, SimonSaysColour.Red },
        new List<SimonSaysColour>() { SimonSaysColour.Red, SimonSaysColour.Blue, SimonSaysColour.Yellow, SimonSaysColour.Green },
        new List<SimonSaysColour>() { SimonSaysColour.Yellow, SimonSaysColour.Green, SimonSaysColour.Blue, SimonSaysColour.Red },
    };

}
public enum SimonSaysColour { Red, Blue, Green, Yellow, Nothing };

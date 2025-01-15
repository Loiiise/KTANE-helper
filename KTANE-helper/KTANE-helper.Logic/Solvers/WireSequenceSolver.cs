using KTANE_helper.Logic.IO;

namespace KTANE_helper.Logic.Solvers;

public class WireSequenceSolver : Solvable<WireSequenceSolver>
{
    public override void Solve(BombKnowledge bk)
    {
        int _redCounter = 0;
        int _blueCounter = 0;
        int _blackCounter = 0;

        while (true)
        {
            var userInput = _ioHandler.Query("Input the wires in order of starting point by giving their colour and endpoint connection. (R = Red, B = Blue, Z = Black)").ToUpper();

            if (userInput.Length > 6 ||
                userInput.HasIllegalCharacters('R', 'B', 'Z', 'A', 'C'))
            {
                break;
            }

            List<bool> cut = new();

            foreach (var wire in GetWires(userInput))
            {
                var cutIfIsThisOne = wire.Colour switch
                {
                    WireSequenceColour.Red => _redMap[_redCounter++],
                    WireSequenceColour.Blue => _blueMap[_blueCounter++],
                    WireSequenceColour.Black => _blackMap[_blackCounter++],
                    _ => throw new ArgumentException(),
                };

                cut.Add(cutIfIsThisOne.HasFlag(wire.Type));
            }

            // If everything is true
            if (cut.All(x => x)) Answer(WireSequenceAnswerState.Everything);
            // If everything is false
            else if (cut.All(x => !x)) Answer(WireSequenceAnswerState.Nothing);
            // If the first thing is true and the rest is false
            else if (cut.First() && cut.Skip(1).All(x => !x)) Answer(WireSequenceAnswerState.FirstOnly);
            // If the last thing is true and the rest is false
            else if (cut.Last() && cut.Take(cut.Count - 1).All(x => !x)) Answer(WireSequenceAnswerState.LastOnly);
            else
            {
                var wireIndices = cut
                    .Zip(Enumerable.Range(1, cut.Count)) // Zip the booleans with their index
                    .Where(x => x.First)                 // Filter only those tuples where the boolean is true
                    .Select(x => x.Second)               // Throw away the boolean and keep just the index
                    .ToArray();

                Answer(WireSequenceAnswerState.Custom, wireIndices);
            }
        }
    }

    private void Answer(WireSequenceAnswerState state) => Answer(state, Array.Empty<int>());
    private void Answer(WireSequenceAnswerState state, int[] wiresToCutIfCustom)
        => _ioHandler.Answer(new WireSequenceAnswer { Value = new WireSequenceAnswerValue(state, wiresToCutIfCustom) });

    private IEnumerable<Wire> GetWires(string input)
    {
        for (int i = 0; i < input.Length / 2; ++i)
        {
            var wire = input.Substring(i * 2, 2);

            var colour = wire[0] switch
            {
                'R' => WireSequenceColour.Red,
                'B' => WireSequenceColour.Blue,
                'Z' => WireSequenceColour.Black,
                _ => throw new ArgumentException($"Illegal wire colour \"{wire[0]}\""),
            };

            var type = wire[1] switch
            {
                'A' => WireSequenceType.A,
                'B' => WireSequenceType.B,
                'C' => WireSequenceType.C,
                _ => throw new ArgumentException($"Illegal wire type \"{wire[1]}\""),
            };

            yield return new(colour, type);
        }
    }

    private readonly WireSequenceType[] _redMap =
    [
        WireSequenceType.C,
        WireSequenceType.B,
        WireSequenceType.A,
        WireSequenceType.A | WireSequenceType.C,
        WireSequenceType.B,
        WireSequenceType.A | WireSequenceType.C,
        WireSequenceType.A | WireSequenceType.B | WireSequenceType.C,
        WireSequenceType.A | WireSequenceType.B,
        WireSequenceType.B,
    ];
    private readonly WireSequenceType[] _blueMap =
    [
        WireSequenceType.B,
        WireSequenceType.A | WireSequenceType.C,
        WireSequenceType.B,
        WireSequenceType.A,
        WireSequenceType.B,
        WireSequenceType.B | WireSequenceType.C,
        WireSequenceType.C,
        WireSequenceType.A | WireSequenceType.C,
        WireSequenceType.A,
    ];
    private readonly WireSequenceType[] _blackMap =
    [
        WireSequenceType.A | WireSequenceType.B | WireSequenceType.C,
        WireSequenceType.A | WireSequenceType.C,
        WireSequenceType.B,
        WireSequenceType.A | WireSequenceType.C,
        WireSequenceType.B,
        WireSequenceType.B | WireSequenceType.C,
        WireSequenceType.A | WireSequenceType.B,
        WireSequenceType.C,
        WireSequenceType.C,
    ];

    private record Wire(WireSequenceColour Colour, WireSequenceType Type);
}

[Flags]
enum WireSequenceType
{
    A = 1 << 0,
    B = 1 << 1,
    C = 1 << 2,
}

enum WireSequenceColour
{
    Red,
    Blue,
    Black
}

public enum WireSequenceAnswerState
{
    Everything,
    Nothing,
    FirstOnly,
    LastOnly,
    Custom,
}
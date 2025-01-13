using System;
using System.Collections.Generic;
using static KTANE_helper.IOHandler;

namespace KTANE_helper.Solvers
{
    internal class WireSequenceSolver : Solvable<WireSequenceSolver>
    {
        internal override void Solve(BombKnowledge bk)
        {
            int _redCounter = 0;
            int _blueCounter = 0;
            int _blackCounter = 0;

            while (true)
            {
                var userInput = Query("Input the wires in order of starting point by giving their colour and endpoint connection. (R = Red, B = Blue, Z = Black)").ToUpper();
                var wireCounter = 0;

                if (userInput.Length > 6 ||
                    HasIllegalCharacters(userInput, 'R', 'B', 'Z', 'A', 'C'))
                {
                    break;
                }

                foreach (var wire in GetWires(userInput))
                {
                    WireSequenceType cutIfIsThisOne;

                    switch (wire.Colour)
                    {
                        case WireSequenceColour.Red:
                            cutIfIsThisOne = _redMap[_redCounter++];
                            break;
                        case WireSequenceColour.Blue:
                            cutIfIsThisOne = _blueMap[_blueCounter++];
                            break;
                        case WireSequenceColour.Black:
                            cutIfIsThisOne = _blackMap[_blackCounter++];
                            break;
                        default: throw new ArgumentOutOfRangeException();
                    }

                    if (cutIfIsThisOne.HasFlag(wire.Type))
                    {
                        Show($"Cut the {PositionWord(++wireCounter)} wire.");
                    }
                    else
                    {
                        Show($"DON'T cut the {PositionWord(++wireCounter)} wire.");
                    }
                }
            }

        }

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
                    _ => throw new ArgumentOutOfRangeException($"Illegal wire colour \"{wire[0]}\""),
                };

                var type = wire[1] switch
                {
                    'A' => WireSequenceType.A,
                    'B' => WireSequenceType.B,
                    'C' => WireSequenceType.C,
                    _ => throw new ArgumentOutOfRangeException($"Illegal wire type \"{wire[1]}\""),
                };

                yield return new(colour, type);
            }
        }

        private readonly WireSequenceType[] _redMap = new WireSequenceType[]
        {
            WireSequenceType.C,
            WireSequenceType.B,
            WireSequenceType.A,
            WireSequenceType.A | WireSequenceType.C,
            WireSequenceType.B,
            WireSequenceType.A | WireSequenceType.C,
            WireSequenceType.A | WireSequenceType.B | WireSequenceType.C,
            WireSequenceType.A | WireSequenceType.B,
            WireSequenceType.B,
        };
        private readonly WireSequenceType[] _blueMap = new WireSequenceType[]
        {
            WireSequenceType.B,
            WireSequenceType.A | WireSequenceType.C,
            WireSequenceType.B,
            WireSequenceType.A,
            WireSequenceType.B,
            WireSequenceType.B | WireSequenceType.C,
            WireSequenceType.C,
            WireSequenceType.A | WireSequenceType.C,
            WireSequenceType.A,
        };
        private readonly WireSequenceType[] _blackMap = new WireSequenceType[]
        {
            WireSequenceType.A | WireSequenceType.B | WireSequenceType.C,
            WireSequenceType.A | WireSequenceType.C,
            WireSequenceType.B,
            WireSequenceType.A | WireSequenceType.C,
            WireSequenceType.B,
            WireSequenceType.B | WireSequenceType.C,
            WireSequenceType.A | WireSequenceType.B,
            WireSequenceType.C,
            WireSequenceType.C,
        };

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
}

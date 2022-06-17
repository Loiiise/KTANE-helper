using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KTANE_helper.IOHandler;

namespace KTANE_helper.Solvers
{
    internal class WireSolver : Solvable<WireSolver>
    {
        internal override void Solve(BombKnowledge bk)
        {
            string wires = Query("What wires are present?");

            int amount = wires.Length;
            int lastWire = amount - 1;

            int redWires = wires.Where(w => w == 'r').Count(); // refactor to wires.Count(predicate)
            int whiteWires = wires.Where(w => w == 'w').Count();
            int blueWires = wires.Where(w => w == 'b').Count();
            int blackWires = wires.Where(w => w == 'z').Count();
            int yellowWires = wires.Where(w => w == 'g').Count();

            char lastWireColour = wires[lastWire];

            switch (amount)
            {
                case 3:
                    if (redWires == 0) Cut(2);
                    else if (wires.Last() == 'w') Cut(lastWire);
                    else if (blueWires > 1) CutLast('b');
                    else Cut(lastWire);
                    return;
                case 4:
                    if (redWires > 1 && bk.SerialNumberLastDigitOdd()) CutLast('r');
                    else if (wires.Last() == 'g' && redWires == 0 || blueWires == 1) Cut(1);
                    else if (yellowWires > 1) Cut(lastWire);
                    else Cut(2);
                    return;
                case 5:
                    if (lastWireColour == 'b' && bk.SerialNumberLastDigitOdd()) Cut(4);
                    else if (redWires == 1 && yellowWires <= 1) Cut(1);
                    else if (blackWires == 0) Cut(2);
                    else Cut(1);
                    return;
                case 6:
                    if (yellowWires == 0 && bk.SerialNumberLastDigitOdd()) Cut(3);
                    else if (yellowWires == 1 && whiteWires > 1) Cut(4);
                    else if (redWires == 0) Cut(lastWire);
                    else Cut(4);
                    return;
                default:
                    Show(".... idiot");
                    Solve(bk);
                    return;
            }

            void Cut(int wire)
            {
                Show($"Cut the {PositionWord(wire)} wire");
            }
            void CutLast(char colour) => Cut(LastIndex(colour));

            int LastIndex(char colour)
            {
                for (int i = lastWire; i > 0; --i)
                    if (wires[i] == colour)
                        return i;
                return 0;
            }
        }
    }
}

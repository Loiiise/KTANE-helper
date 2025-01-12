using KTANE_helper.Solvers;

namespace KTANE_helper.NeedyModules;

internal class NeedyKnob : Solvable<NeedyKnob>
{
    internal override void Solve(BombKnowledge bk)
    {
        _ioHandler.ShowLine(_ioHandler.Query("What are the first three lamps? (1 for on, 0 for off)") switch
        {
            "001" => "Up",
            "011" => "Down",
            "000" => "Left",
            "101" => _ioHandler.Query("What are the first three lamps (BOTTOM ROW)? (1 for on, 0 for off)") switch
            {
                "011" => "Up",
                "010" => "Down",
                "111" => "Right",
                _ => _invalidInputResponse,
            },
            _ => _invalidInputResponse,
        });
    }

    private const string _invalidInputResponse = "You provided invalid input, cancelling operation...";
}
namespace KTANE_helper.Solvers;

public class ButtonSolver : Solvable<ButtonSolver>
{
    public override void Solve(BombKnowledge bk)
    {
        var data = _ioHandler.Query("What are the colour (dutch first letter) and the text of the button.").Split(' ');
        var colour = data[0].ToLower()[0];
        var text = data[1].ToLower();

        if (colour == 'b' && text == "abort") HoldAndRelease();
        else if (text == "detonate" && (bk.Batteries()) > 1) PressAndRelease();
        else if (colour == 'w' && bk.LitIndicator("CAR")) HoldAndRelease();
        else if (bk.Batteries() > 2 && bk.LitIndicator("FRK")) PressAndRelease();
        else if (colour == 'g') HoldAndRelease();
        else if (colour == 'r' && text == "hold") PressAndRelease();
        else HoldAndRelease();

        void PressAndRelease() => _ioHandler.ShowLine("Press and immediately release the button.");

        void HoldAndRelease()
        {
            string colour = _ioHandler.Query("What colour is the strip?");
            if (colour == "b") ReleaseWhen(4);
            else if (colour == "g") ReleaseWhen(5);
            else ReleaseWhen(1);

            void ReleaseWhen(int i) => _ioHandler.ShowLine($"Release when the countdown timer has a {i} in any position");
        }
    }
}

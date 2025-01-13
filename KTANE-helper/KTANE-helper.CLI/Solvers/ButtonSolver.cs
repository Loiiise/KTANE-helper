using static KTANE_helper.IOHandler;

namespace KTANE_helper.Solvers
{
    internal class ButtonSolver : Solvable<ButtonSolver>
    {
        internal override void Solve(BombKnowledge bk)
        {
            var data = Query("What are the colour (dutch first letter) and the text of the button.").Split(' ');
            var colour = data[0].ToLower()[0];
            var text = data[1].ToLower();

            if (colour == 'b' && text == "abort") HoldAndRelease();
            else if (text == "detonate" && (bk.Batteries()) > 1) PressAndRelease();
            else if (colour == 'w' && bk.LitIndicator("CAR")) HoldAndRelease();
            else if (bk.Batteries() > 2 && bk.LitIndicator("FRK")) PressAndRelease();
            else if (colour == 'g') HoldAndRelease();
            else if (colour == 'r' && text == "hold") PressAndRelease();
            else HoldAndRelease();

            void PressAndRelease() => Show("Press and immediately release the button.");

            void HoldAndRelease()
            {
                string colour = Query("What colour is the strip?");
                if (colour == "b") ReleaseWhen(4);
                else if (colour == "g") ReleaseWhen(5);
                else ReleaseWhen(1);

                void ReleaseWhen(int i) => Show($"Release when the countdown timer has a {i} in any position");
            }
        }
    }
}

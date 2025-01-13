using KTANE_helper.Logic.IO;
using System.Runtime.CompilerServices;

namespace KTANE_helper.Logic.Solvers;

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

        void PressAndRelease() => _ioHandler.Answer(new ButtonAnswer { Value = new ButtonAnswerValue(ButtonReleaseOrHold.ReleaseImmediatly, null)});

        void HoldAndRelease()
        {
            _ioHandler.Answer(new ButtonAnswer { Value = new ButtonAnswerValue(ButtonReleaseOrHold.Hold, null) });
            string colour = _ioHandler.Query("What colour is the strip?");
            if (colour == "b") ReleaseWhen(4);
            else if (colour == "g") ReleaseWhen(5);
            else ReleaseWhen(1);

            void ReleaseWhen(int i) => _ioHandler.Answer(new ButtonAnswer { Value = new ButtonAnswerValue(ButtonReleaseOrHold.ReleaseWhen, i) });
        }        
    }
}

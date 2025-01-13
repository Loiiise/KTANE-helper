using KTANE_helper.Logic.IO;
using KTANE_helper.Logic.Solvers;

namespace KTANE_helper.CLI;

internal class CLIHandler : IOHandler
{
    public override void Show(string message) => Console.Write(message);
    public override void ShowLine(string message) => Console.WriteLine(message);
    public override string ReadLine() => Console.ReadLine() ?? string.Empty;

    public override void Answer<T>(Answer<T> answer) => ShowLine(answer switch
    {
        ButtonAnswer buttonAnswer => buttonAnswer.Value.ReleaseOrHold switch
        {
            ButtonReleaseOrHold.ReleaseImmediatly => "Press and immediately release the button.",
            ButtonReleaseOrHold.Hold => "Hold the button. Don't let go yet.", 
            ButtonReleaseOrHold.ReleaseWhen => $"Release when the countdown timer has a {buttonAnswer.Value.When } in any position",
            _ => ErrorInSolverMessage(nameof(ButtonSolver)),
        },
        ComplicatedWiresAnswer complicatedWiresAnswer => complicatedWiresAnswer.Value ? "CUT THE WIRE!" : "Don't cut.",
        KeypadAnswer keypadAnswer => $"Solution found! Your result column is: { KeypadSolver.DisplaySymbols(keypadAnswer.Value) }",
        MazeAnswer mazeAnswer => mazeAnswer.Value.Any() ?
            "The solution to the maze is: " + string.Join(',', mazeAnswer.Value) :
            "Start position is the target!",
        MemoryAnswer memoryAnswer => memoryAnswer.Value.PositionOrLabel switch 
        { 
            MemoryPositionOrLabel.Position => $"Press the button in the {memoryAnswer.Value.Value.PositionWord()} position", 
            MemoryPositionOrLabel.Label => $"Press the button labeled \"{memoryAnswer.Value.Value.PositionWord()}\"",
            _ => ErrorInSolverMessage(nameof(MemorySolver)),
        },
        MorseCodeAnswer morseCodeAnswer => $"The correct frequency is {morseCodeAnswer.Value} MHz",
        PasswordAnswer passwordAnswer => $"Password is {passwordAnswer.Value}",
        SimonSaysAnswer simonSaysAnswer => $"Enter the following sequence: {string.Join("", simonSaysAnswer.Value.Select(c => "\n    " + c.ToString()))}",
        WhosOnFirstAnswer whosOnFirstAnswer => string.Join('\n', whosOnFirstAnswer.Value),
        WireSequenceAnswer wireSequenceAnswer => throw new NotImplementedException(),
        WireAnswer wireAnswer => $"Cut the {wireAnswer.Value.PositionWord()} wire",
        _ => "Answer for this puzzle was received, but displaying it has not been implemented",
    });

    private string ErrorInSolverMessage(string solverName) => $"Something weird happend in {solverName}. Please contact Hookbloom";
}

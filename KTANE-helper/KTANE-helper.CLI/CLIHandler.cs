using KTANE_helper.Logic;
using KTANE_helper.Logic.IO;
using System;

namespace KTANE_helper.CLI;

internal class CLIHandler : IOHandler
{
    public override void Show(string message) => Console.Write(message);
    public override void ShowLine(string message) => Console.WriteLine(message);
    public override string ReadLine() => Console.ReadLine() ?? string.Empty;

    public override void Answer<T>(Answer<T> answer) => ShowLine(answer switch
    {
        WireAnswer wireAnswer => $"Cut the {wireAnswer.Value.PositionWord()} wire",
        _ => "Answer for this puzzle was received, but displaying it has not been implemented",
    });
}

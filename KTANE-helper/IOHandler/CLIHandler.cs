namespace KTANE_helper.IOHandler;

public class CLIHandler : IOHandler
{
    public override void Show(string message) => Console.Write(message);
    public override void ShowLine(string message) => Console.WriteLine(message);
    public override string ReadLine() => Console.ReadLine();
}
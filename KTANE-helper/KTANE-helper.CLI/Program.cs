using KTANE_helper.Logic;

namespace KTANE_helper.CLI;

internal class Program
{
    static void Main(string[] args)
    {
        new Game(new CLIHandler());

    }
}

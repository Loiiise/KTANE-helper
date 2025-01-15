using KTANE_helper.Logic;

namespace KTANE_helper.CLI;

internal class Program
{
    static void Main(string[] args)
    {
        var game = new Game(new CLIHandler());

        game.Play();
    }
}

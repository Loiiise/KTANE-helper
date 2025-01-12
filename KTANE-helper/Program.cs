using KTANE_helper.IOHandler;

namespace KTANE_helper;

class Program
{
    static void Main(string[] args)
    {
        new Game(new CLIHandler());
    }
}
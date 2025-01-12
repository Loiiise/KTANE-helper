namespace KTANE_helper.Solvers;

internal class MorseCodeSolver : Solvable<MorseCodeSolver>
{
    private List<string> wordOptions;

    private readonly Dictionary<string, string> morseCode = new()
    {
        { ".-"  , "a" },
        { "-...", "b" },
        { "-.-.", "c" },
        { "."   , "e" },
        { "..-.", "f" },
        { "--." , "g" },
        { "....", "h" },
        { ".."  , "i" },
        { "-.-" , "k" },
        { ".-..", "l" },
        { "--"  , "m" },
        { "-."  , "n" },
        { "---" , "o" },
        { ".-." , "r" },
        { "..." , "s" },
        { "-"   , "t" },
        { "...-", "v" },
        { "-..-", "x" },
    };

    private readonly Dictionary<string, double> words = new()
    {
        { "beats", 3.600 },
        { "bistro", 3.552 },
        { "bombs", 3.565 },
        { "boxes", 3.535 },
        { "break", 3.572 },
        { "brick", 3.575 },
        { "flick", 3.555 },
        { "halls", 3.515 },
        { "leaks", 3.542 },
        { "shell", 3.505 },
        { "slick", 3.522 },
        { "steak", 3.582 },
        { "sting", 3.592 },
        { "strobe", 3.545 },
        { "trick", 3.532 },
        { "vector", 3.595 }
    };


    internal override void Solve(BombKnowledge _)
    {
        wordOptions = words.Keys.ToList();

        _ioHandler.ShowLine("Use . for shorts and - or , for longs.");
        string answer;
        while (!ProcessLetter(_ioHandler.Query("What is a letter you see?"), out answer)) ;

        if (answer is not null) _ioHandler.ShowLine($"The correct frequency is {words[answer]} MHz");
        else
        {
            _ioHandler.ShowLine("you f'ed up!");
            Solve(_);
        }
    }

    private bool ProcessLetter(string letter, out string word)
    {
        word = null;

        letter = letter.Replace(',', '-');
        if (!morseCode.Keys.Contains(letter)) return false;

        wordOptions = wordOptions.Where(word => word.Contains(morseCode[letter])).ToList();

        if (!wordOptions.Any()) return true;

        word = wordOptions[0];

        _ioHandler.ShowLine($"There are {wordOptions.Count} left");

        return wordOptions.Count == 1;
    }
}
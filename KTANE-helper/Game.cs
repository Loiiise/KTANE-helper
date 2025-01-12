using KTANE_helper.IOHandler;
using KTANE_helper.NeedyModules;
using KTANE_helper.Solvers;

namespace KTANE_helper;

class Game
{
    BombKnowledge bk = new();
    private List<string> validInputs = new();
    Dictionary<string, InputToken> tokenMap = new();
    readonly Dictionary<InputToken, List<string>> invocations = new()
    {
        {
            InputToken.Quit,
            new()
            {
                "quit",
                "byebye",
                "die",
                "anyone dies with a clean sword, I'll rape his fucking corpse",
                "I'm a fucking marine!",
                "killkillkill",
                "he's got a rocket lau...",
                "abcdefuckoff",
            }
        },
        { InputToken.NextBomb, new() { "n", "next", "fuck this shit I'm out" } },
        { InputToken.Wires, new() { "w", "wir", "wires", "ahhhh, wire" } },
        { InputToken.Button, new() { "b", "but", "button", "boeton", "THE BUTTON!!" } },
        { InputToken.Keypad, new() { "k", "keypad" } },
        { InputToken.SimonSays, new() { "ss", "simon", "simonsays" } },
        { InputToken.WhosOnFirst, new() { "wof", "whosonfirst", "weirdbuttons", "samewords", "words" } },
        { InputToken.Memory, new() { "m", "mem", "memory" } },
        { InputToken.MorseCode, new() { "mc", "morse", "morsecode" } },
        { InputToken.ComplicatedWires, new() { "cw", "complicatedwires" } },
        { InputToken.WireSequences, new() { "ws", "wiresequences", "abc", "abcwires" } },
        { InputToken.Maze, new() { "m", "maze", "maize", "mais", "mees", "mice" } },
        { InputToken.Password, new() { "p", "pw", "password" } },
        { InputToken.NeedyKnob, new() { "nn", "needy", "knobs", "needyknob" } },
    };

    public Game(IIOHandler ioHandler)
    {
        _ioHandler = ioHandler;
        // Flip the script
        foreach ((var token, var values) in invocations)
        {
            validInputs.AddRange(values);
            foreach (var value in values)
                tokenMap[value] = token;
        }

        Play();
    }

    private void Play()
    {
        InputToken token;

        while ((token = NextToken()) is not InputToken.Quit)
        {
            if (token == InputToken.NextBomb)
            {
                _ioHandler.ShowLine("I hear trouble coming... Over and over again");
                bk = new();
                continue;
            }

            SolvePuzzle(token);
        }
    }


    private InputToken NextToken() => tokenMap[_ioHandler.Query("What do you want to do next?", validInputs)];

    private void SolvePuzzle(InputToken puzzle)
    {
        _ioHandler.PromptScopeUp(puzzle.ToString().ToLower());

        switch (puzzle)
        {
            case InputToken.Wires:
                WireSolver.GetInstance(_ioHandler).Solve(bk);
                break;
            case InputToken.Button:
                ButtonSolver.GetInstance(_ioHandler).Solve(bk);
                break;
            case InputToken.Keypad:
                KeypadSolver.GetInstance(_ioHandler).Solve(bk);
                break;
            case InputToken.SimonSays:
                SimonSaysSolver.GetInstance(_ioHandler).Solve(bk);
                break;
            case InputToken.WhosOnFirst:
                WhosOnFirstSolver.GetInstance(_ioHandler).Solve(bk);
                break;
            case InputToken.Memory:
                MemorySolver.GetInstance(_ioHandler).Solve(bk);
                break;
            case InputToken.MorseCode:
                MorseCodeSolver.GetInstance(_ioHandler).Solve(bk);
                break;
            case InputToken.ComplicatedWires:
                ComplicatedWiresSolver.GetInstance(_ioHandler).Solve(bk);
                break;
            case InputToken.WireSequences:
                WireSequenceSolver.GetInstance(_ioHandler).Solve(bk);
                break;
            case InputToken.Maze:
                MazeSolver.GetInstance(_ioHandler).Solve(bk);
                break;
            case InputToken.Password:
                PasswordSolver.GetInstance(_ioHandler).Solve(bk);
                break;
            case InputToken.NeedyKnob:
                NeedyKnob.GetInstance(_ioHandler).Solve(bk);
                break;
            default:
                _ioHandler.ShowLine("This solver is not implemented. You're on your own!");
                break;
        }

        _ioHandler.PromptScopeDown();
    }

    private enum InputToken
    {
        Quit,
        NextBomb,
        Wires,
        Button,
        Keypad,
        SimonSays,
        WhosOnFirst,
        Memory,
        MorseCode,
        ComplicatedWires,
        WireSequences,
        Maze,
        Password,
        NeedyKnob,
    }

    private readonly IIOHandler _ioHandler;
}

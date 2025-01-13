using KTANE_helper.Logic.Solvers;
using System.Collections.Generic;

namespace KTANE_helper.Logic;

public class Game
{
    public Game(IIOHandler ioHandler)
    {
        _ioHandler = ioHandler;
        _bombKnowledge = new(_ioHandler);

        // Flip the script
        foreach ((var token, var values) in _invocations)
        {
            _validInputs.AddRange(values);
            foreach (var value in values)
                _tokenMap[value] = token;
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
                _bombKnowledge = new(_ioHandler);
                continue;
            }

            SolvePuzzle(token);
        }
    }


    private InputToken NextToken() => _tokenMap[_ioHandler.Query("What do you want to do next?", _validInputs)];

    private void SolvePuzzle(InputToken puzzle)
    {
        _ioHandler.PromptScopeUp(puzzle.ToString().ToLower());

        switch (puzzle)
        {
            case InputToken.Wires:
                WireSolver.GetInstance(_ioHandler).Solve(_bombKnowledge);
                break;
            case InputToken.Button:
                ButtonSolver.GetInstance(_ioHandler).Solve(_bombKnowledge);
                break;
            case InputToken.Keypad:
                KeypadSolver.GetInstance(_ioHandler).Solve(_bombKnowledge);
                break;
            case InputToken.SimonSays:
                SimonSaysSolver.GetInstance(_ioHandler).Solve(_bombKnowledge);
                break;
            case InputToken.WhosOnFirst:
                WhosOnFirstSolver.GetInstance(_ioHandler).Solve(_bombKnowledge);
                break;
            case InputToken.Memory:
                MemorySolver.GetInstance(_ioHandler).Solve(_bombKnowledge);
                break;
            case InputToken.MorseCode:
                MorseCodeSolver.GetInstance(_ioHandler).Solve(_bombKnowledge);
                break;
            case InputToken.ComplicatedWires:
                ComplicatedWiresSolver.GetInstance(_ioHandler).Solve(_bombKnowledge);
                break;
            case InputToken.WireSequences:
                WireSequenceSolver.GetInstance(_ioHandler).Solve(_bombKnowledge);
                break;
            case InputToken.Maze:
                MazeSolver.GetInstance(_ioHandler).Solve(_bombKnowledge);
                break;
            case InputToken.Password:
                PasswordSolver.GetInstance(_ioHandler).Solve(_bombKnowledge);
                break;
            case InputToken.NeedyKnob:
                NeedyKnob.GetInstance(_ioHandler).Solve(_bombKnowledge);
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

    private BombKnowledge _bombKnowledge;
    private List<string> _validInputs = new();
    private Dictionary<string, InputToken> _tokenMap = new();

    private readonly IIOHandler _ioHandler;
    private readonly Dictionary<InputToken, List<string>> _invocations = new()
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
}

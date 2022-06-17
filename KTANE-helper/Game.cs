using KTANE_helper.Solvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KTANE_helper.IOHandler;

namespace KTANE_helper
{
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
            { InputToken.Wires, new() { "w", "wir", "wires" } },
            { InputToken.Button, new() { "b", "but", "button", "boeton", "THE BUTTON!!" } },
            { InputToken.Keypad, new() { "k", "keypad" } },
            { InputToken.SimonSays, new() { "simon", "simonsays" } },
            { InputToken.Memory, new() { "m", "mem", "memory" } },
            { InputToken.Password, new() { "p", "pw", "password" } },
            
        };

        public Game()
        {
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
                    Show("I hear trouble coming... Over and over again");
                    bk = new();
                    continue;
                }

                SolvePuzzle(token);
            }
        }


        private InputToken NextToken() => tokenMap[Query("What do you want to do next?", validInputs)];

        private void SolvePuzzle(InputToken puzzle)
        {
            PromptScopeUp(puzzle.ToString().ToLower());

            switch (puzzle)
            {
                case InputToken.Wires:
                    WireSolver.GetInstance().Solve(bk); 
                    break;
                case InputToken.Button:
                    ButtonSolver.GetInstance().Solve(bk); 
                    break;
                case InputToken.Keypad:
                    KeypadSolver.GetInstance().Solve(bk); 
                    break;
                case InputToken.SimonSays:
                    SimonSaysSolver.GetInstance().Solve(bk); 
                    break;
                case InputToken.Memory:
                    MemorySolver.GetInstance().Solve(bk); 
                    break;
                case InputToken.Password:
                    PasswordSolver.GetInstance().Solve(bk); 
                    break;
                default:
                    Show("This solver is not implemented. You're on your own!");
                    break;
            }

            PromptScopeDown();
        }

        private enum InputToken
        {
            Quit, NextBomb, Wires, Button, Keypad, SimonSays, Memory, Password
        }
    }
}

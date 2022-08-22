﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTANE_helper.Solvers
{
    internal class MorseCodeSolver : Solvable<MorseCodeSolver>
    {
        private readonly Dictionary<string, double> possibilities = new()
        {
            { "halls", 3.515 },
            { "shell", 3.505 },
            { "slick", 3.522 },
            { "trick", 3.532 },
            { "boces", 3.535 },
            { "leaks", 3.542 },
            { "strobe", 3.545 },
            { "bistro", 3.552 },
            { "flick", 3.555 },
            { "bombs", 3.565 },
            { "break", 3.572 },
            { "brick", 3.575 },
            { "steak", 3.582 },
            { "sting", 3.592 },
            { "vector", 3.595 },
            { "beats", 3.600 },
        };

        internal override void Solve(BombKnowledge bk)
        {
            
        }
    }
}

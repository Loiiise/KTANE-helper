using KTANE_helper.Solvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KTANE_helper.IOHandler;

namespace KTANE_helper
{
    internal class PasswordSolver : Solvable<PasswordSolver>
    {
        internal override void Solve(BombKnowledge bk)
        {
            var passwordOptions = new List<string> { "about", "after", "again", "below", "could", "every", "first", "found", "great", "house", "large", "learn", "never", "other", "place", "plant", "point", "right", "small", "sound", "spell", "still", "study", "their", "there", "these", "thing", "think", "three", "water", "where", "which", "world", "would", "write" };

            int currentLetter = 0;
            bool found = false;

            while (passwordOptions.Count() > 1)
            {
                var nextLetterOptions = new HashSet<string>(
                    passwordOptions.Select(pw => pw[currentLetter].ToString())
                    );

                var currentLetterOptionsFound = new List<string>();

                if (nextLetterOptions.Count() > 1)
                {
                    var msg = $"{passwordOptions.Count()} options left! What are the options for the {PositionWord(currentLetter + 1)} letter?";

                    foreach (var letter in QueryMultiple(msg, n: 6))
                    {
                        if (!nextLetterOptions.Contains(letter)) continue;

                        currentLetterOptionsFound.Add(letter);
                        nextLetterOptions.Remove(letter);

                        if (!nextLetterOptions.Any()) break;
                    }
                }
                else
                {
                    Show($"Skipping the {PositionWord(currentLetter + 1)} letter!");
                    currentLetterOptionsFound.Add(nextLetterOptions.First());
                }

                passwordOptions = passwordOptions.Where(s => currentLetterOptionsFound.Contains(s[currentLetter].ToString())).ToList();

                ++currentLetter;
            }
            if (passwordOptions.Count() == 0)
            {
                Console.WriteLine("You messed up!");
                return;
            }
            Console.WriteLine($"Password is {passwordOptions.First()}");
        }
    }
}

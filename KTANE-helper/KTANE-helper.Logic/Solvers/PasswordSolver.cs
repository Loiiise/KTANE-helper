using KTANE_helper.Logic.IO;
using System.Collections.Generic;
using System.Linq;

namespace KTANE_helper.Logic.Solvers;

public class PasswordSolver : Solvable<PasswordSolver>
{
    public override void Solve(BombKnowledge bk)
    {
        var passwordOptions = new List<string> { "about", "after", "again", "below", "could", "every", "first", "found", "great", "house", "large", "learn", "never", "other", "place", "plant", "point", "right", "small", "sound", "spell", "still", "study", "their", "there", "these", "thing", "think", "three", "water", "where", "which", "world", "would", "write" };

        int currentLetter = 0;

        while (passwordOptions.Count() > 1)
        {
            var nextLetterOptions = new HashSet<string>(
                passwordOptions.Select(pw => pw[currentLetter].ToString())
                );

            var currentLetterOptionsFound = new List<string>();

            if (nextLetterOptions.Count() > 1)
            {
                var msg = $"{passwordOptions.Count()} options left! What are the options for the {(currentLetter + 1).PositionWord()} letter?";

                foreach (var letter in _ioHandler.QueryMultiple(msg, n: 6))
                {
                    if (!nextLetterOptions.Contains(letter)) continue;

                    currentLetterOptionsFound.Add(letter);
                    nextLetterOptions.Remove(letter);

                    if (!nextLetterOptions.Any()) break;
                }
            }
            else
            {
                _ioHandler.ShowLine($"Skipping the {(currentLetter + 1).PositionWord()} letter!");
                currentLetterOptionsFound.Add(nextLetterOptions.First());
            }

            passwordOptions = passwordOptions.Where(s => currentLetterOptionsFound.Contains(s[currentLetter].ToString())).ToList();

            ++currentLetter;
        }
        if (passwordOptions.Count() == 0)
        {
            _ioHandler.ShowLine("You messed up!");
            return;
        }

        _ioHandler.Answer(new PasswordAnswer { Value = passwordOptions.First() });
    }
}

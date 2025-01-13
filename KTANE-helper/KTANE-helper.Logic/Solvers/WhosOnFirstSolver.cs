using System;
using System.Collections.Generic;
using System.Linq;

namespace KTANE_helper.Logic.Solvers;

public class WhosOnFirstSolver : Solvable<WhosOnFirstSolver>
{
    public override void Solve(BombKnowledge bk)
    {
        for (int i = 1; i <= 3; ++i)
        {
            _ioHandler.PromptScopeAdd($"{i}/3");
            var label = Step1();
            Step2(label);
            _ioHandler.PromptScopeDown();
        }
    }

    private Word Step1()
    {
        string firstWord = _ioHandler.Query("What is the word on the display?", allWordsStrings);

        var buttonPosition = (GetWordFromString(firstWord)) switch
        {
            Word.UR => "top left",
            Word.FIRST or
            Word.OKAY or
            Word.C => "top right",
            Word.YES or
            Word.NOTHING or
            Word.LED or
            Word.THEYARE => "middle left",
            Word.BLANK or
            Word.READ or
            Word.RED or
            Word.YOU or
            Word.YOUR or
            Word.YOURE or
            Word.THEIR => "middle right",
            Word.EmptyString or
            Word.REED or
            Word.LEED or
            Word.THEYRE => "bottom left",
            Word.DISPLAY or
            Word.SAYS or
            Word.NO or
            Word.LEAD or
            Word.HOLDON or
            Word.YOUARE or
            Word.THERE or
            Word.SEE or
            Word.CEE => "bottom right",
            _ => throw new ArgumentException("This word is not supported in this context in the manual."),
        };

        var label = _ioHandler.Query($"What is the label of the {buttonPosition} button", allWordsStrings);
        return GetWordFromString(label);
    }

    private void Step2(Word inputWord)
    {
        Dictionary<Word, List<Word>> wordMap = new()
        {
            { Word.READY, new() { Word.YES, Word.OKAY, Word.MIDDLE, Word.LEFT, Word.PRESS, Word.RIGHT, Word.BLANK, Word.READY } },
            { Word.FIRST, new() { Word.LEFT, Word.OKAY, Word.YES, Word.MIDDLE, Word.NO, Word.RIGHT, Word.NOTHING, Word.UHHH, Word.WAIT, Word.READY, Word.BLANK, Word.WHAT, Word.PRESS, Word.FIRST } },
            { Word.NO, new() { Word.BLANK, Word.UHHH, Word.WAIT, Word.FIRST, Word.WHAT, Word.READY, Word.RIGHT, Word.YES, Word.NOTHING, Word.LEFT, Word.PRESS, Word.OKAY, Word.NO } },
            { Word.BLANK, new() { Word.WAIT, Word.RIGHT, Word.OKAY, Word.MIDDLE, Word.BLANK } },
            { Word.NOTHING, new() { Word.UHHH, Word.RIGHT, Word.OKAY, Word.MIDDLE, Word.YES, Word.BLANK, Word.NO, Word.PRESS, Word.LEFT, Word.WHAT, Word.WAIT, Word.FIRST, Word.NOTHING } },
            { Word.YES, new() { Word.OKAY, Word.RIGHT, Word.UHHH, Word.MIDDLE, Word.FIRST, Word.WHAT, Word.PRESS, Word.READY, Word.NOTHING, Word.YES } },
            { Word.WHAT, new() { Word.UHHH, Word.WHAT } },
            { Word.UHHH, new() { Word.READY, Word.NOTHING, Word.LEFT, Word.WHAT, Word.OKAY, Word.YES, Word.RIGHT, Word.NO, Word.PRESS, Word.BLANK, Word.UHHH } },
            { Word.LEFT, new() { Word.RIGHT, Word.LEFT, Word.FIRST, Word.NO, Word.MIDDLE, Word.YES, Word.BLANK, Word.WHAT, Word.UHHH, Word.WAIT, Word.PRESS, Word.READY, Word.OKAY, Word.NOTHING } },
            { Word.RIGHT, new() { Word.YES, Word.NOTHING, Word.READY, Word.PRESS, Word.NO, Word.WAIT, Word.WHAT, Word.RIGHT } },
            { Word.MIDDLE, new() { Word.BLANK, Word.READY, Word.OKAY, Word.WHAT, Word.NOTHING, Word.PRESS, Word.NO, Word.WAIT, Word.LEFT, Word.MIDDLE } },
            { Word.OKAY, new() { Word.MIDDLE, Word.NO, Word.FIRST, Word.YES, Word.UHHH, Word.NOTHING, Word.WAIT, Word.OKAY } },
            { Word.WAIT, new() { Word.UHHH, Word.NO, Word.BLANK, Word.OKAY, Word.YES, Word.LEFT, Word.FIRST, Word.PRESS, Word.WHAT, Word.WAIT } },
            { Word.PRESS, new() { Word.RIGHT, Word.MIDDLE, Word.YES, Word.READY, Word.PRESS } },
            { Word.YOU, new() { Word.SURE, Word.YOU } },
            { Word.YOUARE, new() { Word.YOUR, Word.NEXT, Word.LIKE, Word.UHHUH, Word.WHATQ, Word.DONE, Word.UHUH, Word.HOLD, Word.YOU, Word.U, Word.YOURE, Word.SURE, Word.UR, Word.YOUARE } },
            { Word.YOUR, new() { Word.UHUH, Word.YOUARE, Word.UHHUH, Word.YOUR } },
            { Word.YOURE, new() { Word.YOU, Word.YOURE } },
            { Word.UR, new() { Word.DONE, Word.U, Word.UR } },
            { Word.U, new() { Word.UHHUH, Word.SURE, Word.NEXT, Word.WHATQ, Word.YOURE, Word.UR, Word.UHUH, Word.DONE, Word.U } },
            { Word.UHHUH, new() { Word.UHHUH } },
            { Word.UHUH, new() { Word.UR, Word.U, Word.YOUARE, Word.YOURE, Word.NEXT, Word.UHUH } },
            { Word.WHATQ, new() { Word.YOU, Word.HOLD, Word.YOURE, Word.YOUR, Word.U, Word.DONE, Word.UHUH, Word.LIKE, Word.YOUARE, Word.UHHUH, Word.UR, Word.NEXT, Word.WHATQ } },
            { Word.DONE, new() { Word.SURE, Word.UHHUH, Word.NEXT, Word.WHATQ, Word.YOUR, Word.UR, Word.YOURE, Word.HOLD, Word.LIKE, Word.YOU, Word.U, Word.YOUARE, Word.UHUH, Word.DONE } },
            { Word.NEXT, new() { Word.WHATQ, Word.UHHUH, Word.UHUH, Word.YOUR, Word.HOLD, Word.SURE, Word.NEXT } },
            { Word.HOLD, new() { Word.YOUARE, Word.U, Word.DONE, Word.UHUH, Word.YOU, Word.UR, Word.SURE, Word.WHATQ, Word.YOURE, Word.NEXT, Word.HOLD } },
            { Word.SURE, new() { Word.YOUARE, Word.DONE, Word.LIKE, Word.YOURE, Word.YOU, Word.HOLD, Word.UHHUH, Word.UR, Word.SURE } },
            { Word.LIKE, new() { Word.YOURE, Word.NEXT, Word.U, Word.UR, Word.HOLD, Word.DONE, Word.UHUH, Word.WHATQ, Word.UHHUH, Word.YOU, Word.LIKE } },
        };

        _ioHandler.ShowLine("The first appearing word in the following list is the button that should be pressed.");

        foreach (var result in wordMap[inputWord]) _ioHandler.ShowLine(GetWordString(result));
    }

    private static string GetWordString(Word word) => word switch
    {
        Word.EmptyString => "",
        Word.YOURE => "YOU'RE",
        Word.THEYRE => "THEY'RE",
        Word.WHATQ => "WHAT?",
        Word.UHUH => "UH UH",
        Word.UHHUH => "UH HUH",
        _ => word.ToString().ToUpper(),
    };


    private static Word GetWordFromString(string stringSymbol) => allWords.First(w => GetWordString(w) == stringSymbol.ToUpper());

    private static IEnumerable<Word> allWords = Enum.GetValues(typeof(Word)).Cast<Word>();
    private static IEnumerable<string> allWordsStrings = allWords.Select(w => GetWordString(w));
    private enum Word
    {
        EmptyString,

        BLANK,
        C,
        CEE,
        DONE,
        DISPLAY,
        FIRST,
        HOLD,
        HOLDON,
        LED,
        LEAD,
        LEED,
        LEFT,
        LIKE,
        MIDDLE,
        NEXT,
        NO, // but damn close
        NOTHING,
        OKAY,
        PRESS,
        RED,
        READ,
        REED,
        READY,
        RIGHT,
        SAYS,
        SEE,
        SURE,
        THEIR,
        THERE,
        THEYARE,
        THEYRE,
        U,
        UHHH,
        UHHUH,
        UHUH,
        UR,
        WAIT,
        WHAT,
        WHATQ,
        YES,
        YOU,
        YOUARE,
        YOUR,
        YOURE,
    };
}
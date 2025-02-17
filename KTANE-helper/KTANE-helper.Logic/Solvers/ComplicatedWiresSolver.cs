﻿using KTANE_helper.Logic.IO;

namespace KTANE_helper.Logic.Solvers;

public class ComplicatedWiresSolver : Solvable<ComplicatedWiresSolver>
{
    public override void Solve(BombKnowledge bk)
    {
        while (true)
        {
            var userInput = _ioHandler.Query("What is the state of the wire? (R for red, B for blue, S for star and L for LED).").ToUpper();

            if (userInput.Length > 4 ||
                userInput.HasIllegalCharacters('R', 'B', 'S', 'L', 'W'))
            {
                break;
            }

            var state = WireProperties.None;

            AddPropertyIfCharacterPresent(ref state, userInput, 'R', WireProperties.Red);
            AddPropertyIfCharacterPresent(ref state, userInput, 'B', WireProperties.Blue);
            AddPropertyIfCharacterPresent(ref state, userInput, 'S', WireProperties.Star);
            AddPropertyIfCharacterPresent(ref state, userInput, 'L', WireProperties.Led);

            var instruction = GetInstructionFromState(state);

            _ioHandler.Answer(new ComplicatedWiresAnswer { Value = ShouldICut(instruction, bk) });
        }
    }

    private void AddPropertyIfCharacterPresent(ref WireProperties state, string userInput, char character, WireProperties propertyToAdd)
    {
        if (userInput.Contains(character))
        {
            state |= propertyToAdd;
        }
    }

    private Instruction GetInstructionFromState(WireProperties state) => _instructionMap[(int)state];

    private readonly Instruction[] _instructionMap = new Instruction[]
    {
        Instruction.Cut,                 // ....
        Instruction.SerialRelated,       // ...R
        Instruction.SerialRelated,       // ..B.
        Instruction.SerialRelated,       // ..BR
        Instruction.Cut,                 // .S..
        Instruction.Cut,                 // .S.R
        Instruction.DoNotCut,            // .SB.
        Instruction.ParallelPortRelated, // .SBR
        Instruction.DoNotCut,            // L...
        Instruction.BatteryRelated,      // L..R
        Instruction.ParallelPortRelated, // L.B.
        Instruction.SerialRelated,       // L.BR
        Instruction.BatteryRelated,      // LS..
        Instruction.BatteryRelated,      // LS.R
        Instruction.ParallelPortRelated, // LSB.
        Instruction.DoNotCut,            // LSBR
    };

    private bool ShouldICut(Instruction instruction, BombKnowledge bk) => instruction switch
    {
        Instruction.Cut => true,
        Instruction.DoNotCut => false,
        Instruction.SerialRelated => bk.SerialNumberLastDigitEven(),
        Instruction.ParallelPortRelated => bk.HasParallelPort(),
        Instruction.BatteryRelated => bk.Batteries() >= 2,
        _ => throw new ArgumentOutOfRangeException(nameof(instruction)),
    };
}

[Flags]
enum WireProperties
{
    None = 0,
    Red = 1 << 0,
    Blue = 1 << 1,
    Star = 1 << 2,
    Led = 1 << 3,
}

enum Instruction
{
    Cut,
    DoNotCut,
    SerialRelated,
    ParallelPortRelated,
    BatteryRelated,
}

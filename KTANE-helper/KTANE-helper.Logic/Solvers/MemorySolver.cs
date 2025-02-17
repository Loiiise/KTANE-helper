﻿using KTANE_helper.Logic.IO;

namespace KTANE_helper.Logic.Solvers;

public class MemorySolver : Solvable<MemorySolver>
{
    public override void Solve(BombKnowledge bk)
    {
        const int stages = 5;
        var labels = new int[stages];
        var positions = new int[stages];

        for (int stage = 1; stage <= stages; ++stage)
        {
            int display = _ioHandler.IntQuery("What does the display say?", new int[] { 1, 2, 3, 4 });

            switch (stage)
            {
                case 1:
                    int position = (display) switch
                    {
                        1 => 2,
                        2 => 2,
                        3 => 3,
                        4 => 4,
                        _ => throw new ArgumentOutOfRangeException(nameof(display)),
                    };

                    labels[stage] = Position(position);
                    positions[stage] = position;
                    break;
                case 2:
                    switch (display)
                    {
                        case 1:
                            positions[stage] = Label(4);
                            labels[stage] = 4;
                            break;
                        case 2:
                        case 4:
                            labels[stage] = PositionStage(1);
                            positions[stage] = positions[1];
                            break;
                        case 3:
                            labels[stage] = Position(1);
                            positions[stage] = 1;
                            break;
                    }
                    break;
                case 3:
                    switch (display)
                    {
                        case 1:
                            positions[stage] = LabelStage(2);
                            labels[stage] = labels[2];
                            break;
                        case 2:
                            positions[stage] = LabelStage(1);
                            labels[stage] = labels[1];
                            break;
                        case 3:
                            labels[stage] = Position(3);
                            positions[stage] = 3;
                            break;
                        case 4:
                            positions[stage] = Label(4);
                            labels[stage] = 4;
                            break;
                    }
                    break;
                case 4:
                    switch (display)
                    {
                        case 1:
                            labels[stage] = PositionStage(1);
                            positions[stage] = positions[1];
                            break;
                        case 2:
                            labels[stage] = Position(1);
                            positions[stage] = 1;
                            break;
                        case 3:
                        case 4:
                            labels[stage] = PositionStage(2);
                            positions[stage] = positions[2];
                            break;
                    }
                    break;
                case 5:
                    _ = display switch
                    {
                        1 => LabelStage(1, ask: false),
                        2 => LabelStage(2, ask: false),
                        3 => LabelStage(4, ask: false),
                        4 => LabelStage(3, ask: false),
                        _ => throw new ArgumentOutOfRangeException(nameof(display)),
                    };

                    _ioHandler.ShowLine("Hurrah! Thou hast donest itst!");
                    return;
            }
        }

        int Position(int position, bool ask = true)
        {
            _ioHandler.Answer(new MemoryAnswer { Value = new MemoryAnswerValue(MemoryPositionOrLabel.Position, position) });
            return ask ? What("label") : -1;
        }
        int PositionStage(int stage, bool ask = true) => Position(positions[stage], ask);

        int Label(int label, bool ask = true)
        {
            _ioHandler.Answer(new MemoryAnswer { Value = new MemoryAnswerValue(MemoryPositionOrLabel.Label, label) });
            return ask ? What("position") : -1;
        }
        int LabelStage(int stage, bool ask = true) => Label(labels[stage], ask);

        int What(string thing)
        {
            return _ioHandler.IntQuery($"What {thing} was this?", new int[] { 1, 2, 3, 4 });
        }

    }
}

public enum MemoryPositionOrLabel { Position, Label }

using System;
using System.Collections.Generic;
using System.Linq;
using static KTANE_helper.IOHandler;

namespace KTANE_helper
{
    class Program
    {
        static char Puzzle;
        static void Main(string[] args)
        {
            while (true)
            {
                switch (Query("What do you want to do next?"))
                {
                    case "w":
                    case "wir":
                    case "wires":
                        SetPromptScope("w");
                        Wires();
                        break;
                    case "m":
                    case "mem":
                    case "memory":
                        SetPromptScope("m");
                        Memory();
                        break;
                    case "p":
                    case "pw":
                    case "password":
                        SetPromptScope("p");
                        Password();
                        break;
                    case "quit":
                    case "byebye":
                    case "die":
                    case "anyone dies with a clean sword, I'll rape his fucking corpse":
                        return;
                    default: break;
                }
                ResetPromptScope();
            }
        }

        static void Wires()
        {
            string wires = Query("What wires are present?");

            int amount = wires.Length;
            int lastWire = amount - 1;

            int redWires = wires.Where(w => w == 'r').Count();
            int whiteWires = wires.Where(w => w == 'w').Count();
            int blueWires = wires.Where(w => w == 'b').Count();
            int blackWires = wires.Where(w => w == 'z').Count();
            int yellowWires = wires.Where(w => w == 'g').Count();

            char lastWireColour = wires[lastWire];

            switch (amount)
            {
                case 3:
                    if (redWires == 0) Cut(2);
                    else if (wires.Last() == 'w') Cut(lastWire);
                    else if (blueWires > 1) CutLast('b');
                    else Cut(lastWire);
                    return;
                case 4:
                    if (redWires > 1 && SerialNumberLastIsOdd()) CutLast('r');
                    else if (wires.Last() == 'g' && redWires == 0 || blueWires == 1) Cut(1);
                    else if (yellowWires > 1) Cut(lastWire);
                    else Cut(2);
                    return;
                case 5:
                    if (lastWireColour == 'b' && SerialNumberLastIsOdd()) Cut(4);
                    else if (redWires == 1 && yellowWires <= 1) Cut(1);
                    else if (blackWires == 0) Cut(2);
                    else Cut(1);
                    return;
                case 6:
                    if (yellowWires == 0 && SerialNumberLastIsOdd()) Cut(3);
                    else if (yellowWires == 1 && whiteWires > 1) Cut(4);
                    else if (redWires == 0) Cut(lastWire);
                    else Cut(4);
                    return;
                default:
                    Console.WriteLine(".... idiot");
                    Wires();
                    return;
            }
            
            void Cut(int wire)
            {
                Console.WriteLine($"Cut the {PositionWord(wire)} wire");
            }
            void CutLast(char colour) => Cut(LastIndex(colour));

            int LastIndex(char colour)
            {
                for (int i = lastWire; i > 0; --i)
                    if (wires[i] == colour)
                        return i;
                return 0;
            }
        }

        static void Memory()
        {
            const int stages = 5;
            var labels = new int[stages];
            var positions = new int[stages];

            for (int stage = 1; stage <= stages; ++stage)
            {
                int display = IntQuery("What does the display say?", new int[] { 1,2,3,4 });

                switch (stage)
                {
                    case 1:
                        int position = (display) switch
                        {
                            1 => 2,
                            2 => 2,
                            3 => 3,
                            4 => 4,
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
                        };

                        Console.WriteLine("Hurrah! Thou hast donest itst!");
                        return;
                }
            }

            int Position(int position, bool ask = true)
            {
                Console.WriteLine($"Press the button in the {PositionWord(position)} position");
                return ask? What("label") : -1;
            }
            int PositionStage(int stage, bool ask = true) => Position(positions[stage], ask);

            int Label(int label, bool ask = true)
            {
                Console.WriteLine($"Press the button labeled \"{label}\"");
                return ask? What("position") : -1;
            }
            int LabelStage(int stage, bool ask = true) => Label(labels[stage], ask);

            int What(string thing)
            {
                return IntQuery($"What {thing} was this?", new int[] { 1, 2, 3, 4 });
            }

        }

        static void Password()
        {
            var options = new List<string> { "about", "after", "again", "below", "could", "every", "first", "found", "great", "house", "large", "learn", "never", "other", "place", "plant", "point", "right", "small", "sound", "spell", "still", "study", "their", "there", "these", "thing", "think", "three", "water", "where", "which", "world", "would", "write"};

            int currentLetter = 0;

            while (options.Count() > 1)
            {
                Console.WriteLine($"{options.Count()} options left! Give all 6 options for the next letter.");
                var letters = GetLines(6).ToList();
                options = options.Where(s => letters.Contains(s[currentLetter].ToString())).ToList();

                ++currentLetter;
            }
            if (options.Count() == 0)
            {
                Console.WriteLine("You messed up!");
                return;
            }
            Console.WriteLine($"Password is {options.First()}");
        }
        
        // questionz?
        static bool SerialNumberLastIsEven()
        {
            int digit = IntQuery("What is the last digit of the serial number?");
            return digit % 2 == 0;
        }

        static bool SerialNumberLastIsOdd() => !SerialNumberLastIsEven();
    }
}

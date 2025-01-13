using KTANE_helper.Logic.Solvers;

namespace KTANE_helper.Logic.IO;

public abstract record Answer<T> : IIOToken
{
    public required T Value { get; init; }
}

public enum ButtonReleaseOrHold { ReleaseImmediatly, Hold, ReleaseWhen }
public sealed record ButtonAnswerValue(ButtonReleaseOrHold ReleaseOrHold, int? When);
public sealed record ButtonAnswer : Answer<ButtonAnswerValue> { }

public sealed record ComplicatedWiresAnswer : Answer<bool> { }

public sealed record KeypadAnswer : Answer<KeypadSymbol[]> { }

public enum MazeDirection { Left, Right, Up, Down }
public sealed record MazeAnswer : Answer<MazeDirection[]> { }

public enum MemoryPositionOrLabel { Position, Label }
public sealed record MemoryAnswerValue(MemoryPositionOrLabel PositionOrLabel, int Value);
public sealed record MemoryAnswer : Answer<MemoryAnswerValue> { }

public sealed record MorseCodeAnswer : Answer<double> { }

public sealed record PasswordAnswer : Answer<string> { }

public sealed record SimonSaysAnswer : Answer<SimonSaysColour[]> { }

public sealed record WhosOnFirstAnswer : Answer<string[]> { }

public sealed record WireSequenceAnswer : Answer<int> 
{ 
    public WireSequenceAnswer() 
    {
        throw new NotImplementedException();
    }
}

public sealed record WireAnswer : Answer<int> { }

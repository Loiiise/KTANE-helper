using KTANE_helper.Logic.Solvers;

namespace KTANE_helper.Logic.IO;

public abstract record Answer<T> : IOToken
{
    public required T Value { get; init; }
}

/// <summary>
/// What the user should do with the button and, in case of <a cref="ButtonReleaseOrHold.ReleaseWhen"/>, when they should do it.
/// </summary>
/// <param name="ReleaseOrHold">Action to perform</param>
/// <param name="When">The second on which it should be released in case of <a cref="ButtonReleaseOrHold.ReleaseWhen"/></param>
public sealed record ButtonAnswerValue(ButtonReleaseOrHold ReleaseOrHold, int? When);
public sealed record ButtonAnswer : Answer<ButtonAnswerValue>;

/// <summary>
/// Whether or not to cut the wire.
/// </summary>
public sealed record ComplicatedWiresAnswer : Answer<bool>;

/// <summary>
/// The sequence in which the <a cref="KeypadSymbol"/>s should be pressed.
/// </summary>
public sealed record KeypadAnswer : Answer<IOTypes.KeypadSymbol[]>;

/// <summary>
/// A sequence of steps for the player to reach the destination.
/// </summary>
public sealed record MazeAnswer : Answer<MazeDirection[]>; 

/// <summary>
/// The x'th position or label which should be pressed. 
/// </summary>
/// <param name="PositionOrLabel">Self explanatory</param>
/// <param name="Value">The x'th postition or label to press, dependant on <a cref="MemoryPositionOrLabel.ReleaseWhen"/></param>
public sealed record MemoryAnswerValue(MemoryPositionOrLabel PositionOrLabel, int Value);
public sealed record MemoryAnswer : Answer<MemoryAnswerValue>;

/// <summary>
/// The frequency.
/// </summary>
public sealed record MorseCodeAnswer : Answer<double>;

/// <summary>
/// The password.
/// </summary>
public sealed record PasswordAnswer : Answer<string>;

/// <summary>
/// The sequence of buttons which should be pressed.
/// </summary>
public sealed record SimonSaysAnswer : Answer<SimonSaysColour[]>;

/// <summary>
/// The order of occurrence. First one that is visible should be cut.
/// </summary>
public sealed record WhosOnFirstAnswer : Answer<string[]>;

/// <summary>
/// Which wires should be cut.
/// </summary>
public sealed record WireSequenceAnswerValue(WireSequenceAnswerState WireSequenceAnswerState, int[] WiresToCutIfCustom);
public sealed record WireSequenceAnswer : Answer<WireSequenceAnswerValue>;

/// <summary>
/// The x'th wire that should be cut.
/// </summary>
public sealed record WireAnswer : Answer<int>;

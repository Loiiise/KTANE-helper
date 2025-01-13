namespace KTANE_helper.Logic.IO;

public abstract record Answer<T> : IIOToken
{
    public required T Value { get; init; }
}

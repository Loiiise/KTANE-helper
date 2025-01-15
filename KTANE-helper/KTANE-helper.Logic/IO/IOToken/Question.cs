namespace KTANE_helper.Logic.IO;

public abstract record Question<T> : IOToken
{
    public required T Value { get; init; }
}

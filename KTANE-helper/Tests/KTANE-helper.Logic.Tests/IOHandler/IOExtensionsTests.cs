using KTANE_helper.Logic.IO;
using Shouldly;

namespace KTANE_helper.Logic.Tests;

public class IOExtensionsTests
{
    public static IEnumerable<string> GetTestStrings
        => ["", "not empty"];

    [Theory, CombinatorialData]
    public void PluraliseReturnsAPluralWordWhenTheCountIsZeroOrGreaterThanOne(
        [CombinatorialValues(int.MinValue, -2, 0, 2, int.MaxValue)] int count,
        [CombinatorialMemberData(nameof(GetTestStrings))] string str)
    {
        str.Pluralise(count).ShouldBe(str + "s");
    }

    [Theory, CombinatorialData]
    public void PluraliseReturnsTheInputWordWhenTheCountIsMinusOneOrOne(
        [CombinatorialValues(-1, 1)] int count,
        [CombinatorialMemberData(nameof(GetTestStrings))] string str)
    {
        str.Pluralise(count).ShouldBe(str);
    }

    [Theory, CombinatorialData]
    public void PluraliseReturnsAPluralWordWithAnEmptyCollectionOrACollectionOfSizeGreaterThanOne(
        [CombinatorialValues(new int[] { }, new int[] { 1, 2 })] IEnumerable<int> collection,
        [CombinatorialMemberData(nameof(GetTestStrings))] string str)
    {
        str.Pluralise(collection).ShouldBe(str + "s");
    }

    [Theory, CombinatorialData]
    public void PluraliseReturnsTheInputWordWithACollectionOfSizeOne(
        [CombinatorialValues(new int[] { 1 })] IEnumerable<int> collection,
        [CombinatorialMemberData(nameof(GetTestStrings))] string str)
    {
        str.Pluralise(collection).ShouldBe(str);
    }

    [Theory, CombinatorialData]
    public void PluraliseThrowsArgumentNullExceptionWhenTheCollectionIsNull<T>(
        [CombinatorialValues(null)] IEnumerable<T> collection,
        [CombinatorialMemberData(nameof(GetTestStrings))] string str)
    {
        Should.Throw<ArgumentNullException>(() => str.Pluralise(collection));
    }

    [Theory]
    [InlineData(new int[] { }, "")]
    [InlineData(new int[] { 1 }, "1")]
    [InlineData(new int[] { 1, 2 }, "1 and 2")]
    [InlineData(new int[] { 1, 2, 3 }, "1, 2 and 3")]
    [InlineData(new int[] { 1, 2, 3, 4 }, "1, 2, 3 and 4")]
    [InlineData(new string[] { "a", "b", "c", "d" }, "a, b, c and d")]
    public void ShowSequenceFormatsCollectionsOfBasicTypesCorrectly<T>(T[] collection, string expectedResult)
    {
        collection.ShowSequence().ShouldBe(expectedResult);
    }

    [Fact]
    public void ShowSequenceHandlesComplexTypesGracefully()
    {
        var collection = new List<List<int>>
        {
            new() { 1, 2 },
            new() { 3 },
            new() { 4, 5, 6 }
        };

        collection.ShowSequence().ShouldBe("System.Collections.Generic.List`1[System.Int32], System.Collections.Generic.List`1[System.Int32] and System.Collections.Generic.List`1[System.Int32]");
    }

    [Fact]
    public void ShowSequenceHandlesNullValuesGracefully()
    {
        var collection = new List<int?> { 1, 2, null, 4, 5 };

        collection.ShowSequence().ShouldBe("1, 2, ???, 4 and 5");
    }
}

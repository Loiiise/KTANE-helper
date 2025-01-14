using Shouldly;

namespace KTANE_helper.Logic.Tests;

public class IOExtensionsTests
{
    public static IEnumerable<string> GetTestStrings
        => ["", "not empty"];

    [Theory, CombinatorialData]
    public void TestPluraliseUsingCount_PluralCase(
        [CombinatorialValues(int.MinValue, -2, 0, 2, int.MaxValue)] int count,
        [CombinatorialMemberData(nameof(GetTestStrings))] string str)
    {
        str.Pluralise(count).ShouldBe(str + "s");
    }

    [Theory, CombinatorialData]
    public void TestPluraliseUsingCount_NonPluralCase(
        [CombinatorialValues(-1, 1)] int count,
        [CombinatorialMemberData(nameof(GetTestStrings))] string str)
    {
        str.Pluralise(count).ShouldBe(str);
    }

    [Theory, CombinatorialData]
    public void TestPluraliseUsingCollection_PluralCase(
        [CombinatorialValues(new int[] { }, new int[] { 1, 2 })] IEnumerable<int> collection,
        [CombinatorialMemberData(nameof(GetTestStrings))] string str)
    {
        str.Pluralise(collection).ShouldBe(str + "s");
    }

    [Theory, CombinatorialData]
    public void TestPluraliseUsingCollection_NonPluralCase(
        [CombinatorialValues(new int[] { 1 })] IEnumerable<int> collection,
        [CombinatorialMemberData(nameof(GetTestStrings))] string str)
    {
        str.Pluralise(collection).ShouldBe(str);
    }

    [Theory, CombinatorialData]
    public void TestPluraliseUsingCollection_NullCollectionCase<T>(
        [CombinatorialValues(null)] IEnumerable<T> collection,
        [CombinatorialMemberData(nameof(GetTestStrings))] string str)
    {
        Should.Throw<ArgumentNullException>(() => str.Pluralise(collection));
    }
}

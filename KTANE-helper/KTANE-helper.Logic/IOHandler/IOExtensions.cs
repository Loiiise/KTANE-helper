using System.Security.Cryptography.X509Certificates;

namespace KTANE_helper.Logic;

public static class IOExtensions
{
    /// <summary>
    /// Pluralise expects a word that is assumed to be non-pluralised, and either a collection or a count.
    /// It then returns that word in either its plural or non-plural form depending on the size of the collection
    /// (or count)
    /// </summary>
    /// <typeparam name="T">The type of things in the collection. Not used in the body logic.</typeparam>
    /// <param name="word">Non-plural word to be pluralised.</param>
    /// <param name="collection">The collection depending on which to pluralise.</param>
    /// <returns>A correctly pluralised word.</returns>
    internal static string Pluralise<T>(this string word, IEnumerable<T> collection)
            => word.Pluralise(collection.Count());

    internal static string Pluralise(this string word, int count)
    {
        // -1 and 1 item both should not pluralise their word
        if (Math.Abs(count) == 1) return word;

        // 2, 3, 4, etc. items
        return word + "s";
    }

    /// <summary>
    /// Formats a collection of Ts into a sequence of the format "a, b, c, and d" etc.
    /// </summary>
    /// <typeparam name="T">The type of the items to sequencify.</typeparam>
    /// <param name="collection">The items to sequencify.</param>
    internal static string ShowSequence<T>(this IEnumerable<T> collection)
    {
        // Return if the collection is empty
        if (collection is null || !collection.Any()) return "";

        // Take all elements except the last item
        var notLastPart = collection.Take(collection.Count() - 1);
        string last;
        if (collection.Last() is var lastT && lastT is not null)
            last = lastT.ToString() ?? "null";
        else last = "null";

        // If there are no preceding items, return just the last item
        if (!notLastPart.Any()) return last.ToString();
        // Else, return the first few separated by a comma, followed by "and {last}"
        else return string.Join(" and ", new List<string> { string.Join(", ", notLastPart), last });
    }
}

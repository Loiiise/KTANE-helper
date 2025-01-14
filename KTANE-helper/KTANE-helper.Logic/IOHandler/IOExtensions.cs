﻿namespace KTANE_helper.Logic;

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
        if (Math.Abs(count) == 1) return word;

        return word + "s";
    }

    /// <summary>
    /// Formats a collection of Ts such as [a, b, c, d] into a sequence of the format "a, b, c and d".
    /// </summary>
    /// <typeparam name="T">The type of the items to sequencify.</typeparam>
    /// <param name="collection">The items to sequencify.</param>
    internal static string ShowSequence<T>(this IEnumerable<T> collection)
    {
        // Return if the collection is empty
        if (collection is null || !collection.Any()) return string.Empty;

        // Map all elements in the selection to a string
        var strings = collection.Select(StringValueOrDefault);

        // Separate the last item from the preceding items
        var preceding = strings.Take(collection.Count() - 1);
        var last = strings.Last();

        // If there are no preceding items, return just the last item
        if (!preceding.Any()) return last;
        // Else, return the first few separated by a comma, followed by "and {last}"
        else return $"{string.Join(", ", preceding)} and {last}";


        // Maps a T to a string representation or a default value if it is null
        static string StringValueOrDefault(T item)
        {
            if (item is null || item.ToString() is not string str) return "???";

            return str;
        }
    }
}

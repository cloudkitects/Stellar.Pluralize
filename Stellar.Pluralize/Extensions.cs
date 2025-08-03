using System.Globalization;
using System.Numerics;

namespace Stellar.Pluralize;

public static class Extensions
{
    public static string ToProperInvariant(this string value)
    {
        return char.ToUpperInvariant(value[0]) + value[1..].ToLowerInvariant();
    }

    public static string Inflect(this string word, bool pluralize = true)
    {
        return new Pluralizer().Inflect(word, pluralize);
    }

    public static string Pluralize(this string word)
    {
        return word.Inflect();
    }

    public static string Singularize(this string word)
    {
        return word.Inflect(true);
    }

    public static string Inflect<T>(this string word, T count, string? format = null) where T : INumber<T>
    {
        var inflected = new Pluralizer().Inflect(word, count != T.One);

        if (string.IsNullOrWhiteSpace(format))
        {
            return inflected;
        }

        return $"{count.ToString(format.Trim(), CultureInfo.CurrentCulture)} {inflected}";
    }
}

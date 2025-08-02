using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("Stellar.Pluralize.Tests")]
namespace Stellar.Pluralize;

public static partial class Pluralizer 
{
    private static readonly List<Rule> plurals = Rules.Plurals;
    private static readonly List<Rule> singulars = Rules.Singulars;
    private static readonly ICollection<string> uncountables = new HashSet<string>(Rules.Uncountables, StringComparer.OrdinalIgnoreCase);
    private static readonly Dictionary<string, string> irregularPlurals = Rules.IrregularPlurals;
    private static readonly Dictionary<string, string> irregulars = Rules.Irregulars;

    [GeneratedRegex("\\$(\\d{1,2})")]
    private static partial Regex ReplacementRegex();

    public static string Pluralize(string word)
    {
        return Inflect(word);
    }

    public static string Singularize(string word)
    {
        return Inflect(word, false);
    }

    internal static string Inflect(string word, bool pluralize = true)
    {
        var replaceables = irregulars;
        var replacements = irregularPlurals;
        var rules = plurals;

        if (!pluralize)
        {
            (replaceables, replacements) = (replacements, replaceables);
        
            rules = singulars;
        }

        if (replacements.ContainsKey(word))
        {
            return word;
        }

        if (replaceables.TryGetValue(word, out var replacement))
        {
            return RestoreCase(word, replacement);
        }

        if (string.IsNullOrWhiteSpace(word) || uncountables.Contains(word))
        {
            return word;
        }

        foreach (var rule in rules.AsEnumerable().Reverse())
        {
            var match = rule.Regex.Match(word);

            if (match.Success)
            {
                var value = match.Groups[0].Value;

                if (string.IsNullOrWhiteSpace(value))
                {
                    return rule.Regex.Replace(word, Evaluate($"{word[match.Index - 1]}", rule.Replacement), 1);
                }

                return rule.Regex.Replace(word, Evaluate(value, rule.Replacement), 1);
            }
        }

        return word;
    }

    public static bool IsSingular(string word) => Singularize(word) == word;

    public static bool IsPlural(string word) => Pluralize(word) == word;

    /// <remarks>
    /// TODO: word:format and other numeric count
    /// TODO: null or empty format is an indicator for no number display
    /// TODO: don't overwrite format (better try/catch)
    /// </remarks>
    public static string Format(string word, int count, string format = "G")
    {
        var quantified = count == 1
            ? Singularize(word)
            : Pluralize(word);

        if (string.IsNullOrWhiteSpace(format))
        {
            return quantified;
        }

        try
        {
            format = count.ToString(format);
        }
        catch
        {
            format = count.ToString("G");
        }

        return $"{format} {quantified}";
    }

    private static string RestoreCase(string word, string newWord)
    {
        if (word == newWord)
        {
            return newWord;
        }

        if (word.ToLowerInvariant().Equals(word))
        {
            return newWord.ToLowerInvariant();
        }

        if (word.ToUpperInvariant().Equals(word))
        {
            return newWord.ToUpperInvariant();
        }

        if (word == word[0].ToString().ToUpperInvariant() + word[1..].ToLowerInvariant())
        {
            return newWord[0].ToString().ToUpperInvariant() + newWord[1..].ToLowerInvariant();
        }

        return newWord.ToLowerInvariant();
    }

    private static MatchEvaluator Evaluate(string originalWord, string replacement)
    {
        return match =>
        {
            return RestoreCase(originalWord, ReplacementRegex().Replace(replacement, m => match.Groups[int.Parse(m.Groups[1].Value)].Value));
        };
    }

    #region add rules
    internal static string SanitizeRegex(string regex)
    {
        return $"{(regex[0] != '^' ? "^" : "")}{regex}{(regex[^1] != '$' ? "$" : "")}";
    }

    public static void AddPlural(string regex, string plural, RegexOptions options = RegexOptions.IgnoreCase)
    {
        plurals.Add(new Rule(SanitizeRegex(regex), plural, options));
    }

    public static void AddSingular(string regex, string singular, RegexOptions options = RegexOptions.IgnoreCase)
    {
        singulars.Add(new Rule(SanitizeRegex(regex), singular, options));
    }

    public static void AddUncountable(string word)
    {
        uncountables.Add(word);

        AddPlural(word, "$0");
        AddSingular(word, "$0");
    }

    public static void AddOrUpdateIrregular(string singular, string plural)
    {
        irregulars[singular] = plural;
        irregularPlurals[plural] = singular;
    }
    #endregion
}

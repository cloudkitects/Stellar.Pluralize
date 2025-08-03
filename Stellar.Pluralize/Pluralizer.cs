using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("Stellar.Pluralize.Tests")]
namespace Stellar.Pluralize;

public partial class Pluralizer(
    List<Rule>? plurals = null,
    List<Rule>? singulars = null,
    List<string>? uncountables = null,
    Dictionary<string, string>? irregularPlurals = null,
    Dictionary<string, string>? irregulars = null)
{
    private readonly List<Rule> plurals = [.. plurals ?? Rules.Plurals];
    private readonly List<Rule> singulars = [.. singulars ?? Rules.Singulars];
    private readonly ICollection<string> uncountables = new HashSet<string>(uncountables ?? Rules.Uncountables, StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, string> irregularPlurals = new(irregularPlurals ?? Rules.IrregularPlurals);
    private readonly Dictionary<string, string> irregulars = new(irregulars ?? Rules.Irregulars);

    [GeneratedRegex("\\$(\\d{1,2})")]
    private partial Regex ReplacementRegex();

    public string Pluralize(string word)
    {
        return Inflect(word);
    }

    public string Singularize(string word)
    {
        return Inflect(word, false);
    }

    public string Inflect(string word, bool pluralize = true)
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

    public bool IsSingular(string word) => Singularize(word) == word;

    public bool IsPlural(string word) => Pluralize(word) == word;

    private static string RestoreCase(string word, string newWord)
    {
        if (newWord.Equals(word))
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

        if (word.ToProperInvariant().Equals(word))
        {
            return newWord.ToProperInvariant();
        }

        return newWord;
    }

    private MatchEvaluator Evaluate(string originalWord, string replacement)
    {
        return match =>
        {
            return RestoreCase(originalWord, ReplacementRegex().Replace(replacement, m => match.Groups[int.Parse(m.Groups[1].Value)].Value));
        };
    }

    #region add rules
    internal static string QualifyRegex(string regex)
    {
        return '^' + regex.Trim('^', '$') + '$';
    }

    public void AddPlural(string regex, string plural, RegexOptions options = RegexOptions.IgnoreCase)
    {
        plurals.Add(new Rule(QualifyRegex(regex), plural, options));
    }

    public void AddSingular(string regex, string singular, RegexOptions options = RegexOptions.IgnoreCase)
    {
        singulars.Add(new Rule(QualifyRegex(regex), singular, options));
    }

    public void AddUncountable(string word)
    {
        uncountables.Add(word);

        AddPlural(word, "$0");
        AddSingular(word, "$0");
    }

    public void AddOrUpdateIrregular(string singular, string plural)
    {
        irregulars[singular] = plural;
        irregularPlurals[plural] = singular;
    }
    #endregion
}

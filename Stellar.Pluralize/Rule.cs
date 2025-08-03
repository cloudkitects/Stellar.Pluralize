using System.Text.RegularExpressions;

namespace Stellar.Pluralize;

public class Rule(Regex regex, string replacement)
{
    public Regex Regex { get; set; } = regex;

    public string Replacement { get; set; } = replacement;

    public Rule(string regex, string replacement, RegexOptions options = RegexOptions.IgnoreCase) : this(new Regex(regex, options), replacement)
    {
    }
}

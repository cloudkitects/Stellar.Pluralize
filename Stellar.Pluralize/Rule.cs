using System.Text.RegularExpressions;

namespace Stellar.Pluralize;

public class Rule(string regex, string replacement, RegexOptions options = RegexOptions.IgnoreCase)
{
    public Regex Regex { get; set; } = new Regex(regex, options);

    public string Replacement { get; set; } = replacement;
}

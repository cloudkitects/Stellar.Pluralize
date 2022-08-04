using System.Text.RegularExpressions;

namespace Stellar.Pluralize
{
    /// <summary>
    /// An encapsulation of patterns to replace.
    /// </summary>
    public class Rule
    {
        /// <summary>
        /// Regular expression.
        /// </summary>
        public Regex Regex { get; set; }
        
        /// <summary>
        /// Replacement for matches.
        /// </summary>
        public string Replacement { get; set; }

        /// <summary>
        /// Simple constructor.
        /// Use new Rule(/* language=regex */ "<regex>", "<replacement>") for 
        /// <see href="https://stackoverflow.com/questions/63639758">VS regex highlighting support</see>.
        /// </summary>
        public Rule(string regex, string replacement, RegexOptions options = RegexOptions.IgnoreCase)
        {
            Regex = new Regex(regex, options);
            Replacement = replacement;
        }
    }
}

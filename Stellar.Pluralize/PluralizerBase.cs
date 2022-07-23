using System.Text.RegularExpressions;

namespace Stellar.Pluralize
{
    public abstract class PluralizerBase : IPluralizer
    {
        protected readonly IList<Rule> _plurals;
        protected readonly IList<Rule> _singulars;
        protected readonly ICollection<string> _uncountables;
        protected readonly IDictionary<string, string> _irregularPlurals;
        protected readonly IDictionary<string, string> _irregulars;

        private static readonly Regex _replacementRegex = new("\\$(\\d{1,2})");

        public PluralizerBase(
            IList<Rule>? plurals = null,
            IList<Rule>? singulars = null,
            ICollection<string>? uncountables = null,
            IDictionary<string, string>? irregularPlurals = null,
            IDictionary<string, string>? irregulars = null)
        {
            _plurals = new List<Rule>(plurals ?? Rules.Plurals);
            _singulars = new List<Rule>(singulars ?? Rules.Singulars);
            _uncountables = new HashSet<string>(uncountables ?? Rules.Uncountables, StringComparer.OrdinalIgnoreCase);
            _irregularPlurals = new Dictionary<string, string>(irregularPlurals ?? Rules.IrregularPlurals);
            _irregulars = new Dictionary<string, string>(irregulars ?? Rules.Irregulars);
        }

        public string Pluralize(string word)
        {
            return Transform(word, _irregulars, _irregularPlurals, _plurals);
        }

        public string Singularize(string word)
        {
            return Transform(word, _irregularPlurals, _irregulars, _singulars);
        }

        public bool IsSingular(string word)
        {
            return Singularize(word) == word;
        }

        public bool IsPlural(string word)
        {
            return Pluralize(word) == word;
        }

        public string Format(string word, int count, bool inclusive = false)
        {
            var pluralized = count == 1
                ? Singularize(word)
                : Pluralize(word);

            return (inclusive ? count + " " : "") + pluralized;
        }

        private static string RestoreCase(string word, string newWord)
        {
            if (word == newWord)
            {
                return newWord;
            }

            if (word == word.ToLower())
            {
                return newWord.ToLower();
            }

            if (word == word.ToUpper())
            {
                return newWord.ToUpper();
            }

            if (word == word.ToTitle())
            {
                return newWord.ToTitle();
            }

            return newWord.ToLower();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <param name="replaceables"></param>
        /// <param name="replacements"></param>
        /// <param name="rules"></param>
        /// <returns></returns>
        /// <remarks>
        /// Iterates over the rules backwards to prioritize user specific/custom rules.
        /// </remarks>
        private string Transform(
            string word,
            IDictionary<string, string> replaceables,
            IDictionary<string, string> replacements,
            IList<Rule> rules)
        {
            if (replacements.ContainsKey(word))
            {
                return word;
            }

            if (replaceables.TryGetValue(word, out var replacement))
            {
                return RestoreCase(word, replacement);
            }

            if (string.IsNullOrWhiteSpace(word) || _uncountables.Contains(word))
            {
                return word;
            }

            foreach (var rule in rules.Reverse())
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
        
        private static MatchEvaluator Evaluate(string originalWord, string replacement)
        {
            return match =>
            {
                return RestoreCase(originalWord, _replacementRegex.Replace(replacement, m => match.Groups[int.Parse(m.Groups[1].Value)].Value));
            };
        }

        #region add rules
        private static string SanitizeRegex(string regex)
        {
            return $"{(regex[0] != '^' ? "^" : "")}{regex}{(regex[^1] != '$' ? "$" : "")}";
        }

        public void AddPlural(string regex, string plural, RegexOptions options = RegexOptions.IgnoreCase)
        {
            _plurals.Add(new Rule(SanitizeRegex(regex), plural, options));
        }

        public void AddSingular(string regex, string singular, RegexOptions options = RegexOptions.IgnoreCase)
        {
            _singulars.Add(new Rule(SanitizeRegex(regex), singular, options));
        }

        public void AddUncountable(string word)
        {
            _uncountables.Add(word);

            _plurals.Add(new Rule(SanitizeRegex(word), "$0"));
            _singulars.Add(new Rule(SanitizeRegex(word), "$0"));
        }

        public void AddOrUpdateIrregular(string singular, string plural)
        {
            _irregulars[singular] = plural;
            _irregularPlurals[plural] = singular;
        }
        #endregion
    }
}

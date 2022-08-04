using System.Text.RegularExpressions;

namespace Stellar.Pluralize.Tests
{
    public class PluralizerTests
    {
        #region parse test data
        /// <summary>
        /// Data reader helper.
        /// </summary>
        /// <param name="data">A string, typically the contents of a CSV file resource.</param>
        /// <returns>The file contents as data (a list of singular and plural pairs).</returns>
        private static IEnumerable<object[]> ParseData(string data)
        {
            return data.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Split(','))
            .Select(pair => new object[] { pair[0], pair[1] })
            .ToList();
        }

        public static IEnumerable<object[]> Words => ParseData(Resources.Words);
        public static IEnumerable<object[]> Singulars => ParseData(Resources.Singulars);
        public static IEnumerable<object[]> Plurals => ParseData(Resources.Plurals);
        #endregion

        private readonly Pluralizer _pluralizer = new();

        [Fact]
        public void SanitizesRegexes()
        {
            /* language=regex */ var regex1 = "^hello$";
            /* language=regex */ var regex2 = "hello";

            Assert.Equal(regex1, PluralizerBase.SanitizeRegex(regex1));
            Assert.Equal(regex1, PluralizerBase.SanitizeRegex(regex2));
        }

        [Theory]
        [MemberData(nameof(Words))]
        public void HandlesWords(string singular, string plural)
        {
            Assert.Equal(singular, _pluralizer.Singularize(plural));
            Assert.Equal(plural, _pluralizer.Pluralize(singular));

            Assert.Equal(singular, _pluralizer.Singularize(singular));
            Assert.Equal(plural, _pluralizer.Pluralize(plural));
        }

        [Theory]
        [MemberData(nameof(Plurals))]
        public void HandlesPluralExceptions(string singular, string plural)
        {
            Assert.Equal(singular, _pluralizer.Singularize(plural));
            Assert.Equal(singular, _pluralizer.Singularize(singular));
        }

        [Theory]
        [MemberData(nameof(Singulars))]
        public void HandlesSingularExceptions(string singular, string plural)
        {
            Assert.Equal(plural, _pluralizer.Pluralize(singular));
            Assert.Equal(plural, _pluralizer.Pluralize(plural));
        }

        [Theory]
        [InlineData("confetto", "confetti")] // not confettos
        [InlineData("cul-de-sac", "culs-de-sac")] // not cul-de-sacs
        [InlineData("regex", "regexii")] // not regexes
        public void HandlesNewRules(string singular, string plural)
        {
            Assert.NotEqual(plural, _pluralizer.Pluralize(singular));
            Assert.NotEqual(singular, _pluralizer.Singularize(plural));

            _pluralizer.AddPlural(singular, plural);
            _pluralizer.AddSingular(plural, singular);

            Assert.Equal(plural, _pluralizer.Pluralize(singular));
            Assert.Equal(singular, _pluralizer.Singularize(plural));
        }

        [Theory]
        [InlineData("single", "singular")] // not singles
        [InlineData("angle", "angular")] // not singles
        public void HandlesNewRules2(string word, string expected)
        {
            _pluralizer.AddPlural("((a|si)ng)le", "$1ular");
            _pluralizer.AddSingular("((a|si)ng)ular", "$1le");

            var pluralized = _pluralizer.Pluralize(word);

            Assert.Equal(expected, pluralized);
            Assert.Equal(word, _pluralizer.Singularize(pluralized));
        }

        [Theory]
        [InlineData("cul-de-sac", "culs-de-sac")]
        [InlineData("mother-in-law", "mothers-in-law")]
        [InlineData("one-half", "ones-half")] // incorrect
        public void HandlesNewRules3(string word, string expected)
        {
            _pluralizer.AddPlural(  /* language=regex */ @"(\w+)([-\w]+)+",  "$1s$2");
            _pluralizer.AddSingular(/* language=regex */ @"(\w+)s([-\w]+)+", "$1$2");

            var pluralized = _pluralizer.Pluralize(word);

            Assert.Equal(expected, pluralized);
            Assert.Equal(word, _pluralizer.Singularize(pluralized));
        }

        [Theory]
        [InlineData("cul-de-sac", "culs-de-sac")]
        [InlineData("mother-in-law", "mothers-in-law")]
        [InlineData("one-half", "one-halves")]
        public void HandlesNewRules4(string word, string expected)
        {
            _pluralizer.AddPlural(  /* language=regex */ @"((cul|mother)(-de-sac|-in-law))",  "$2s$3");
            _pluralizer.AddSingular(/* language=regex */ @"((cul|mother)s(-de-sac|-in-law))", "$2$3");

            var pluralized = _pluralizer.Pluralize(word);

            Assert.Equal(expected, pluralized);
            Assert.Equal(word, _pluralizer.Singularize(pluralized));
        }

        [Fact]
        public void HandlesInvalidRules()
        {
            Assert.Throws<RegexParseException>(() => _pluralizer.AddPlural("a|si)ngle", "$1ngular"));
            Assert.Throws<RegexParseException>(() => _pluralizer.AddSingular("([^a-z).*", "$1"));
            Assert.Throws<RegexParseException>(() => _pluralizer.AddUncountable("(|bogus.*"));
        }

        [Theory]
        [InlineData("person", "people", "persons")]
        [InlineData("schema", "schemata", "schemas")]
        public void HandlesNewOrUpdatedIrregulars(string singular, string plural, string irregular)
        {
            Assert.Equal(plural, _pluralizer.Pluralize(singular));
            Assert.Equal(singular, _pluralizer.Singularize(plural));
            Assert.NotEqual(irregular, _pluralizer.Pluralize(singular));

            _pluralizer.AddOrUpdateIrregular(singular, irregular);

            Assert.Equal(irregular, _pluralizer.Pluralize(singular));
            Assert.Equal(singular, _pluralizer.Singularize(irregular));
            Assert.Equal(singular, _pluralizer.Singularize(plural));
        }

        [Theory]
        [InlineData("mean", "means")]
        [InlineData("vertebra", "vertebrae")]
        public void HandlesNewUncountables(string singular, string plural)
        {
            Assert.Equal(plural, _pluralizer.Pluralize(singular));
            Assert.Equal(singular, _pluralizer.Singularize(plural));
            
            _pluralizer.AddUncountable(singular);
            _pluralizer.AddUncountable(plural);
            
            Assert.Equal(singular, _pluralizer.Pluralize(singular));
            Assert.Equal(plural, _pluralizer.Singularize(plural));
        }
        
        [Theory]
        [InlineData("vertebra", 3, "3 vertebrae")]
        [InlineData("dogs", 1, "1 dog")]
        [InlineData("dog", 5000, "5000 dogs")]
        [InlineData("vote", 11, "11 votes")]
        [InlineData("cat", 0, "0 cats")]
        public void FormatsWithoutFormat(string word, int count, string expected)
        {
            Assert.Equal(expected, _pluralizer.Format(word, count));
        }

        [Theory]
        [InlineData("vertebra", 3, null, "vertebrae")]
        [InlineData("vertebra", 3, "", "vertebrae")]
        [InlineData("vertebra", 3, " ", "vertebrae")]
        [InlineData("vertebra", 3, "    ", "vertebrae")] // Tab
        [InlineData("dog", 5000, "",   "dogs")]
        [InlineData("dog", 5000, "N0", "5,000 dogs")]
        [InlineData("person", 11, "N2", "11.00 people")]
        public void FormatsWithFormat(string word, int count, string format, string expected)
        {
            Assert.Equal(expected, _pluralizer.Format(word, count, format));
        }
    }
}
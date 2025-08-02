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
        private static List<object[]> ParseData(string data)
        {
            return [.. data.Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Split(','))
            .Select(pair => new object[] { pair[0], pair[1] })];
        }

        public static IEnumerable<object[]> Words => ParseData(Resources.Words);
        public static IEnumerable<object[]> Singulars => ParseData(Resources.Singulars);
        public static IEnumerable<object[]> Plurals => ParseData(Resources.Plurals);
        #endregion

        [Fact]
        public void SanitizesRegexes()
        {
            /* language=regex */ var regex1 = "^hello$";
            /* language=regex */ var regex2 = "hello";

            Assert.Equal(regex1, Pluralizer.SanitizeRegex(regex1));
            Assert.Equal(regex1, Pluralizer.SanitizeRegex(regex2));
        }

        [Theory]
        [MemberData(nameof(Words))]
        public void HandlesWords(string singular, string plural)
        {
            Assert.Equal(singular, Pluralizer.Singularize(plural));
            Assert.Equal(plural, Pluralizer.Pluralize(singular));

            Assert.Equal(singular, Pluralizer.Singularize(singular));
            Assert.Equal(plural, Pluralizer.Pluralize(plural));
        }

        [Theory]
        [MemberData(nameof(Plurals))]
        public void HandlesPluralExceptions(string singular, string plural)
        {
            Assert.Equal(singular, Pluralizer.Singularize(plural));
            Assert.Equal(singular, Pluralizer.Singularize(singular));
        }

        [Theory]
        [MemberData(nameof(Singulars))]
        public void HandlesSingularExceptions(string singular, string plural)
        {
            Assert.Equal(plural, Pluralizer.Pluralize(singular));
            Assert.Equal(plural, Pluralizer.Pluralize(plural));
        }

        [Theory]
        [InlineData("confetto", "confetti")] // not confettos
        [InlineData("cul-de-sac", "culs-de-sac")] // not cul-de-sacs
        [InlineData("regex", "regexii")] // not regexes
        public void HandlesNewRules(string singular, string plural)
        {
            Assert.NotEqual(plural, Pluralizer.Pluralize(singular));
            Assert.NotEqual(singular, Pluralizer.Singularize(plural));

            Pluralizer.AddPlural(singular, plural);
            Pluralizer.AddSingular(plural, singular);

            Assert.Equal(plural, Pluralizer.Pluralize(singular));
            Assert.Equal(singular, Pluralizer.Singularize(plural));
        }

        [Theory]
        [InlineData("single", "singular")] // not singles
        [InlineData("angle", "angular")] // not singles
        public void HandlesNewRules2(string word, string expected)
        {
            Pluralizer.AddPlural("((a|si)ng)le", "$1ular");
            Pluralizer.AddSingular("((a|si)ng)ular", "$1le");

            var pluralized = Pluralizer.Pluralize(word);

            Assert.Equal(expected, pluralized);
            Assert.Equal(word, Pluralizer.Singularize(pluralized));
        }

        [Theory]
        [InlineData("cul-de-sac", "culs-de-sac")]
        [InlineData("mother-in-law", "mothers-in-law")]
        [InlineData("one-half", "ones-half")] // incorrect
        public void HandlesNewRules3(string word, string expected)
        {
            Pluralizer.AddPlural(  /* language=regex */ @"(\w+)([-\w]+)+", "$1s$2");
            Pluralizer.AddSingular(/* language=regex */ @"(\w+)s([-\w]+)+", "$1$2");

            var pluralized = Pluralizer.Pluralize(word);

            Assert.Equal(expected, pluralized);
            Assert.Equal(word, Pluralizer.Singularize(pluralized));
        }

        [Theory]
        [InlineData("cul-de-sac", "culs-de-sac")]
        [InlineData("mother-in-law", "mothers-in-law")]
        [InlineData("one-half", "one-halves")]
        public void HandlesNewRules4(string word, string expected)
        {
            Pluralizer.AddPlural(  /* language=regex */ @"((cul|mother)(-de-sac|-in-law))", "$2s$3");
            Pluralizer.AddSingular(/* language=regex */ @"((cul|mother)s(-de-sac|-in-law))", "$2$3");

            var pluralized = Pluralizer.Pluralize(word);

            Assert.Equal(expected, pluralized);
            Assert.Equal(word, Pluralizer.Singularize(pluralized));
        }

        [Fact]
        public void HandlesInvalidRules()
        {
            Assert.Throws<RegexParseException>(() => Pluralizer.AddPlural("a|si)ngle", "$1ngular"));
            Assert.Throws<RegexParseException>(() => Pluralizer.AddSingular("([^a-z).*", "$1"));
            Assert.Throws<RegexParseException>(() => Pluralizer.AddUncountable("(|bogus.*"));
        }

        [Theory]
        [InlineData("person", "people", "persons")]
        [InlineData("schema", "schemata", "schemas")]
        public void HandlesNewOrUpdatedIrregulars(string singular, string plural, string irregular)
        {
            Assert.Equal(plural, Pluralizer.Pluralize(singular));
            Assert.Equal(singular, Pluralizer.Singularize(plural));
            Assert.NotEqual(irregular, Pluralizer.Pluralize(singular));

            Pluralizer.AddOrUpdateIrregular(singular, irregular);

            Assert.Equal(irregular, Pluralizer.Pluralize(singular));
            Assert.Equal(singular, Pluralizer.Singularize(irregular));
            Assert.Equal(singular, Pluralizer.Singularize(plural));
        }

        [Theory]
        [InlineData("mean", "means")]
        [InlineData("vertebra", "vertebrae")]
        public void HandlesNewUncountables(string singular, string plural)
        {
            Assert.Equal(plural, Pluralizer.Pluralize(singular));
            Assert.Equal(singular, Pluralizer.Singularize(plural));

            Pluralizer.AddUncountable(singular);
            Pluralizer.AddUncountable(plural);

            Assert.Equal(singular, Pluralizer.Pluralize(singular));
            Assert.Equal(plural, Pluralizer.Singularize(plural));
        }

        [Theory]
        [InlineData("vertebra", 3, "3 vertebrae")]
        [InlineData("dogs", 1, "1 dog")]
        [InlineData("dog", 5000, "5000 dogs")]
        [InlineData("vote", 11, "11 votes")]
        [InlineData("cat", 0, "0 cats")]
        public void FormatsWithoutFormat(string word, int count, string expected)
        {
            Assert.Equal(expected, Pluralizer.Format(word, count));
        }

        [Theory]
        [InlineData("vertebra", 3, null, "vertebrae")]
        [InlineData("vertebra", 3, "", "vertebrae")]
        [InlineData("vertebra", 3, " ", "vertebrae")]
        [InlineData("vertebra", 3, "    ", "vertebrae")] // Tab
        [InlineData("dog", 5000, "", "dogs")]
        [InlineData("dog", 5000, "N0", "5,000 dogs")]
        [InlineData("person", 11, "N2", "11.00 people")]
        public void FormatsWithFormat(string word, int count, string format, string expected)
        {
            Assert.Equal(expected, Pluralizer.Format(word, count, format));
        }
    }
}
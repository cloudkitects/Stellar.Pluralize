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
        [InlineData("for whom the bell tolls", "For Whom the Bell Tolls")]
        [InlineData("girl on a train", "Girl On a Train")]
        [InlineData("a visit from the Goon Squad", "A Visit From the Goon Squad")]
        [InlineData("a string value, e.g., a STRING value", "A String Value, e.g., a String Value")]
        public void Capitalizes(string sentence, string expected)
        {
            Assert.Equal(expected, sentence.ToTitle());
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

            var pluralizer = new Pluralizer();
            
            pluralizer.AddPlural(singular, plural);
            pluralizer.AddSingular(plural, singular);

            Assert.Equal(plural, pluralizer.Pluralize(singular));
            Assert.Equal(singular, pluralizer.Singularize(plural));
        }

        [Theory]
        [InlineData("person", "persons", "people")]
        public void HandlesNewOrUpdatedIrregulars(string singular, string plural, string original)
        {
            Assert.NotEqual(plural, _pluralizer.Pluralize(singular));
            Assert.Equal(singular, _pluralizer.Singularize(plural));
            Assert.Equal(singular, _pluralizer.Singularize(original));

            var pluralizer = new Pluralizer();
            
            pluralizer.AddOrUpdateIrregular(singular, plural);

            Assert.Equal(plural, pluralizer.Pluralize(singular));
            Assert.Equal(singular, pluralizer.Singularize(plural));
            Assert.Equal(singular, pluralizer.Singularize(original));
        }

        [Theory]
        [InlineData("mean", "means")]
        [InlineData("vertebra", "vertebrae")]
        public void HandlesNewUncountables(string singular, string plural)
        {
            Assert.Equal(plural, _pluralizer.Pluralize(singular));
            Assert.Equal(singular, _pluralizer.Singularize(plural));
            
            var pluralizer = new Pluralizer();

            pluralizer.AddUncountable(singular);
            pluralizer.AddUncountable(plural);
            
            Assert.Equal(singular, pluralizer.Pluralize(singular));
            Assert.Equal(plural, pluralizer.Singularize(plural));
        }
        
        [Theory]
        [InlineData("vertebra", 3, "3 vertebrae")]
        [InlineData("dogs", 1, "1 dog")]
        [InlineData("dog", 5000, "5000 dogs")]
        [InlineData("vote", 11, "11 votes")]
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
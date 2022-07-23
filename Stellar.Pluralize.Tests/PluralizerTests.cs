namespace Stellar.Pluralize.Tests
{
    public class PluralizerTests
    {
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

        private readonly Pluralizer _pluralizer = new();

        [Theory]
        [MemberData(nameof(Words))]
        public void TestWords(string singular, string plural)
        {
            Assert.Equal(singular, _pluralizer.Singularize(plural));
            Assert.Equal(plural, _pluralizer.Pluralize(singular));

            Assert.Equal(singular, _pluralizer.Singularize(singular));
            Assert.Equal(plural, _pluralizer.Pluralize(plural));
        }

        [Theory]
        [MemberData(nameof(Plurals))]
        public void TestPluralExceptions(string singular, string plural)
        {
            Assert.Equal(singular, _pluralizer.Singularize(plural));
            Assert.Equal(singular, _pluralizer.Singularize(singular));
        }


        [Theory]
        [MemberData(nameof(Singulars))]
        public void TestSingularExceptions(string singular, string plural)
        {
            Assert.Equal(plural, _pluralizer.Pluralize(singular));
            Assert.Equal(plural, _pluralizer.Pluralize(plural));
        }

        /// <summary>
        /// Test new rules.
        /// </summary>
        [Theory]
        [InlineData("confetto", "confetti")]
        [InlineData("cul-de-sac", "culs-de-sac")]
        public void TestNewRules(string singular, string plural)
        {
            _pluralizer.AddPlural(singular, plural);
            _pluralizer.AddSingular(plural, singular);

            Assert.Equal(plural, _pluralizer.Pluralize(singular));
            Assert.Equal(singular, _pluralizer.Singularize(plural));
        }

        /// <summary>
        /// Test that adding uncountables override rules.
        /// </summary>
        [Theory]
        [InlineData("mean", "means")]
        [InlineData("vertebra", "vertebrae")]
        public void TestAddUncountables(string singular, string plural)
        {
            Assert.Equal(plural, _pluralizer.Pluralize(singular));
            Assert.Equal(singular, _pluralizer.Singularize(plural));
            
            // user-defined context
            _pluralizer.AddUncountable(singular);
            _pluralizer.AddUncountable(plural);
            
            Assert.Equal(singular, _pluralizer.Pluralize(singular));
            Assert.Equal(plural, _pluralizer.Singularize(plural));
        }
    }
}
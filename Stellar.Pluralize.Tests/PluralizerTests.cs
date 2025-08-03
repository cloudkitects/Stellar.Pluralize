using System.Text.RegularExpressions;

namespace Stellar.Pluralize.Tests;

public class PluralizerTests
{
    private readonly Pluralizer pluralizer = new();

    #region test data
    private static TheoryData<string, string> FromResourcePairs(string data)
    {
        var theoryData = new TheoryData<string, string>();

        foreach (var line in data.Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries))
        {
            var pair = line.Split(',');

            theoryData.Add(pair[0], pair[1]);
        }

        return theoryData;
    }

    private static TheoryData<string, string, string> FromResourceTrios(string data)
    {
        var theoryData = new TheoryData<string, string, string>();

        foreach (var line in data.Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries))
        {
            var pair = line.Split(',');

            theoryData.Add(pair[0], pair[1], pair[2]);
        }

        return theoryData;
    }

    public static readonly TheoryData<string, string> Regexes = new()
    {
        { "^hello$", "^hello$" },
        { "hello", "^hello$" }
    };

    public static readonly TheoryData<string, string> NewRules = new()
    {
        { "confetto", "confetti" },      // not confettos
        { "cul-de-sac", "culs-de-sac" }, // not cul-de-sacs
        { "regex", "regexii" }           // not regexes
    };
    #endregion

    [Theory]
    [MemberData(nameof(Regexes))]
    public void QualifiesRegexes(string input, string output)
    {
        Assert.Equal(output, Pluralizer.QualifyRegex(input));
    }

    public static TheoryData<string, string> Words => FromResourcePairs(Resources.Words);

    [Theory]
    [MemberData(nameof(Words))]
    public void HandlesWords(string singular, string plural)
    {
        Assert.Equal(singular, pluralizer.Singularize(plural));
        Assert.Equal(plural, pluralizer.Pluralize(singular));

        Assert.Equal(singular, pluralizer.Singularize(singular));
        Assert.Equal(plural, pluralizer.Pluralize(plural));
    }

    public static TheoryData<string, string> Plurals => FromResourcePairs(Resources.Plurals);

    [Theory]
    [MemberData(nameof(Plurals))]
    public void HandlesPluralExceptions(string singular, string plural)
    {
        Assert.Equal(singular, pluralizer.Singularize(plural));
        Assert.Equal(singular, pluralizer.Singularize(singular));
    }

    public static TheoryData<string, string> Singulars => FromResourcePairs(Resources.Singulars);

    [Theory]
    [MemberData(nameof(Singulars))]
    public void HandlesSingularExceptions(string singular, string plural)
    {
        Assert.Equal(plural, pluralizer.Pluralize(singular));
        Assert.Equal(plural, pluralizer.Pluralize(plural));
    }

    [Theory]
    [MemberData(nameof(NewRules))]
    public void HandlesNewRules(string singular, string plural)
    {
        Assert.NotEqual(plural, pluralizer.Pluralize(singular));
        Assert.NotEqual(singular, pluralizer.Singularize(plural));

        pluralizer.AddPlural(singular, plural);
        pluralizer.AddSingular(plural, singular);

        Assert.Equal(plural, pluralizer.Pluralize(singular));
        Assert.Equal(singular, pluralizer.Singularize(plural));
    }

    [Theory]
    [InlineData("single", "singular")] // not singles
    [InlineData("angle", "angular")] // not singles
    public void HandlesNewRules2(string word, string expected)
    {
        pluralizer.AddPlural("((a|si)ng)le", "$1ular");
        pluralizer.AddSingular("((a|si)ng)ular", "$1le");

        var pluralized = pluralizer.Pluralize(word);

        Assert.Equal(expected, pluralized);
        Assert.Equal(word, pluralizer.Singularize(pluralized));
    }

    [Theory]
    [InlineData("cul-de-sac", "culs-de-sac")]
    [InlineData("mother-in-law", "mothers-in-law")]
    [InlineData("one-half", "ones-half")] // incorrect
    public void HandlesNewRules3(string word, string expected)
    {
        pluralizer.AddPlural(  /* language=regex */ @"(\w+)([-\w]+)+", "$1s$2");
        pluralizer.AddSingular(/* language=regex */ @"(\w+)s([-\w]+)+", "$1$2");

        var pluralized = pluralizer.Pluralize(word);

        Assert.Equal(expected, pluralized);
        Assert.Equal(word, pluralizer.Singularize(pluralized));
    }

    [Theory]
    [InlineData("cul-de-sac", "culs-de-sac")]
    [InlineData("mother-in-law", "mothers-in-law")]
    [InlineData("one-half", "one-halves")]
    public void HandlesNewRules4(string word, string expected)
    {
        pluralizer.AddPlural(  /* language=regex */ @"((cul|mother)(-de-sac|-in-law))", "$2s$3");
        pluralizer.AddSingular(/* language=regex */ @"((cul|mother)s(-de-sac|-in-law))", "$2$3");

        var pluralized = pluralizer.Pluralize(word);

        Assert.Equal(expected, pluralized);
        Assert.Equal(word, pluralizer.Singularize(pluralized));
    }

    [Fact]
    public void HandlesInvalidRules()
    {
        Assert.Throws<RegexParseException>(() => pluralizer.AddPlural("a|si)ngle", "$1ngular"));
        Assert.Throws<RegexParseException>(() => pluralizer.AddSingular("([^a-z).*", "$1"));
        Assert.Throws<RegexParseException>(() => pluralizer.AddUncountable("(|bogus.*"));
    }

    public static TheoryData<string, string, string> Irregulars => FromResourceTrios(Resources.Irregulars);

    [Theory]
    [MemberData(nameof(Irregulars))]
    public void HandlesNewOrUpdatedIrregulars(string singular, string plural, string irregular)
    {
        Assert.Equal(plural, pluralizer.Pluralize(singular));
        Assert.Equal(singular, pluralizer.Singularize(plural));
        Assert.NotEqual(irregular, pluralizer.Pluralize(singular));

        pluralizer.AddOrUpdateIrregular(singular, irregular);

        Assert.Equal(irregular, pluralizer.Pluralize(singular));
        Assert.Equal(singular, pluralizer.Singularize(irregular));
        Assert.Equal(singular, pluralizer.Singularize(plural));
    }

    [Theory]
    [InlineData("mean", "means")]
    [InlineData("vertebra", "vertebrae")]
    public void HandlesNewUncountables(string singular, string plural)
    {
        Assert.Equal(plural, pluralizer.Pluralize(singular));
        Assert.Equal(singular, pluralizer.Singularize(plural));

        pluralizer.AddUncountable(singular);
        pluralizer.AddUncountable(plural);

        Assert.Equal(singular, pluralizer.Pluralize(singular));
        Assert.Equal(plural, pluralizer.Singularize(plural));
    }

    [Theory]
    [InlineData("vertebra", 3, null, "vertebrae")]
    [InlineData("dogs", 1, null, "dog")]
    [InlineData("dog", 5000, null, "dogs")]
    [InlineData("vote", 11, null, "votes")]
    [InlineData("cat", 0, null, "cats")]
    [InlineData("cat", 0, "N0", "0 cats")]
    [InlineData("vertebra", 3, "", "vertebrae")]
    [InlineData("vertebra", 3, " ", "vertebrae")]
    [InlineData("vertebra", 3, "\t", "vertebrae")]
    [InlineData("dog", 5000, "", "dogs")]
    [InlineData("dog", 5000, "N0", "5,000 dogs")]
    [InlineData("person", 44.75, "F2", "44.75 people")]
    [InlineData("galaxies", 1, "N0", "1 galaxy")]
    [InlineData("galaxies", 1, null, "galaxy")]
    [InlineData("galaxy", 2048328473, "N0", "2,048,328,473 galaxies")]
    [InlineData("galaxy", 2048328473, "E4", "2.0483E+009 galaxies")]
    [InlineData("respondent", 0.4475, "P2", "44.75% respondents")]
    public void Formats(string word, double count, string? format, string expected)
    {
        Assert.Equal(expected, word.Inflect(count, format));
    }


    [Fact]
    public void Phrases()
    {
        var boilerplate = "The quick brown fox jumps over the lazy dog.";

        var nouns = new string[] { "fox", "dog" };
        var verbs = new string[] { "jumps" };
        
        var result = boilerplate;

        foreach (var noun in nouns)
        {
            var inflected = noun.Inflect();

            result = result.Replace(noun, inflected);
        }

        foreach (var verb in verbs)
        {
            var inflected = verb.Inflect(false);
            
            result = result.Replace(verb, inflected);
        }

        Assert.Equal("The quick brown foxes jump over the lazy dogs.", result);
    }
}
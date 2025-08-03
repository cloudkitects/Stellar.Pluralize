[![GitHub license](https://img.shields.io/github/license/cloudkitects/Stellar.Pluralize.svg)](https://github.com/cloudkitects/Stellar.Pluralize/blob/master/LICENSE.md)

# Stellar.Pluralize
A .NET 9.0 refactor of Sarath KCM's [Pluralize.NET](https://github.com/sarathkcm/Pluralize.NET) library, in turn, a C# port of Blake Embrey's [pluralize](https://github.com/blakeembrey/pluralize), a library for pluralizing and singularizing almost any English word based on built-in and optional user-defined rules for plural, singular, irregular and uncountable words.

This is a *breaking* version not compatible with previous versions, including the removal of the interface and the abstract base class. On the other hand, it takes advantage of generated regex code for improved performance, and it includes string pluralization, singularization and formatting extensions that make it easier to use and preserve casing.

_Inflection_ rules take in regular expresison or string patterns with options and a replacement string with $_n_ interpolation placeholders for matches.

# Usage

```cs
var pluralizer = new Pluralizer();

pluralizer.Pluralize("House");   // Houses
pluralizer.Singularize("Geese"); // Goose

pluralizer.IsPlural("plates"); // true
pluralizer.IsSingular("cats"); // false

"dog".Pluralize();         \\ "dogs"
"House".Pluralize();       \\ "Houses"
"SUITES".Singularize();    \\ "SUITE"
"kitchenEtTe".Pluralize(); \\ "kitchenEtTes"

// plural rules
pluralizer.AddPlural("gex", "gexii");

pluralizer.Pluralize("regex"); // regexii, not regexes

// symmetric rules
pluralizer.AddPlural("(a|si)ngle", "$1ngular");
pluralizer.AddSingular("(a|si)ngular", "$1ngle");

pluralizer.Pluralize("single");    // singular, not singles
pluralizer.Singularize("angular"); // angle

// irregular (assymetric) rules
pluralizer.AddorUpdateIrregular("person", "persons");

pluralizer.Pluralize("person");    // persons, not people
pluralizer.Singularize("persons"); // person
pluralizer.Singularize("people");  // person

// uncountables
pluralizer.AddUncountable("paper");

pluralizer.Pluralize("paper"); // paper, not papers
 
 // generic (placeholder) rules
pluralizer.AddPlural(new Regex(@"(\w+)([-\w]+)+", RegexOptions.IgnoreCase),  "$1s$2");
pluralizer.AddSingular(/* language=regex */ @"(\w+)s([-\w]+)+", "$1$2");

pluralizer.Pluralize("cul-de-sac");       // culs-de-sac, not cul-de-sacs
pluralizer.Singularize("mothers-in-law"); // mother-in-law
pluralizer.Pluralize("one-half");         // ones-half

// less generic rules
pluralizer.AddPlural(  /* language=regex */ @"((cul|mother)(-de-sac|-in-law))",  "$2s$3");
pluralizer.AddSingular(new Regex(@"((cul|mother)s(-de-sac|-in-law))"), "$2$3");

pluralizer.Pluralize("cul-de-sac");       // culs-de-sac
pluralizer.Singularize("mothers-in-law"); // mother-in-law
pluralizer.Pluralize("one-half");         // one-halves

// invalid regex patterns
new Rule(/* language=regex */ "([^a-z).*", ""); // throws
_pluralizer.AddPlural("a|si)ngle", "$1ngular"); // throws
_pluralizer.AddUncountable("(|bogus.*");        // throws

"dog".Inflect(7);         // dogs
"dogs".Inflect(1, "N0");  // 1 dog
"cats".Inflect(0);        // cats
"cat".Inflect(0, "N0");   // 0 cats

"house".Inflect(5000);               // 5000 houses
"house".Inflect(5000, "N0");         // 5,000 houses
"respondent".Inflect(0.4475, "P2");  // 44.75% respondents
```

## Sentences

The library can support basic sentence inflection:

```cs
    [Fact]
    public void Phrases()
    {
        var boilerplate = "The quick brown fox jumps over the lazy dog.";

        var nouns = new string[] { "fox", "dog" };
        var verbs = new string[] { "jumps" };
        
        var result = boilerplate;

        foreach (var noun in nouns)
        {
            result = result.Replace(noun, noun.Inflect());
        }

        foreach (var verb in verbs)
        {
            result = result.Replace(verb, verb.Inflect(false));
        }

        Assert.Equal("The quick brown foxes jump over the lazy dogs.", result);
    }
```

The challenge is parsing sentence structure and infering verb conjugation.

# Licence
[MIT](https://github.com/cloudkitects/Stellar.Pluralize/blob/master/LICENSE.md).

# Contributors ✨
This project follows the [all-contributors](https://github.com/all-contributors/all-contributors) specification. Contributions of any kind welcome!

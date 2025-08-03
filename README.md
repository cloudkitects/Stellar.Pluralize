[![GitHub license](https://img.shields.io/github/license/cloudkitects/Stellar.Pluralize.svg)](https://github.com/cloudkitects/Stellar.Pluralize/blob/master/LICENSE.md) [![Coverage](https://coveralls.io/repos/github/cloudkitects/Stellar.Pluralize/badge.svg?branch=main&v=1)](https://coveralls.io/github/cloudkitects/Stellar.Pluralize?branch=main)  [![NuGet](https://img.shields.io/nuget/v/Stellar.Pluralize.svg)](https://www.nuget.org/packages/Stellar.Pluralize/) [![NuGet](https://img.shields.io/nuget/dt/Stellar.Pluralize.svg)](https://www.nuget.org/packages/Stellar.Pluralize/)

# Stellar.Pluralize
A .NET 9.0 refactor of Sarath KCM's [Pluralize.NET](https://github.com/sarathkcm/Pluralize.NET) library, in turn, a C# port of Blake Embrey's [pluralize](https://github.com/blakeembrey/pluralize), a library for pluralizing and singularizing almost any English word based on built-in and optional user-defined rules for plural, singular, irregular and uncountable words.

This is a *breaking* version not compatible with previous versions, including the removal of the interface and the abstract base class. On the other hand, it takes advantage of generated regex code for improved performance, and it includes string pluralization, singularization and formatting extensions that make it easier to use and preserve casing.

_Inflection_ rules take in `Regex` (regular expression) objects or string patterns and `Regex` options and a replacement string with optional $_n_ interpolation placeholders to take advantage of `Regex.Match` evaluation.

# Usage

```cs
var pluralizer = new Pluralizer();

pluralizer.Pluralize("House");   // Houses
pluralizer.Singularize("Geese"); // Goose

pluralizer.IsPlural("plates"); // true
pluralizer.IsSingular("cats"); // false

"dog".Pluralize();         // "dogs"
"House".Pluralize();       // "Houses"
"SUITES".Singularize();    // "SUITE"
"kitchenEtTe".Pluralize(); // "kitchenEtTes"

pluralizer.AddPlural("gex", "gexii");
pluralizer.Pluralize("regex"); // regexii, not regexes
```

## Symmetric rules

Symmetric rules complement each other--are bidirectional.

```cs
pluralizer.AddPlural("(a|si)ngle", "$1ngular");
pluralizer.AddSingular("(a|si)ngular", "$1ngle");

pluralizer.Pluralize("single");    // singular, not singles
pluralizer.Singularize("angular"); // angle
```

## Asymmetric rules

Asymmetric (1:_n_ or _n_:1) rules result from rule overrides:

```cs
pluralizer.AddorUpdateIrregular("person", "persons"); // originally person <=> people

pluralizer.Pluralize("person");    // persons
pluralizer.Singularize("persons"); // person
pluralizer.Singularize("people");  // person
```

## Uncountable words

```cs
pluralizer.AddUncountable("paper");
pluralizer.Pluralize("paper");    // paper
pluralizer.Pluralize("firmware"); // firmware
```

## Generic rules

Generic rules leverage capture groups:

```cs
pluralizer.AddPlural(new Regex(@"(\w+)([-\w]+)+", RegexOptions.IgnoreCase),  "$1s$2");
pluralizer.AddSingular("(\w+)s([-\w]+)+", "$1$2");

pluralizer.Pluralize("cul-de-sac");       // culs-de-sac, not cul-de-sacs
pluralizer.Singularize("mothers-in-law"); // mother-in-law
pluralizer.Pluralize("one-half");         // ones-half

// less generic...
pluralizer.AddPlural("((cul|mother)(-de-sac|-in-law))",  "$2s$3");
pluralizer.AddSingular(new Regex(@"((cul|mother)s(-de-sac|-in-law))"), "$2$3");

pluralizer.Pluralize("cul-de-sac");       // culs-de-sac
pluralizer.Singularize("mothers-in-law"); // mother-in-law
pluralizer.Pluralize("one-half");         // one-halves
```

## Extensions

```cs
"dog".Inflect(7);         // dogs
"dogs".Inflect(1, "N0");  // 1 dog
"cats".Inflect(0);        // cats
"cat".Inflect(0, "N0");   // 0 cats

"house".Inflect(5000);               // 5000 houses
"house".Inflect(5000, "N0");         // 5,000 houses
"respondent".Inflect(0.4475, "P2");  // 44.75% respondents
```

## Sentences

A new set of rules can be created to handle verb conjugation, yet the `Inflect` extension method can be used to achieve basic sentence inflection:

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

## Exceptions

```cs
var rule = new Rule("([^a-z).*", "");          // throws
pluralizer.AddPlural("a|si)ngle", "$1ngular"); // throws
pluralizer.AddUncountable("(|bogus.*");        // throws
```

# Licence
[MIT](https://github.com/cloudkitects/Stellar.Pluralize/blob/master/LICENSE.md).

# Contributors ✨
This project follows the [all-contributors](https://github.com/all-contributors/all-contributors) specification. Contributions of any kind welcome!

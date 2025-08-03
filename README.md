# Stellar.Pluralize
This is a **breaking** .NET 6.0 refactor of Sarath KCM's [Pluralize.NET](https://github.com/sarathkcm/Pluralize.NET) library.

This library pluralizes or singularizes almost any English word based on built-in and/or user-defined rules for plural, singular, irregular and uncountable words.

Rule add or update helpers take in regex patterns and a replacement string with `$n` interpolation placeholders for matches, and will throw for invalid regex patterns.

# Usage
```C#
IPluralize pluralizer = new Pluralizer();

pluralizer.Pluralize("House");   // Houses
pluralizer.Singularize("Geese"); // Goose

// test words
pluralizer.IsPlural("plates"); // true
pluralizer.IsSingular("cats"); // false

// format a word based on count
pluralizer.Format(7, "dog");  // 7 dogs
pluralizer.Format(1, "dogs"); // 1 dog
pluralizer.Format(0, "cat");  // 0 dogs... jk, 0 cats :)
```
## Format or remove count
```C#
pluralizer.Format(5000, "house");       // 5000 houses
pluralizer.Format(5000, "house", "N0"); // 5,000 houses
pluralizer.Format(11, "person", "N2");  // 11.00 people

// format a word without the count 
pluralizer.Format(5000, "house", null); // houses
pluralizer.Format(5000, "house", "");   // houses
pluralizer.Format(1, "houses", " ");    // house
```

## Adding rules
```C#
// add a plural rule
pluralizer.AddPlural("gex", "gexii");
pluralizer.Pluralize("regex"); // regexii, not regexes

## Symmetric rules

Rules are symmetric (bidirectional) by default

```cs
pluralizer.AddPlural("(a|si)ngle", "$1ngular");
pluralizer.AddSingular("(a|si)ngular", "$1ngle");
pluralizer.Pluralize("single");    // singular, not singles
pluralizer.Singularize("angular"); // angle

// add or update an irregular
pluralizer.AddorUpdateIrregular("person", "persons");
pluralizer.Pluralize("person");    // persons, not people
pluralizer.Singularize("persons"); // person
pluralizer.Singularize("people");  // person

// add an uncountable
pluralizer.AddUncountable("paper");
pluralizer.Pluralize("paper"); // paper, not papers
```

## Adding generic rules
```C#
pluralizer.AddPlural(  /* language=regex */ @"(\w+)([-\w]+)+",  "$1s$2");
pluralizer.AddSingular(/* language=regex */ @"(\w+)s([-\w]+)+", "$1$2");
pluralizer.Pluralize("cul-de-sac");       // culs-de-sac, not cul-de-sacs
pluralizer.Singularize("mothers-in-law"); // mother-in-law
pluralizer.Pluralize("one-half");         // ones-half
```

## Adding less generic rules
```C#
pluralizer.AddPlural(  /* language=regex */ @"((cul|mother)(-de-sac|-in-law))",  "$2s$3");
pluralizer.AddSingular(/* language=regex */ @"((cul|mother)s(-de-sac|-in-law))", "$2$3");
pluralizer.Pluralize("cul-de-sac");       // culs-de-sac
pluralizer.Singularize("mothers-in-law"); // mother-in-law
pluralizer.Pluralize("one-half");         // one-halves
```

## Exceptions
```C#
// invalid regex patterns throw a RegexParseException
new Rule(/* language=regex */ "([^a-z).*", ""); // throws
_pluralizer.AddPlural("a|si)ngle", "$1ngular"); // throws
_pluralizer.AddUncountable("(|bogus.*");        // throws
```

# Licence
[MIT](https://github.com/cloudkitects/Stellar.Pluralize/blob/master/LICENCE) - because the original project is MIT

# Contributors ✨
This project follows the [all-contributors](https://github.com/all-contributors/all-contributors) specification. Contributions of any kind welcome!

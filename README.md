# Stellar.Pluralize
This is a **breaking** .NET 6.0 refactor of Sarath KCM's [Pluralize.NET](https://github.com/sarathkcm/Pluralize.NET) library.

This library pluralizes or singularizes almost any English word based on built-in, default rule sets for plural, singular, irregular and uncountable words.

You can add rules to the rule sets or pass in new rule sets to the pluralizer constructors.

The add or update helpers take in regex strings and optional regex options and will throw for invalid regex patterns.

Interpolated $0, $1, etc. literals in rule replacement values are shortcuts to the match, tyoically $0 for the entire word, $1 for the root.

# Usage
```C#
IPluralize pluralizer = new Pluralizer();

pluralizer.Pluralize("House");   // Houses
pluralizer.Singularize("Geese"); // Goose

// add a plural rule
pluralizer.AddPlural("gex", "gexii");
pluralizer.Pluralize("regex"); // regexii, not regexes

// add two-way rules
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

// invalid regex ptterns throw a RegexParseException
new Rule(/* language=regex */ "([^a-z).*", ""); // throws
_pluralizer.AddPlural("a|si)ngle", "$1ngular"); // throws
_pluralizer.AddUncountable("(|bogus.*");        // throws

// test a word's count
pluralizer.IsPlural("test");   // false
pluralizer.IsSingular("test"); // true

// format a word based on count
pluralizer.Format(7, "dog");  // 7 dogs
pluralizer.Format(1, "dogs"); // 1 dog
pluralizer.Format(0, "cat");  // 0 dogs... jk, 0 cats :)

// format a word based on count with format
pluralizer.Format(5000, "house");       // 5000 houses
pluralizer.Format(5000, "house", "N0"); // 5,000 houses
pluralizer.Format(11, "person", "N2");  // 11.00 people

// format a word without the count 
pluralizer.Format(5000, "house", null); // houses
pluralizer.Format(5000, "house", "");   // houses
pluralizer.Format(1, "houses", " ");    // house
```

# Licence
[MIT](https://github.com/cloudkitects/Stellar.Pluralize/blob/master/LICENCE) - because the original project is MIT

# Contributors ✨
This project follows the [all-contributors](https://github.com/all-contributors/all-contributors) specification. Contributions of any kind welcome!

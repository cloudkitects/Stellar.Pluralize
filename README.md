# Stellar.Pluralize
This is a **breaking** .NET 6.0 refactor of Sarath KCM's [Pluralize.NET](https://github.com/sarathkcm/Pluralize.NET) library.

This library pluralizes or singularizes almost any English word based on built-in, default rule sets for plural, singular, irregular and uncountable words. The sets are both partially extensible and completely overridable.

# Usage
```C#
IPluralize pluralizer = new Pluralizer();

pluralizer.Pluralize("House");     // returns "Houses"
pluralizer.Singularize("Geese");  // returns "Goose"

// add a plural rule
pluralizer.AddPlural("gex", "gexii");
pluralizer.Pluralize("regex"); // returns "regexii", not "regexes"

// add a singular rule
pluralizer.AddSingular("singles", "singular");
pluralizer.Singularize("singles"); // returns "singular", not "singles"

// add or update an irregular
pluralizer.AddorUpdateIrregular("person", "persons");
pluralizer.Pluralize("person");    // returns "persons", not "people"
pluralizer.Singularize("persons"); // returns "person"
pluralizer.Singularize("people");  // returns "person"

// add an uncountable
pluralizer.AddUncountable("paper");
pluralizer.Pluralize("paper"); // returns "paper", not "papers"

// test a word
pluralizer.IsPlural("test"); // returns false
pluralizer.IsSingular("test"); // returns true

// format a word based on count
pluralizer.Format(1, "dogs"); //  returns "1 dog"

// format a word based on count with format
pluralizer.Format(5000, "house"); //  returns "5000 houses" (format default = "G")
pluralizer.Format(5000, "house", "N0"); //  returns "5,000 houses"
pluralizer.Format(11, "person", "N2"); //  returns "11.00 people"

// format a word without the count with null, empty or whitespace format 
pluralizer.Format(5000, "house", null); //  returns "houses"
pluralizer.Format(5000, "house", ""); //  returns "houses"
pluralizer.Format(5000, "house", " "); //  returns "houses"
```

# Licence
[MIT](https://github.com/cloudkitects/Stellar.Pluralize/blob/master/LICENCE) - because the original project is MIT

# Contributors ✨
This project follows the [all-contributors](https://github.com/all-contributors/all-contributors) specification. Contributions of any kind welcome!

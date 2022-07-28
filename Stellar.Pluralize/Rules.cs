namespace Stellar.Pluralize
{
    public static class Rules
    {
        /// <summary>
        /// A list of pluralization rules ordered by descending genericness.
        /// </summary>
        public static readonly List<Rule> Plurals = new()
        { 
                new Rule(/* language=regex */ "s?$", "s" ),
                new Rule(/* language=regex */ "[^\u0000-\u007F]$",  "$0" ),
                new Rule(/* language=regex */ "([^aeiou]ese)$", "$1" ),
                new Rule(/* language=regex */ "(ax|test)is$",  "$1es" ),
                new Rule(/* language=regex */ "(alias|[^aou]us|t[lm]as|gas|ris)$",  "$1es" ),
                new Rule(/* language=regex */ "(e[mn]u)s?$",  "$1s" ),
                new Rule(/* language=regex */ "([^l]ias|[aeiou]las|[ejzr]as|[iu]am)$",  "$1" ),
                new Rule(/* language=regex */ "(alumn|syllab|vir|radi|nucle|fung|cact|stimul|termin|bacill|foc|uter|loc|strat)(?:us|i)$", "$1i" ),
                new Rule(/* language=regex */ "(alumn|alg|vertebr)(?:a|ae)$", "$1ae" ),
                new Rule(/* language=regex */ "(seraph|cherub)(?:im)?$", "$1im" ),
                new Rule(/* language=regex */ "(her|at|gr)o$", "$1oes" ),
                new Rule(/* language=regex */ "(agend|addend|millenni|dat|extrem|bacteri|desiderat|strat|candelabr|errat|ov|symposi|curricul|automat|quor)(?:a|um)$",  "$1a" ),
                new Rule(/* language=regex */ "(apheli|hyperbat|periheli|asyndet|noumen|phenomen|criteri|organ|prolegomen|hedr|automat)(?:a|on)$",  "$1a" ),
                new Rule(/* language=regex */ "sis$", "ses" ),
                new Rule(/* language=regex */ "(?:(kni|wi|li)fe|(ar|l|ea|eo|oa|hoo)f)$",  "$1$2ves" ),
                new Rule(/* language=regex */ "([^aeiouy]|qu)y$",  "$1ies" ),
                new Rule(/* language=regex */ "([^ch][ieo][ln])ey$",  "$1ies" ),
                new Rule(/* language=regex */ "(x|ch|ss|sh|zz)$",  "$1es" ),
                new Rule(/* language=regex */ "(matr|cod|mur|sil|vert|ind|append)(?:ix|ex)$",  "$1ices" ),
                new Rule(/* language=regex */ "\\b((?:tit)?m|l)(?:ice|ouse)$",  "$1ice" ),
                new Rule(/* language=regex */ "(pe)(?:rson|ople)$",  "$1ople" ),
                new Rule(/* language=regex */ "(child)(?:ren)?$",  "$1ren" ),
                new Rule(/* language=regex */ "eaux$",  "$0" ),
                new Rule(/* language=regex */ "m[ae]n$", "men" ),
                new Rule(/* language=regex */ "^thou$", "you" ),
                new Rule(/* language=regex */ "pox$", "$0" ),
                new Rule(/* language=regex */ "o[iu]s$", "$0" ),
                new Rule(/* language=regex */ "deer$", "$0" ),
                new Rule(/* language=regex */ "fish$", "$0" ),
                new Rule(/* language=regex */ "sheep$", "$0" ),
                new Rule(/* language=regex */ "measles$/", "$0" ),
                new Rule(/* language=regex */ "[^aeiou]ese$", "$0")
        };

        /// <summary>
        /// A list of singularization rules ordered by descending genericness.
        /// </summary>
        public static readonly List<Rule> Singulars = new()
        {
            new Rule(/* language=regex */ "s$", ""),
            new Rule(/* language=regex */ "(ss)$", "$1"),
            new Rule(/* language=regex */ "(wi|kni|(?:after|half|high|low|mid|non|night|[^\\w]|^)li)ves$", "$1fe"),
            new Rule(/* language=regex */ "(ar|(?:wo|[ae])l|[eo][ao])ves$", "$1f"),
            new Rule(/* language=regex */ "ies$", "y"),
            new Rule(/* language=regex */ "\\b([pl]|zomb|(?:neck|cross)?t|coll|faer|food|gen|goon|group|lass|talk|goal|cut)ies$", "$1ie" ),
            new Rule(/* language=regex */ "\\b(mon|smil)ies$", "$1ey"),
            new Rule(/* language=regex */ "\\b((?:tit)?m|l)ice$", "$1ouse"),
            new Rule(/* language=regex */ "(seraph|cherub)im$", "$1"),
            new Rule(/* language=regex */ "(x|ch|ss|sh|zz|tto|go|cho|alias|[^aou]us|t[lm]as|gas|(?:her|at|gr)o|[aeiou]ris)(?:es)?$", "$1"),
            new Rule(/* language=regex */ "(analy|diagno|parenthe|progno|synop|the|empha|cri|ne)(?:sis|ses)$", "$1sis"),
            new Rule(/* language=regex */ "(movie|twelve|abuse|e[mn]u)s$", "$1"),
            new Rule(/* language=regex */ "(test)(?:is|es)$", "$1is"),
            new Rule(/* language=regex */ "(alumn|syllab|octop|vir|radi|nucle|fung|cact|stimul|termin|bacill|foc|uter|loc|strat)(?:us|i)$", "$1us"),
            new Rule(/* language=regex */ "(agend|addend|millenni|dat|extrem|bacteri|desiderat|strat|candelabr|errat|ov|symposi|curricul|quor)a$", "$1um"),
            new Rule(/* language=regex */ "(apheli|hyperbat|periheli|asyndet|noumen|phenomen|criteri|organ|prolegomen|hedr|automat)a$", "$1on"),
            new Rule(/* language=regex */ "(alumn|alg|vertebr)ae$", "$1a"),
            new Rule(/* language=regex */ "(cod|mur|sil|vert|ind)ices$", "$1ex"),
            new Rule(/* language=regex */ "(matr|append)ices$", "$1ix"),
            new Rule(/* language=regex */ "(pe)(rson|ople)$", "$1rson"),
            new Rule(/* language=regex */ "(child)ren$", "$1"),
            new Rule(/* language=regex */ "(eau)x?$", "$1"),
            new Rule(/* language=regex */ "men$", "man" ),
            new Rule(/* language=regex */ "[^aeiou]ese$", "$0"),
            new Rule(/* language=regex */ "deer$", "$0"),
            new Rule(/* language=regex */ "fish$", "$0"),
            new Rule(/* language=regex */ "measles$", "$0"),
            new Rule(/* language=regex */ "o[iu]s$", "$0"),
            new Rule(/* language=regex */ "pox$", "$0"),
            new Rule(/* language=regex */ "sheep$", "$0")
        };

        /// <summary>
        /// A collection of words with no plurals.
        /// </summary>
        public static readonly ICollection<string> Uncountables = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        { 
            "adulthood",
            "advice",
            "agenda",
            "aid",
            "aircraft",
            "alcohol",
            "ammo",
            "anime",
            "athletics",
            "audio",
            "bison",
            "blood",
            "bream",
            "buffalo",
            "butter",
            "carp",
            "cash",
            "chassis",
            "chess",
            "clothing",
            "cod",
            "commerce",
            "cooperation",
            "corps",
            "debris",
            "diabetes",
            "digestion",
            "elk",
            "energy",
            "equipment",
            "excretion",
            "expertise",
            "firmware",
            "flounder",
            "fun",
            "gallows",
            "garbage",
            "graffiti",
            "headquarters",
            "health",
            "herpes",
            "highjinks",
            "homework",
            "housework",
            "information",
            "jeans",
            "justice",
            "kudos",
            "labour",
            "literature",
            "machinery",
            "mackerel",
            "mail",
            "media",
            "mews",
            "moose",
            "music",
            "mud",
            "manga",
            "news",
            "only",
            "personnel",
            "pike",
            "plankton",
            "pliers",
            "police",
            "pollution",
            "premises",
            "rain",
            "research",
            "rice",
            "salmon",
            "scissors",
            "series",
            "sewage",
            "shambles",
            "shrimp",
            "software",
            "species",
            "staff",
            "swine",
            "tennis",
            "traffic",
            "transportation",
            "trout",
            "tuna",
            "wealth",
            "welfare",
            "whiting",
            "wildebeest",
            "wildlife",
            "you"
        };

        /// <summary>
        /// A dictionary of English irregular pluralization rules.
        /// </summary>
        public static readonly Dictionary<string, string> Irregulars = new (StringComparer.OrdinalIgnoreCase)
        {
            #region pronouns
            {"I", "we"},
            {"me", "us"},
            {"he", "they"},
            {"she", "they"},
            {"them", "them"},
            {"myself", "ourselves"},
            {"yourself", "yourselves"},
            {"itself", "themselves"},
            {"herself", "themselves"},
            {"himself", "themselves"},
            {"themself", "themselves"},
            {"is", "are"},
            {"was", "were"},
            {"has", "have"},
            {"this", "these"},
            {"that", "those"},
            #endregion

            #region words ending in with a consonant and `o`
            {"echo", "echoes"},
            {"dingo", "dingoes"},
            {"volcano", "volcanoes"},
            {"tornado", "tornadoes"},
            {"torpedo", "torpedoes"},
            #endregion

            #region ends with `us`
            {"genus", "genera"},
            {"viscus", "viscera"},
            #endregion

            #region ends with `ma`
            {"stigma", "stigmata"},
            {"stoma", "stomata"},
            {"dogma", "dogmata"},
            {"lemma", "lemmata"},
            {"schema", "schemata"},
            {"anathema", "anathemata"},
            #endregion

            #region other
            {"ox", "oxen"},
            {"axe", "axes"},
            {"die", "dice"},
            {"yes", "yeses"},
            {"foot", "feet"},
            {"eave", "eaves"},
            {"goose", "geese"},
            {"tooth", "teeth"},
            {"quiz", "quizzes"},
            {"human", "humans"},
            {"proof", "proofs"},
            {"carve", "carves"},
            {"valve", "valves"},
            {"looey", "looies"},
            {"thief", "thieves"},
            {"groove", "grooves"},
            {"pickaxe", "pickaxes"},
            {"passerby","passersby" },
            {"cookie","cookies" }
            #endregion
        };

        /// <summary>
        /// The inverse of the above after ensuring uniqueness.
        /// </summary>
        public static Dictionary<string, string> IrregularPlurals => Irregulars
            .Where(i => !new string[] { "he", "itself", "herself", "himself" }.Contains(i.Key))
            .ToDictionary(i => i.Value, i => i.Key);
    }
}

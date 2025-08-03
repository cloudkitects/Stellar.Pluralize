using System.Text.RegularExpressions;

namespace Stellar.Pluralize;

public static partial class Rules
{
    #region plurals
    [GeneratedRegex("s?$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p01();
    
    [GeneratedRegex("[^\u0000-\u007F]$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p02();
    
    [GeneratedRegex("([^aeiou]ese)$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p03();
    
    [GeneratedRegex("(ax|test)is$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p04();
    
    [GeneratedRegex("(alias|[^aou]us|t[lm]as|gas|ris)$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p05();
    
    [GeneratedRegex("(e[mn]u)s?$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p06();
    
    [GeneratedRegex("([^l]ias|[aeiou]las|[ejzr]as|[iu]am)$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p07();
    
    [GeneratedRegex("(alumn|syllab|vir|radi|nucle|fung|cact|stimul|termin|bacill|foc|uter|loc|strat)(?:us|i)$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p08();
    
    [GeneratedRegex("(alumn|alg|vertebr)(?:a|ae)$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p09();
    
    [GeneratedRegex("(seraph|cherub)(?:im)?$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p10();
    
    [GeneratedRegex("(her|at|gr)o$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p11();
    
    [GeneratedRegex("(agend|addend|millenni|dat|extrem|bacteri|desiderat|strat|candelabr|errat|ov|symposi|curricul|automat|quor)(?:a|um)$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p12();
    
    [GeneratedRegex("(apheli|hyperbat|periheli|asyndet|noumen|phenomen|criteri|organ|prolegomen|hedr|automat)(?:a|on)$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p13();
    
    [GeneratedRegex("sis$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p14();
    
    [GeneratedRegex("(?:(kni|wi|li)fe|(ar|l|ea|eo|oa|hoo)f)$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p15();
    
    [GeneratedRegex("([^aeiouy]|qu)y$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p16();
    
    [GeneratedRegex("([^ch][ieo][ln])ey$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p17();
    
    [GeneratedRegex("(x|ch|ss|sh|zz)$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p18();
    
    [GeneratedRegex("(matr|cod|mur|sil|vert|ind|append)(?:ix|ex)$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p19();
    
    [GeneratedRegex("\\b((?:tit)?m|l)(?:ice|ouse)$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p20();
    
    [GeneratedRegex("(pe)(?:rson|ople)$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p21();
    
    [GeneratedRegex("(child)(?:ren)?$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p22();
    
    [GeneratedRegex("eaux$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p23();
    
    [GeneratedRegex("m[ae]n$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p24();
    
    [GeneratedRegex("^thou$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p25();
    
    [GeneratedRegex("pox$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p26();
    
    [GeneratedRegex("o[iu]s$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p27();
    
    [GeneratedRegex("deer$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p28();
    
    [GeneratedRegex("fish$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p29();
    
    [GeneratedRegex("sheep$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p30();
    
    [GeneratedRegex("measles$/", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p31();
    
    [GeneratedRegex("[^aeiou]ese$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex p32();
    
    public static readonly List<Rule> Plurals =
    [
        new Rule(p01(), "s" ),
        new Rule(p02(), "$0" ),
        new Rule(p03(), "$1" ),
        new Rule(p04(), "$1es" ),
        new Rule(p05(), "$1es" ),
        new Rule(p06(), "$1s" ),
        new Rule(p07(), "$1" ),
        new Rule(p08(), "$1i" ),
        new Rule(p09(), "$1ae" ),
        new Rule(p10(), "$1im" ),
        new Rule(p11(), "$1oes" ),
        new Rule(p12(), "$1a" ),
        new Rule(p13(), "$1a" ),
        new Rule(p14(), "ses" ),
        new Rule(p15(), "$1$2ves" ),
        new Rule(p16(), "$1ies" ),
        new Rule(p17(), "$1ies" ),
        new Rule(p18(), "$1es" ),
        new Rule(p19(), "$1ices" ),
        new Rule(p20(), "$1ice" ),
        new Rule(p21(), "$1ople" ),
        new Rule(p22(), "$1ren" ),
        new Rule(p23(), "$0" ),
        new Rule(p24(), "men" ),
        new Rule(p25(), "you" ),
        new Rule(p26(), "$0" ),
        new Rule(p27(), "$0" ),
        new Rule(p28(), "$0" ),
        new Rule(p29(), "$0" ),
        new Rule(p30(), "$0" ),
        new Rule(p31(), "$0" ),
        new Rule(p32(), "$0")
    ];
    #endregion

    #region singulars
    [GeneratedRegex("s$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s01();

    [GeneratedRegex("(ss)$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s02();

    [GeneratedRegex("(wi|kni|(?:after|half|high|low|mid|non|night|[^\\w]|^)li)ves$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s03();

    [GeneratedRegex("(ar|(?:wo|[ae])l|[eo][ao])ves$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s04();

    [GeneratedRegex("ies$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s05();

    [GeneratedRegex("\\b([pl]|zomb|(?:neck|cross)?t|coll|faer|food|gen|goon|group|lass|talk|goal|cut)ies$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s06();

    [GeneratedRegex("\\b(mon|smil)ies$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s07();

    [GeneratedRegex("\\b((?:tit)?m|l)ice$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s08();

    [GeneratedRegex("(seraph|cherub)im$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s09();

    [GeneratedRegex("(x|ch|ss|sh|zz|tto|go|cho|alias|[^aou]us|t[lm]as|gas|(?:her|at|gr)o|[aeiou]ris)(?:es)?$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s10();

    [GeneratedRegex("(analy|diagno|parenthe|progno|synop|the|empha|cri|ne)(?:sis|ses)$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s11();

    [GeneratedRegex("(movie|twelve|abuse|e[mn]u)s$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s12();

    [GeneratedRegex("(test)(?:is|es)$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s13();

    [GeneratedRegex("(alumn|syllab|octop|vir|radi|nucle|fung|cact|stimul|termin|bacill|foc|uter|loc|strat)(?:us|i)$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s14();

    [GeneratedRegex("(agend|addend|millenni|dat|extrem|bacteri|desiderat|strat|candelabr|errat|ov|symposi|curricul|quor)a$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s15();

    [GeneratedRegex("(apheli|hyperbat|periheli|asyndet|noumen|phenomen|criteri|organ|prolegomen|hedr|automat)a$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s16();

    [GeneratedRegex("(alumn|alg|vertebr)ae$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s17();

    [GeneratedRegex("(cod|mur|sil|vert|ind)ices$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s18();

    [GeneratedRegex("(matr|append)ices$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s19();

    [GeneratedRegex("(pe)(rson|ople)$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s20();

    [GeneratedRegex("(child)ren$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s21();

    [GeneratedRegex("(eau)x?$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s22();

    [GeneratedRegex("men$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s23();

    [GeneratedRegex("[^aeiou]ese$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s24();

    [GeneratedRegex("deer$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s25();

    [GeneratedRegex("fish$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s26();

    [GeneratedRegex("measles$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s27();

    [GeneratedRegex("o[iu]s$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s28();

    [GeneratedRegex("pox$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s29();

    [GeneratedRegex("sheep$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex s30();

        public static readonly List<Rule> Singulars =
    [
        new Rule(s01(), ""),
        new Rule(s02(), "$1"),
        new Rule(s03(), "$1fe"),
        new Rule(s04(), "$1f"),
        new Rule(s05(), "y"),
        new Rule(s06(), "$1ie" ),
        new Rule(s07(), "$1ey"),
        new Rule(s08(), "$1ouse"),
        new Rule(s09(), "$1"),
        new Rule(s10(), "$1"),
        new Rule(s11(), "$1sis"),
        new Rule(s12(), "$1"),
        new Rule(s13(), "$1is"),
        new Rule(s14(), "$1us"),
        new Rule(s15(), "$1um"),
        new Rule(s16(), "$1on"),
        new Rule(s17(), "$1a"),
        new Rule(s18(), "$1ex"),
        new Rule(s19(), "$1ix"),
        new Rule(s20(), "$1rson"),
        new Rule(s21(), "$1"),
        new Rule(s22(), "$1"),
        new Rule(s23(), "man" ),
        new Rule(s24(), "$0"),
        new Rule(s25(), "$0"),
        new Rule(s26(), "$0"),
        new Rule(s27(), "$0"),
        new Rule(s28(), "$0"),
        new Rule(s29(), "$0"),
        new Rule(s30(), "$0")
    ];
    #endregion

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

    private static readonly string[] irregularExceptions = ["he", "itself", "herself", "himself"];

    public static Dictionary<string, string> IrregularPlurals => Irregulars
        .Where(static entry => !irregularExceptions.Contains(entry.Key))
        .ToDictionary(entry => entry.Value, i => i.Key);
}

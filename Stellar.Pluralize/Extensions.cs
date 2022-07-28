using System.Text.RegularExpressions;

namespace Stellar.Pluralize
{
    internal static class Extensions
    {
        /// <summary>
        /// Title case exceptions as per Associated Press (AP) Stylebook.
        /// </summary>
        private static readonly List<string> TitleExceptions = new()
        {
            "a", "an", "the", "for", "and", "nor", "but", "or", "yet", "so"
        };

        /// <summary>
        /// To Title Invariant Case.
        /// </summary>
        /// <param name="value">A string value, e.g., "a string value, e.g. a string value".</param>
        /// <returns>The string value in title case, e.g., "A String Value, e.g. a string value".</returns>
        public static string ToTitleInvariant(this string value)
        {
            return Regex.Replace(value, @"(^\w\s|\b\w\w+)", match =>
            {
                var word = match.Value;

                return match.Index > 0 && TitleExceptions.Contains(word)
                    ? word
                    : word[0].ToString().ToUpperInvariant() + word[1..].ToLowerInvariant();
            });
        }

        /// <summary>
        /// To Title Case.
        /// </summary>
        /// <param name="value">A string value, e.g., "a string value, e.g. a string value".</param>
        /// <returns>The string value in title case, e.g., "A String Value, e.g. a string value".</returns>
        public static string ToTitle(this string value)
        {
            return Regex.Replace(value, @"(^\w\s|\b\w\w+)", match =>
            {
                var word = match.Value;

                return match.Index > 0 && TitleExceptions.Contains(word)
                    ? word
                    : word[0].ToString().ToUpper() + word[1..].ToLower();
            });
        }
    }
}

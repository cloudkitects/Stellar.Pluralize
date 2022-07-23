using System.Text.RegularExpressions;

namespace Stellar.Pluralize
{
    internal static class Extensions
    {
        /// <summary>
        /// To Title Case.
        /// </summary>
        /// <param name="value">A string value, e.g., "a string value".</param>
        /// <returns>The string value in title case, e.g., "A String Value".</returns>
        /// <remarks>
        /// Major style guides—such as the AP Stylebook, The Chicago Manual of Style,
        /// and the AMA Manual of Style—have specific rules on title capitalization.
        /// This defaults to none of them.
        /// </remarks>
        public static string ToTitle(this string value)
        {
            //return value[0].ToString().ToUpperInvariant() + value[1..];
            //return Regex.Replace(value, @"(?<=(^|[.!?])\s*)\w|\b\w(?=\w{2})", match => match.Value.ToUpperInvariant());
            return Regex.Replace(value, @"(?<=(^|[.!?]\s+))\w|\b\w", match => match.Value.ToUpperInvariant());
        }
    }
}

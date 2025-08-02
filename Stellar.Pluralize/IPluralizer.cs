using System.Text.RegularExpressions;

namespace Stellar.Pluralize
{
    public interface IPluralizer
    {
        string Pluralize(string word);
        
        string Singularize(string word);
        
        bool IsPlural(string word);
        
        bool IsSingular(string word);
        
        /// <remarks>
        /// TODO: word:format and other numeric count
        /// </remarks>
        string Format(string word, int count, string format = "G");
        
        void AddPlural(string regex, string plural, RegexOptions options = RegexOptions.IgnoreCase);
        
        void AddSingular(string regex, string singular, RegexOptions options = RegexOptions.IgnoreCase);
        
        void AddUncountable(string word);
        
        void AddOrUpdateIrregular(string singular, string plural);
    }
}
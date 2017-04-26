using System.Linq;

namespace StrategyCorps.SampleCode.WebApi.Helpers
{
    public static class StringHelpers
    {
        public static bool HasSpecialCharacters(string yourString)
        {
            return yourString.Any(ch => !char.IsLetterOrDigit(ch));
        }
    }
}
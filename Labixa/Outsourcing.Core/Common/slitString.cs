using System;

namespace Outsourcing.Core.Common
{
    public static class SlitString
    {
        public static string TruncateAtWord(this string input, int length)
        {
            if (input == null || input.Length < length)
                return input;
            var iNextSpace = input.LastIndexOf(" ", length, StringComparison.Ordinal);
            return $"{input.Substring(0, (iNextSpace > 0) ? iNextSpace : length).Trim()}...";
        }

    }
}

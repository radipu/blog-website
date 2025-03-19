using System.Text.RegularExpressions;

namespace My_Blog_Website.Helpers
{
    public static class URLHelper
    {
        public static string GeneratePostSlug(string title)
        {
            if (string.IsNullOrEmpty(title)) return string.Empty;

            // Step 1: Remove all punctuation except hyphens and spaces
            string cleaned = Regex.Replace(title,
                @"[\p{P}\p{S}]", "", // Removes all punctuation and symbols
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

            // Step 2: Format for URL
            return cleaned.Trim()
                       .Replace(" ", "-")
                       .Replace("--", "-")
                       .Trim('-')
                       .ToLowerInvariant();
        }

        public static string GenerateCategorySlug(string categories)
        {
            if (string.IsNullOrEmpty(categories)) return "uncategorized";
            return Regex.Replace(categories.ToLower(), @"[^a-z0-9\s-]", "")
                   .Replace(" ", "-")
                   .Replace(".", "")
                   .Replace(",", "")
                   .Replace("?", "")
                   .Replace("!", "")
                   .Replace("--", "-")
                   .Trim('-');
        }
    }
}

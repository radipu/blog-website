using System.Text.RegularExpressions;

namespace My_Blog_Website.Helpers
{
    public static class URLHelper
    {
        public static string GeneratePostSlug(string title)
        {
            if (string.IsNullOrEmpty(title)) return string.Empty;

            return Regex.Replace(title.ToLower(), @"[^a-z0-9\s-]", "")
                       .Replace(" ", "-")
                       .Replace(".", "")
                       .Replace(",", "")
                       .Replace("?", "")
                       .Replace("!", "")
                       .Replace("--", "-")
                       .Trim('-');
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

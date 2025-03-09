using System.Text.RegularExpressions;

namespace My_Blog_Website.Helpers
{
    public static class HtmlHelpers
    {
        public static string StripHtmlTruncate(string html, int maxLength = 200)
        {
            if (string.IsNullOrEmpty(html)) return "";

            // Strip HTML tags
            var strippedContent = Regex.Replace(html, "<[^>]+>", " ").Trim();

            // Truncate to avoid long text
            return strippedContent.Length > maxLength
                ? strippedContent.Substring(0, maxLength) + "..."
                : strippedContent;
        }
    }
}
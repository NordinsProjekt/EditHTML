namespace HTMLTagColorer;

public static class StringExtensions
{
    /// <summary>
    /// Calculates the length of an HTML tag starting from the specified index.
    /// Searches for the closing '>' character to determine the tag's length.
    /// </summary>
    /// <param name="htmlText">The HTML string to search within.</param>
    /// <param name="index">The starting index where the HTML tag begins (typically pointing to the '<' character).</param>
    /// <returns>
    /// The length of the HTML tag (number of characters from index to the closing '>'). 
    /// If no closing '>' is found, returns the starting index.
    /// </returns>
    /// <example>
    /// For the string "&lt;div class='test'&gt;", if index points to '&lt;', this returns 16 (the length of "&lt;div class='test'&gt;").
    /// </example>
    public static int GetHtmlTagLength(this string htmlText, int index)
    {
        var max = htmlText.Length;
        
        for (var i = index; i < max; i++)
        {
            if (htmlText[i] == '>')
            {
                return i - index;
            }
        }
        
        return index;
    }

    /// <summary>
    /// Extracts the keyword (tag name) from an HTML tag string.
    /// The keyword is the text between the opening '<' and the first space or '>'.
    /// </summary>
    /// <param name="htmlText">The HTML string containing the tag.</param>
    /// <param name="index">The starting index of the tag (typically pointing to the '<' character).</param>
    /// <returns>
    /// The HTML tag name (e.g., "div", "span", "p"). 
    /// Returns an empty string if no space is found before the end of the string.
    /// </returns>
    /// <example>
    /// For the string "&lt;div class='test'&gt;", this returns "div".
    /// </example>
    public static string GetHtmlKeyword(this string htmlText, int index)
    {
        var max = htmlText.Length;
        
        for (var i = 0; i < max; i++)
        {
            if (htmlText[i] == ' ') return htmlText.Substring(index + 1, i);
        }

        return "";
    }

    /// <summary>
    /// Parses an HTML tag and extracts all properties (attributes) and their values into a dictionary.
    /// Removes angle brackets and splits the tag by spaces to find attribute name-value pairs separated by '='.
    /// </summary>
    /// <param name="htmlTag">The HTML tag string to parse (e.g., "&lt;div class='test' id='main'&gt;" or "div class='test' id='main'").</param>
    /// <returns>
    /// A dictionary containing attribute names as keys and their corresponding values as values.
    /// Only attributes with name-value pairs (separated by '=') are included.
    /// Self-closing tags (containing '/') return an empty dictionary.
    /// Tags without attributes also return an empty dictionary.
    /// </returns>
    /// <remarks>
    /// The method performs the following steps:
    /// 1. Skips self-closing tags (checks for '/' character)
    /// 2. Trims and removes angle brackets '&lt;' and '&gt;'
    /// 3. Splits by spaces to separate individual attributes
    /// 4. For each attribute, splits by '=' to extract name and value
    /// 5. Only attributes with exactly a name and value are added to the result
    /// 
    /// Example: "&lt;div class='test' id='main'&gt;" returns {"class": "'test'", "id": "'main'"}
    /// Note: Attribute values retain their original quotes if present.
    /// </remarks>
    /// TODO: Add support for self-closing tags
    public static Dictionary<string, string> GetPropertiesWithValueFromHtmlTag(this string htmlTag)
    {
        if (htmlTag.Contains('/')) return new Dictionary<string, string>();
        
        var parsedText = htmlTag.Trim().Replace("<", "").Replace(">", "").Split(' ');
        if (parsedText.Length == 1) return new Dictionary<string, string>();

        var result = new Dictionary<string, string>();
        foreach (var prop in parsedText)
        {
            var values = prop.Split('=');
            if (values.Length == 2) result.TryAdd(values[0], values[1]); //This filters away tags without properties
        }

        return result;
    }
}

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
    /// The number of characters from the starting index to the closing '>' character (exclusive of the '>' itself). 
    /// If no closing '>' is found, returns the starting index value.
    /// </returns>
    /// <example>
    /// For "&lt;div class='test'&gt;" with index = 0, returns 17 (position of '>') - 0 = 17.
    /// If no '>' is found in "incomplete tag", returns the index value unchanged.
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
    /// Searches from the specified index until the first space character is found.
    /// </summary>
    /// <param name="htmlText">The HTML string containing the tag.</param>
    /// <param name="index">The starting index of the tag (typically pointing to the '<' character).</param>
    /// <returns>
    /// The HTML tag name extracted from (index + 1) to the first space character.
    /// Returns an empty string if no space is found in the entire string from the beginning.
    /// </returns>
    /// <remarks>
    /// Note: The loop starts from 'index' (changed from 0 in original code), but the substring 
    /// calculation uses (index + 1) to skip the '<' character and (i - index) for the length.
    /// This will return an empty string if no space exists anywhere in the string.
    /// </remarks>
    /// <example>
    /// For "&lt;div class='test'&gt;" with index = 0, searches from index 0 for first space at position 4,
    /// then returns Substring(1, 4-0) = Substring(1, 4) = "div ".
    /// </example>
    public static string GetHtmlKeyword(this string htmlText, int index)
    {
        var max = htmlText.Length;
        
        for (var i = index; i < max; i++)
        {
            if (htmlText[i] == ' ') return htmlText.Substring(index + 1, i-index);
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
    /// 1. Returns empty dictionary if the tag contains '/' (self-closing tags like "&lt;br /&gt;")
    /// 2. Trims whitespace and removes all '&lt;' and '&gt;' characters
    /// 3. Splits the result by spaces to get individual parts (tag name + attributes)
    /// 4. Returns empty dictionary if only one part exists (tag name without attributes)
    /// 5. Iterates through all parts and splits each by '=' to separate name and value
    /// 6. Uses TryAdd to add only attributes with exactly 2 parts (name=value format)
    /// 
    /// The first element in parsedText array will be the tag name, which is skipped
    /// since splitting it by '=' won't produce 2 parts.
    /// 
    /// Example: "&lt;div class='test' id='main'&gt;" returns {"class": "'test'", "id": "'main'"}
    /// Note: Attribute values retain their original quotes if present.
    /// 
    /// Limitations:
    /// - Does not handle attributes with spaces in values (e.g., data-value="hello world")
    /// - Does not handle boolean attributes without values (e.g., disabled, checked)
    /// - Does not handle closing tags properly if they contain '/'
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

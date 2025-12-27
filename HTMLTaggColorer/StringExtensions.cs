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
            if (htmlText[i] == ' ') return htmlText.Substring(index + 1, i-index-1);
            if (htmlText[i] == '>') return htmlText.Substring(index + 1, i-index-1);
        }

        return "";
    }

    /// <summary>
    /// Parses an HTML tag and extracts all properties (attributes) and their values into a dictionary.
    /// Handles attribute values that contain spaces (e.g., style="background: white;").
    /// </summary>
    /// <param name="htmlTag">The HTML tag string to parse (e.g., "&lt;div class='test' id='main'&gt;").</param>
    /// <returns>
    /// A dictionary containing attribute names as keys and their corresponding values as values.
    /// Only attributes with name-value pairs (separated by '=') are included.
    /// Closing tags (starting with '&lt;/') return an empty dictionary.
    /// Tags without attributes also return an empty dictionary.
    /// </returns>
    /// <remarks>
    /// The method handles:
    /// - Attribute values with spaces inside quotes (e.g., style="background: white;")
    /// - Both single and double quoted values
    /// - URLs containing slashes
    /// 
    /// Limitations:
    /// - Does not handle boolean attributes without values (e.g., disabled, checked)
    /// </remarks>
    public static Dictionary<string, string> GetPropertiesWithValueFromHtmlTag(this string htmlTag)
    {
        // Only skip closing tags (e.g., </div>), not tags with URLs containing '/'
        if (htmlTag.TrimStart().StartsWith("</")) return new Dictionary<string, string>();

        var cleanedTag = htmlTag.Trim().Replace("<", "").Replace(">", "");
        
        var result = new Dictionary<string, string>();
        var i = 0;
        
        // Skip the tag name (first word)
        while (i < cleanedTag.Length && cleanedTag[i] != ' ')
        {
            i++;
        }
        
        // Parse attributes
        while (i < cleanedTag.Length)
        {
            // Skip whitespace
            while (i < cleanedTag.Length && cleanedTag[i] == ' ')
            {
                i++;
            }
            
            if (i >= cleanedTag.Length) break;
            
            // Read attribute name
            var nameStart = i;
            while (i < cleanedTag.Length && cleanedTag[i] != '=' && cleanedTag[i] != ' ')
            {
                i++;
            }
            
            if (i >= cleanedTag.Length || cleanedTag[i] != '=')
            {
                // No '=' found, skip this part
                continue;
            }
            
            var attrName = cleanedTag.Substring(nameStart, i - nameStart);
            i++; // Skip '='
            
            if (i >= cleanedTag.Length) break;
            
            // Read attribute value
            string attrValue;
            if (cleanedTag[i] == '"' || cleanedTag[i] == '\'')
            {
                // Quoted value - find matching closing quote
                var quoteChar = cleanedTag[i];
                var valueStart = i;
                i++; // Skip opening quote
                
                while (i < cleanedTag.Length && cleanedTag[i] != quoteChar)
                {
                    i++;
                }
                
                if (i < cleanedTag.Length)
                {
                    i++; // Skip closing quote
                }
                
                attrValue = cleanedTag.Substring(valueStart, i - valueStart);
            }
            else
            {
                // Unquoted value - read until space
                var valueStart = i;
                while (i < cleanedTag.Length && cleanedTag[i] != ' ')
                {
                    i++;
                }
                attrValue = cleanedTag.Substring(valueStart, i - valueStart);
            }
            
            result.TryAdd(attrName, attrValue);
        }
        
        return result;
    }
}

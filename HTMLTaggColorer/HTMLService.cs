using HTMLTagColorer.Extensions;

namespace HTMLTagColorer;

public static class HtmlService
{
    public static void ParseHtml(string htmlText)
    {
        var max = htmlText.Length;

        for (var i = 0; i < max; i++)
        {
            if (htmlText[i] == '<')
            {
                var tagLength = htmlText.GetHtmlTagLength(i);
                var tag = htmlText.GetHtmlKeyword(i);

                var tagKeyword = tag.Split(' ')[0];

                Console.ForegroundColor = HtmlKeywords.DoesTagExist(tagKeyword) ? ConsoleColor.Green : ConsoleColor.White;
                
                // tagLength includes '<' to '>' distance
                // tag.Length is just the keyword (e.g., "head" = 4)
                // For "<head>": tagLength = 5, tag.Length = 4
                // A tag has properties if tagLength > tag.Length + 1 (accounting for '<')
                if (tagLength > tag.Length + 1)
                {
                    // Tag with extra content after the keyword
                    var props = htmlText.Substring(i, tagLength + 1).GetPropertiesWithValueFromHtmlTag();
                    
                    if (props.Count > 0)
                    {
                        // Tag with key=value properties: write tag name, then colored properties, then '>'
                        Console.Write(htmlText.Substring(i, tag.Length + 1));
                        
                        foreach (var key in props.Keys)
                        {
                            PrintTheColoredProps(key, props[key]);
                        }
                        //Console.SetCursorPosition(Console.GetCursorPosition().Left - 1, Console.GetCursorPosition().Top);
                        Console.ForegroundColor = HtmlKeywords.DoesTagExist(tagKeyword) ? ConsoleColor.Green : ConsoleColor.White;
                        Console.Write(">");
                    }
                    else
                    {
                        // Tag with content after keyword but no key=value properties (e.g., <!DOCTYPE html>)
                        // Write the entire tag as-is
                        Console.Write(htmlText.Substring(i, tagLength + 1));
                    }
                }
                else
                {
                    // Simple tag without properties: write the entire tag including '>'
                    Console.Write(htmlText.Substring(i, tagLength + 1));
                }

                // Reset color after writing the tag so text content is not colored
                Console.ResetColor();
                
                i += tagLength;
            }
            else
            {
                Console.Write(htmlText[i]);
            }
        }
    }

    public static void FindLoneHtmlTag()
    {

    }

    private static void PrintTheColoredProps(string key, string propsValue)
    {
        Console.ForegroundColor = HtmlKeywords.DoesPropertyExist(key) ? ConsoleColor.Yellow : ConsoleColor.White;
        Console.Write(" " + key);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("=");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(propsValue);
    }
}
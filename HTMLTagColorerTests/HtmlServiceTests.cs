using HTMLTagColorer;

namespace HTMLTagColorerTests;

public class HtmlServiceTests
{
    /// <summary>
    /// Simulates ParseHtml output logic to verify no characters are lost.
    /// This test replicates the exact loop logic from HtmlService.ParseHtml
    /// </summary>
    [Theory]
    [InlineData("<html></html>", "<html></html>")]
    [InlineData("<head></head>", "<head></head>")]
    [InlineData("<div>content</div>", "<div>content</div>")]
    [InlineData("<h1>Title</h1>", "<h1>Title</h1>")]
    [InlineData("<p>text</p><span>more</span>", "<p>text</p><span>more</span>")]
    [InlineData("<!DOCTYPE html>", "<!DOCTYPE html>")]
    public void ParseHtml_SimpleTags_NoCharactersLost(string input, string expected)
    {
        var output = SimulateParseHtml(input);
        Assert.Equal(expected, output);
    }

    [Theory]
    [InlineData("<html lang='en'></html>")]
    [InlineData("<div class='test'>content</div>")]
    [InlineData("<meta charset='UTF-8'>")]
    public void ParseHtml_TagsWithProperties_AllTagCharactersAccountedFor(string input)
    {
        var output = SimulateParseHtml(input);
        
        // Verify the output contains the tag start and end
        Assert.StartsWith("<", output);
        Assert.Contains(">", output);
    }

    [Theory]
    [InlineData("<html>\n  <head>\n  </head>\n</html>")]
    [InlineData("<!DOCTYPE html>\n<html>\n</html>")]
    public void ParseHtml_MultilineHtml_AllCharactersPreserved(string input)
    {
        var output = SimulateParseHtml(input);
        
        // Verify same length (no characters lost)
        Assert.Equal(input.Length, output.Length);
    }

    /// <summary>
    /// Tests that the loop counter correctly moves past the entire tag
    /// </summary>
    [Theory]
    [InlineData("<a>X", "X", 3)]  // After <a>, next char 'X' should be at effective position 3
    [InlineData("<div>Y", "Y", 5)]
    public void ParseHtml_AfterTag_NextCharacterIsCorrect(string input, string expectedNextChar, int expectedPosition)
    {
        // Verify the character after the tag is correctly identified
        Assert.Equal(expectedNextChar, input[expectedPosition].ToString());
        
        var output = SimulateParseHtml(input);
        Assert.Contains(expectedNextChar, output);
    }

    /// <summary>
    /// Simulates the exact logic of HtmlService.ParseHtml but outputs to a string instead of Console
    /// </summary>
    private static string SimulateParseHtml(string htmlText)
    {
        var output = new System.Text.StringBuilder();
        var max = htmlText.Length;

        for (var i = 0; i < max; i++)
        {
            if (htmlText[i] == '<')
            {
                var tagLength = htmlText.GetHtmlTagLength(i);
                var tag = htmlText.GetHtmlKeyword(i);

                // tagLength includes '<' to '>' distance
                // tag.Length is just the keyword (e.g., "head" = 4)
                // A tag has properties if tagLength > tag.Length + 1 (accounting for '<')
                if (tagLength > tag.Length + 1)
                {
                    // Tag with extra content after the keyword
                    var props = htmlText.Substring(i, tagLength + 1).GetPropertiesWithValueFromHtmlTag();
                    
                    if (props.Count > 0)
                    {
                        // Tag with key=value properties: write tag name, then properties, then '>'
                        output.Append(htmlText.Substring(i, tag.Length + 1));

                        foreach (var key in props.Keys)
                        {
                            output.Append(" " + key);
                            output.Append("=");
                            output.Append(props[key] + " ");
                        }
                        output.Append(">");
                    }
                    else
                    {
                        // Tag with content after keyword but no key=value properties (e.g., <!DOCTYPE html>)
                        output.Append(htmlText.Substring(i, tagLength + 1));
                    }
                }
                else
                {
                    // Simple tag without properties: write the entire tag including '>'
                    output.Append(htmlText.Substring(i, tagLength + 1));
                }

                i += tagLength;
            }
            else
            {
                output.Append(htmlText[i]);
            }
        }

        return output.ToString();
    }
}

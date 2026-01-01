using HTMLTagColorer.Extensions;

namespace HTMLTagColorerTests;

public class StringExtensionsTests
{
    [Theory]
    [InlineData("<html>", 0, 5)]
    [InlineData("<html class='id'>", 0, 16)]
    [InlineData("  <html>", 2, 5)]
    [InlineData("<!DOCTYPE html>", 0, 14)]
    [InlineData("<head>", 0, 5)]
    public void GetHtmlTagLength_StringWithTag_ReturnTagLength(string text, int index, int expected)
    {
        var result = text.GetHtmlTagLength(index);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("<html>", 0, "html")]
    [InlineData("<head>", 0, "head")]
    [InlineData("<main class='id'>", 0, "main")]
    [InlineData("  <div>", 2, "div")]
    [InlineData("<!DOCTYPE html>", 0, "!DOCTYPE")]
    [InlineData("</html>", 0, "/html")]
    [InlineData("<h1>", 0, "h1")]
    public void GetHtmlKeyword_StringWithTag_ReturnExactTagName(string text, int index, string expected)
    {
        var result = text.GetHtmlKeyword(index);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("<html>", 0)]
    [InlineData("<main class='id'>", 0)]
    [InlineData("  <div>", 2)]
    public void GetHtmlKeyword_StringWithTag_ReturnTag(string text, int index)
    {
        var result = text.GetHtmlKeyword(index);

        Assert.NotNull(result);
        Assert.Contains(result, text);
    }

    [Theory]
    [InlineData("<html test='4'>", "test", "'4'")]
    [InlineData("<main class='id'>", "class", "'id'")]
    [InlineData("<a href='https://example.com/page'>", "href", "'https://example.com/page'")]
    [InlineData("<img src='/images/logo.png'>", "src", "'/images/logo.png'")]
    [InlineData("<img style=\"background: white; padding: 2px;\">", "style", "\"background: white; padding: 2px;\"")]
    [InlineData("<div class=\"my-class\" style=\"color: red;\">", "style", "\"color: red;\"")]
    [InlineData("<div class=\"my-class\" style=\"color: red;\">", "class", "\"my-class\"")]
    public void GetPropertiesWithValueFromHtmlTag_StringWithTag_ReturnDictionaryWithPropAndValue(string text,
        string key, string expected)
    {
        var result = text.GetPropertiesWithValueFromHtmlTag();
        Assert.Equal(expected, result.GetValueOrDefault(key));
    }

    [Theory]
    [InlineData("</div>")]
    [InlineData("</html>")]
    [InlineData("</a>")]
    public void GetPropertiesWithValueFromHtmlTag_ClosingTag_ReturnsEmptyDictionary(string text)
    {
        var result = text.GetPropertiesWithValueFromHtmlTag();
        Assert.Empty(result);
    }

    [Theory]
    [InlineData("<html>", 0, "<html>")]
    [InlineData("<head>", 0, "<head>")]
    [InlineData("<div class='test'>", 0, "<div class='test'>")]
    [InlineData("<!DOCTYPE html>", 0, "<!DOCTYPE html>")]
    public void GetFullTag_UsingTagLengthAndSubstring_ReturnsCompleteTag(string text, int index, string expected)
    {
        var tagLength = text.GetHtmlTagLength(index);
        var result = text.Substring(index, tagLength + 1);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("<html>", 0, "<html")]
    [InlineData("<head>", 0, "<head")]
    [InlineData("<div class='test'>", 0, "<div")]
    [InlineData("<!DOCTYPE html>", 0, "<!DOCTYPE")]
    public void GetTagKeywordWithBracket_UsingKeywordLength_ReturnsTagWithOpeningBracket(string text, int index, string expected)
    {
        var tag = text.GetHtmlKeyword(index);
        var result = text.Substring(index, tag.Length + 1);

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Verifies that tag.Length + 2 gives us the full tag including both brackets for simple tags
    /// </summary>
    [Theory]
    [InlineData("<html>", 0, "<html>")]
    [InlineData("<head>", 0, "<head>")]
    [InlineData("<h1>", 0, "<h1>")]
    public void GetTagKeywordWithBothBrackets_UsingKeywordLengthPlusTwo_ReturnsFullTag(string text, int index, string expected)
    {
        var tag = text.GetHtmlKeyword(index);
        // tag.Length + 1 for '<' + 1 for '>' = tag.Length + 2
        var result = text.Substring(index, tag.Length + 2);

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// This test simulates the CORRECT way to process HTML tags
    /// </summary>
    [Theory]
    [InlineData("<html>content</html>", "<html>content</html>")]
    [InlineData("<head></head>", "<head></head>")]
    [InlineData("<!DOCTYPE html><html>", "<!DOCTYPE html><html>")]
    [InlineData("<div class='test'>text</div>", "<div class='test'>text</div>")]
    public void SimulateParseHtml_ProcessingTags_OutputMatchesInput(string input, string expected)
    {
        var output = new System.Text.StringBuilder();
        var max = input.Length;

        for (var i = 0; i < max; i++)
        {
            if (input[i] == '<')
            {
                var tagLength = input.GetHtmlTagLength(i);
                
                // The correct approach: use tagLength + 1 to get the full tag including '>'
                output.Append(input.Substring(i, tagLength + 1));

                i += tagLength;  // Move to '>', then loop's i++ moves past it
            }
            else
            {
                output.Append(input[i]);
            }
        }

        Assert.Equal(expected, output.ToString());
    }
}
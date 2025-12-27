using HTMLTagColorer;

namespace HTMLTagColorerTests;

public class StringExtensionsTests
{
    [Theory]
    [InlineData("<html>", 0, 5)]
    [InlineData("<html class='id'>", 0, 16)]
    [InlineData("  <html>", 2, 5)]
    public void GetHtmlTagLength_StringWithTag_ReturnTagLength(string text, int index, int expected)
    {
        var result = text.GetHtmlTagLength(index);

        Assert.Equal(result, expected);
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
    public void GetPropertiesWithValueFromHtmlTag_StringWithTag_ReturnDictionaryWithPropAndValue(string text,
        string key, string exception)
    {
        var result = text.GetPropertiesWithValueFromHtmlTag();
        Assert.Equal(result.GetValueOrDefault(key), exception);
    }
}
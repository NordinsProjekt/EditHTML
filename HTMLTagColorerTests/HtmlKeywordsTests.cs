using HTMLTagColorer;

namespace HTMLTagColorerTests;

public class HtmlKeywordsTests
{
    [Theory]
    [InlineData("main")]
    [InlineData("/main")]
    [InlineData("header")]
    public void DoesTagExist_LookForExistingTag_ShouldReturnTrue(string key)
    {
        Assert.True(HtmlKeywords.DoesTagExist(key));
    }

    [Theory]
    [InlineData("mian")]
    [InlineData("/test")]
    [InlineData("classes")]
    public void DoesTagExist_LookForNonExistingTag_ShouldReturnFalse(string key)
    {
        Assert.False(HtmlKeywords.DoesTagExist(key));
    }

    [Theory]
    [InlineData("class")]
    [InlineData("id")]
    public void DoesPropertyExist_LookForExistingTag_ShouldReturnTrue(string key)
    {
        Assert.True(HtmlKeywords.DoesPropertyExist(key));
    }

    [Theory]
    [InlineData("help")]
    [InlineData("test")]
    public void DoesPropertyExist_LookForNonExistingTag_ShouldReturnFalse(string key)
    {
        Assert.False(HtmlKeywords.DoesPropertyExist(key));
    }
}

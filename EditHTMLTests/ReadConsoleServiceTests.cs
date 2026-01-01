using EditHTML;

namespace EditHTMLTests;

public class ReadConsoleServiceTests
{
    [Theory]
    [InlineData(ConsoleKey.UpArrow, true)]
    [InlineData(ConsoleKey.DownArrow, true)]
    [InlineData(ConsoleKey.LeftArrow, true)]
    [InlineData(ConsoleKey.RightArrow, true)]
    [InlineData(ConsoleKey.Tab, true)]
    [InlineData(ConsoleKey.Home, true)]
    [InlineData(ConsoleKey.End, true)]
    [InlineData(ConsoleKey.PageDown, true)]
    [InlineData(ConsoleKey.PageUp, true)]
    [InlineData(ConsoleKey.Backspace, false)]
    [InlineData(ConsoleKey.Enter, false)]
    public void IsNavigationButton_SendKey_ShouldMatchExpectedValue(ConsoleKey consoleKey, bool expected)
    {
        var result = ReadConsoleService.IsNavigationKey(consoleKey);
        Assert.Equal(expected, result);
    }
}

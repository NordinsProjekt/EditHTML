using System;
namespace EditHTML;

public static class ReadConsoleService
{
    /// <summary>
    /// Determines whether the specified console key is treated as a navigation key.
    /// </summary>
    /// <param name="consoleKey">The <see cref="ConsoleKey"/> value to evaluate.</param>
    /// <returns><c>true</c> if the key is considered a navigation key; otherwise, <c>false</c>.</returns>
    public static bool IsNavigationKey(ConsoleKey consoleKey)
    {
        switch (consoleKey)
        {
            case ConsoleKey.UpArrow: return true;
            case ConsoleKey.DownArrow: return true;
            case ConsoleKey.LeftArrow: return true;
            case ConsoleKey.RightArrow: return true;
            case ConsoleKey.Tab: return true;
            case ConsoleKey.Home: return true;
            case ConsoleKey.End: return true;
            case ConsoleKey.PageUp: return true;
            case ConsoleKey.PageDown: return true;

            default: return false;
        }
    }
}

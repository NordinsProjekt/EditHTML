using System;
namespace EditHTML;

public static class ReadConsoleService
{
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

namespace ConsoleEditLogic;

public static class DisplayService
{
    public static void OverwriteConsoleLine(string newLine, int cursorTop)
    {
        Console.CursorVisible = false;
        Console.SetCursorPosition(0, cursorTop);
        // Clear the entire line
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, cursorTop);
        Console.Write(newLine);
        Console.CursorVisible = true;
    }
}

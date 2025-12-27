namespace HTMLTagColorer;

public static class DisplayService
{
    public static void PrintTextWithColor(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ResetColor();
    }
}

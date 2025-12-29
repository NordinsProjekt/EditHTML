using System.Runtime.CompilerServices;

namespace ConsoleEditLogic;

public static class EditConsoleTextService
{
    public static CursorPosition EditConsoleLine(ConsoleKeyInfo consoleKey, string[] htmlContent, int cursorLeft, int cursorTop)
    {
        //TODO 
        //Ta reda på vilken rad cursor är på
        //Plocka fram textraden.
        //Gör ändringen.
        //Flytta cursor till left = 0
        //Skriv ut hela raden igen.

        //TODO ENTER
        //Enterslag delar på raden
        //Alla raderna nedanför måste ritas om.
        string newLine = "";
        switch(consoleKey.Key)
        {
            case ConsoleKey.Enter:
                return new CursorPosition(cursorLeft, cursorTop);

            case ConsoleKey.Backspace:
                if (cursorLeft > 0)
                {
                    newLine = htmlContent[cursorTop].Remove(cursorLeft - 1, 1);
                    htmlContent[cursorTop] = newLine;
                    OverwriteConsoleLine(htmlContent[cursorTop], cursorLeft, cursorTop);
                    return new CursorPosition(cursorLeft-1, cursorTop);
                }
                return new CursorPosition(cursorLeft, cursorTop);

            case ConsoleKey.Spacebar:
                newLine = htmlContent[cursorTop].Insert(cursorLeft, " ");
                htmlContent[cursorTop] = newLine;
                OverwriteConsoleLine(htmlContent[cursorTop], cursorLeft + 1, cursorTop);
                return new CursorPosition(cursorLeft+1, cursorTop);

            default: throw new ArgumentOutOfRangeException();
        }
    }

    private static void OverwriteConsoleLine(string newLine, int cursorLeft, int cursorTop)
    {
        Console.SetCursorPosition(0, cursorTop);
        // Clear the entire line
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, cursorTop);
        Console.Write(newLine);
    }
}

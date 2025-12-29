using System.Runtime.CompilerServices;

namespace ConsoleEditLogic;

public static class EditConsoleTextService
{
    public static CursorPosition EditConsoleLine(ConsoleKeyInfo consoleKey, ref string[] htmlContent, int cursorLeft, int cursorTop)
    {
        string newLine = "";
        string oldLine = "";
        switch(consoleKey.Key)
        {
            case ConsoleKey.Enter:
                oldLine = htmlContent[cursorTop].Substring(0, cursorLeft);
                newLine = htmlContent[cursorTop].Substring(cursorLeft);

                htmlContent[cursorTop] = oldLine;

                var list = htmlContent.ToList();
                list.Insert(cursorTop + 1, newLine);
                htmlContent = list.ToArray();

                // Save current window position
                int savedWindowTop = Console.WindowTop;

                for (int i = cursorTop; i < htmlContent.Length; i++)
                {
                    OverwriteConsoleLine(htmlContent[i], i);
                }

                // Restore window position to prevent scrolling
                Console.SetWindowPosition(0, savedWindowTop);

                return new CursorPosition(0, cursorTop+1);

            case ConsoleKey.Backspace:
                if (cursorLeft > 0)
                {
                    newLine = htmlContent[cursorTop].Remove(cursorLeft - 1, 1);
                    htmlContent[cursorTop] = newLine;
                    OverwriteConsoleLine(htmlContent[cursorTop], cursorTop);
                    return new CursorPosition(cursorLeft-1, cursorTop);
                }
                return new CursorPosition(cursorLeft, cursorTop);

            case ConsoleKey.Spacebar:
                newLine = htmlContent[cursorTop].Insert(cursorLeft, " ");
                htmlContent[cursorTop] = newLine;
                OverwriteConsoleLine(htmlContent[cursorTop], cursorTop);
                return new CursorPosition(cursorLeft+1, cursorTop);

            case ConsoleKey.S:
                if (consoleKey.Modifiers.HasFlag(ConsoleModifiers.Control))
                {
                    //TODO
                    //Overwrite the html file with the array
                    //Trigger a console redraw with HTML colors
                    return new CursorPosition(cursorLeft, cursorTop);
                }
                else
                {
                    //Just a normal S
                    newLine = htmlContent[cursorTop].Insert(cursorLeft, consoleKey.KeyChar.ToString());
                    htmlContent[cursorTop] = newLine;
                    OverwriteConsoleLine(htmlContent[cursorTop], cursorTop);
                    return new CursorPosition(cursorLeft + 1, cursorTop);
                }

            default:
                newLine = htmlContent[cursorTop].Insert(cursorLeft, consoleKey.KeyChar.ToString());
                htmlContent[cursorTop] = newLine;
                OverwriteConsoleLine(htmlContent[cursorTop], cursorTop);
                return new CursorPosition(cursorLeft + 1, cursorTop);
        }
    }

    private static void OverwriteConsoleLine(string newLine, int cursorTop)
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

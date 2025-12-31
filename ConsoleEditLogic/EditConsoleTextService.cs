using System.Net.Http;

namespace ConsoleEditLogic;

public static class EditConsoleTextService
{
    public static TextServiceResult EditConsoleLine(ConsoleKeyInfo consoleKey, ref string[] htmlContent, int cursorLeft, int cursorTop)
    {
        // Bounds check - prevent accessing beyond array
        if (cursorTop >= htmlContent.Length)
        {
            return new(0, htmlContent.Length - 1, false);
        }
        
        // Ensure cursorLeft is within bounds of current line
        if (cursorLeft > htmlContent[cursorTop].Length)
        {
            cursorLeft = htmlContent[cursorTop].Length;
        }

        int savedWindowTop = 0;
        switch (consoleKey.Key)
        {
            case ConsoleKey.Enter:

                // Save current window position
                savedWindowTop = Console.WindowTop;

                ConsoleCommandService.EnterCommand(ref htmlContent, cursorLeft, cursorTop);

                // Restore window position to prevent scrolling
                Console.SetWindowPosition(0, savedWindowTop);

                return new(0, cursorTop + 1, false);

            case ConsoleKey.Backspace:
                if (cursorLeft == 0 && cursorTop > 0)
                {
                    // Get the length of the previous line before appending
                    int previousLineLength = htmlContent[cursorTop - 1].Length;
                    ConsoleCommandService.BackSpaceCommandLeftIndexZero(ref htmlContent, cursorTop, ref savedWindowTop);

                    return new(previousLineLength, cursorTop -1, false);
                }

                if (cursorLeft > 0)
                {
                    ConsoleCommandService.BackSpaceCommand(htmlContent, cursorLeft, cursorTop);
                    return new(cursorLeft-1, cursorTop, false);
                }
                return new(cursorLeft, cursorTop, false);

            case ConsoleKey.Delete:
                if(cursorLeft == htmlContent[cursorTop].Length)
                {
                    ConsoleCommandService.DeleteCommandLeftIndexMax(ref htmlContent, cursorLeft, cursorTop, ref savedWindowTop);
                    return new(cursorLeft, cursorTop, false);
                }
                else
                {
                    ConsoleCommandService.DeleteCommand(htmlContent, cursorLeft, cursorTop);
                    return new(cursorLeft, cursorTop, false);
                }

            case ConsoleKey.Spacebar:
                htmlContent[cursorTop] = htmlContent[cursorTop].Insert(cursorLeft, " ");
                DisplayService.OverwriteConsoleLine(htmlContent[cursorTop], cursorTop);

                return new(cursorLeft+1, cursorTop, false);

            case ConsoleKey.S:
                if (consoleKey.Modifiers.HasFlag(ConsoleModifiers.Control))
                {
                    return new(cursorLeft, cursorTop, true);
                }
                else
                {
                    htmlContent[cursorTop] = htmlContent[cursorTop].Insert(cursorLeft, consoleKey.KeyChar.ToString());
                    DisplayService.OverwriteConsoleLine(htmlContent[cursorTop], cursorTop);

                    return new(cursorLeft+1, cursorTop, false);
                }

            default:
                htmlContent[cursorTop] = htmlContent[cursorTop].Insert(cursorLeft, consoleKey.KeyChar.ToString());
                DisplayService.OverwriteConsoleLine(htmlContent[cursorTop], cursorTop);

                return new(cursorLeft+1, cursorTop, false);
        }
    }


}

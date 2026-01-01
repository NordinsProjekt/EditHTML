using ConsoleEditLogic.Dtos;

namespace ConsoleEditLogic;

public static class EditConsoleTextService
{
    /// <summary>
    /// Processes a console key input and updates the in-memory HTML content and console display for the current line.
    /// </summary>
    /// <param name="consoleKey">The key information for the key that was pressed.</param>
    /// <param name="htmlContent">
    /// The HTML content buffer, where each element represents a line. This array may be modified in place
    /// (lines inserted, removed, or updated) as a result of handling the key input.
    /// </param>
    /// <param name="cursorLeft">The current horizontal cursor position (column index) within the active line.</param>
    /// <param name="cursorTop">The current vertical cursor position (line index) within <paramref name="htmlContent"/>.</param>
    /// <returns>
    /// A <see cref="TextServiceResult"/> containing the updated cursor position and a flag indicating
    /// whether the content should be saved (for example, when Ctrl+S is pressed).
    /// </returns>
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

                    return new(previousLineLength, cursorTop - 1, false);
                }

                if (cursorLeft > 0)
                {
                    ConsoleCommandService.BackSpaceCommand(htmlContent, cursorLeft, cursorTop);
                    return new(cursorLeft - 1, cursorTop, false);
                }
                return new(cursorLeft, cursorTop, false);

            case ConsoleKey.Delete:
                if (cursorLeft == htmlContent[cursorTop].Length)
                {
                    // Only attempt to delete/merge with the next line if it exists
                    if (cursorTop < htmlContent.Length - 1)
                    {
                        ConsoleCommandService.DeleteCommandLeftIndexMax(ref htmlContent, cursorLeft, cursorTop, ref savedWindowTop);
                    }
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

                    return new(cursorLeft + 1, cursorTop, false);
                }

            default:
                htmlContent[cursorTop] = htmlContent[cursorTop].Insert(cursorLeft, consoleKey.KeyChar.ToString());
                DisplayService.OverwriteConsoleLine(htmlContent[cursorTop], cursorTop);

                return new(cursorLeft + 1, cursorTop, false);
        }
    }


}

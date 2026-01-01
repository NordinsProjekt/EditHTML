namespace ConsoleEditLogic;

public static class ConsoleCommandService
{
    public static void EnterCommand(ref string[] htmlContent, int cursorLeft, int cursorTop)
    {
        var oldLine = htmlContent[cursorTop].Substring(0, cursorLeft);
        var newLine = htmlContent[cursorTop].Substring(cursorLeft);

        htmlContent[cursorTop] = oldLine;

        var list = htmlContent.ToList();
        list.Insert(cursorTop + 1, newLine);
        htmlContent = list.ToArray();

        for (int i = cursorTop; i < htmlContent.Length; i++)
        {
            DisplayService.OverwriteConsoleLine(htmlContent[i], i);
        }
    }

    public static void BackSpaceCommandLeftIndexZero(ref string[] htmlContent, int cursorTop, ref int savedWindowTop)
    {
        // Append current line to previous line
        htmlContent[cursorTop - 1] += htmlContent[cursorTop];

        // Remove current line from array
        var tempList = htmlContent.ToList();
        tempList.RemoveAt(cursorTop);
        htmlContent = tempList.ToArray();

        // Save current window position
        savedWindowTop = Console.WindowTop;

        // Redraw from the merged line onwards, plus one extra to clear
        int linesToRedraw = Math.Min(Console.WindowHeight, htmlContent.Length + 1);
        for (int i = cursorTop - 1; i < linesToRedraw; i++)
        {
            if (i < htmlContent.Length)
            {
                DisplayService.OverwriteConsoleLine(htmlContent[i], i);
            }
            else
            {
                // Clear lines beyond the content
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.WindowWidth));
            }
        }

        // Restore window position to prevent scrolling
        Console.SetWindowPosition(0, savedWindowTop);
    }

    public static void BackSpaceCommand(string[] htmlContent, int cursorLeft, int cursorTop)
    {
        var newLine = "";
        newLine = htmlContent[cursorTop].Remove(cursorLeft - 1, 1);
        htmlContent[cursorTop] = newLine;
        DisplayService.OverwriteConsoleLine(htmlContent[cursorTop], cursorTop);
    }

    public static void DeleteCommand(string[]htmlContent, int cursorLeft, int CursorTop)
    {
        var newLine = "";
        newLine = htmlContent[CursorTop].Remove(cursorLeft, 1);
        htmlContent[CursorTop] = newLine;
        DisplayService.OverwriteConsoleLine(htmlContent[CursorTop], CursorTop);
    }

    public static void DeleteCommandLeftIndexMax(ref string[] htmlContent, int cursorLeft, int cursorTop, ref int savedWindowTop)
    {
        htmlContent[cursorTop] += htmlContent[cursorTop+1];

        // Remove current line from array
        var tempList = htmlContent.ToList();
        tempList.RemoveAt(cursorTop+1);
        htmlContent = tempList.ToArray();

        // Save current window position
        savedWindowTop = Console.WindowTop;
        // Redraw from the merged line onwards, plus one extra to clear
        int linesToRedraw = Math.Min(Console.WindowHeight, htmlContent.Length + 1);
        for (int i = cursorTop; i < linesToRedraw; i++)
        {
            if (i < htmlContent.Length)
            {
                DisplayService.OverwriteConsoleLine(htmlContent[i], i);
            }
            else
            {
                // Clear lines beyond the content
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.WindowWidth));
            }
        }

        Console.SetWindowPosition(0, savedWindowTop);
    }
}

using ConsoleEditLogic;
using ConsoleEditLogic.Dtos;
using HTMLTagColorer;
using System.Text;

namespace EditHTML
{
    public class Program
    {
        static void Main(string[] args)
        {
            var consoleCursorService = new ConsoleCursorService();
            Console.CursorVisible = true;
            //if (args.Length == 0)
            //{
            //    Console.WriteLine("How to use!\n<name of program> <index.html>");
            //    return;
            //}
            string fileName = "index.html";
            string content;
            string[] lines = [];
            try
            {
                content = ReadFromHtml(fileName);

                // Split content into lines to track line lengths
                lines = content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

                RedrawScreen(content, new CursorPosition(0, 0));
                consoleCursorService.SetCursorPosition(0, 0);

            }
            catch (Exception e)
            {
                Console.ResetColor();
                Console.WriteLine(e.Message);
                return;
            }

            while (true)
            {
                var keyPress = Console.ReadKey(true);

                // Get the current cursor position to determine which line we're on
                var currentTop = Console.GetCursorPosition().Top;
                var currentLeft = Console.GetCursorPosition().Left;

                // Clamp cursor position to valid array bounds
                if (currentTop >= lines.Length)
                {
                    currentTop = lines.Length - 1;
                    Console.SetCursorPosition(currentLeft, currentTop);
                }

                // Get max left for current row (line length), default to 0 if out of bounds
                var maxLeftForCurrentRow = currentTop < lines.Length
                    ? lines[currentTop].TrimEnd('\r').Length
                    : 0;

                // Clamp left position to current line length
                if (currentLeft > maxLeftForCurrentRow)
                {
                    currentLeft = maxLeftForCurrentRow;
                    Console.SetCursorPosition(currentLeft, currentTop);
                }

                var isNavigationalKey = ReadConsoleService.IsNavigationKey(keyPress.Key);

                if (isNavigationalKey)
                {
                    var newCursorPosition = consoleCursorService.GetNewCursorPositionAfterNavigationKeys(
                        keyPress,
                        maxLeftForCurrentRow,
                        lines.Length - 1);

                    Console.SetCursorPosition(newCursorPosition.Left, newCursorPosition.Top);
                }
                else
                {
                    var EditConsoleResult = EditConsoleTextService.EditConsoleLine(keyPress, ref lines, Console.CursorLeft, Console.CursorTop);
                    Console.SetCursorPosition(EditConsoleResult.CursorPosition.Left, EditConsoleResult.CursorPosition.Top);
                    consoleCursorService.SetCursorPosition(EditConsoleResult.CursorPosition.Left, EditConsoleResult.CursorPosition.Top);
                    if (EditConsoleResult.RedrawScreen)
                    {
                        WriteToHtml(lines, fileName);
                        content = ReadFromHtml(fileName);
                        lines = content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                        RedrawScreen(content, EditConsoleResult.CursorPosition);
                        Console.SetCursorPosition(EditConsoleResult.CursorPosition.Left, EditConsoleResult.CursorPosition.Top);
                    }
                }
            }
        }

        private static void RedrawScreen(string content, CursorPosition cursorPosition)
        {
            Console.Clear();
            HtmlService.ParseHtml(content);
            Console.ResetColor();
            Console.SetCursorPosition(cursorPosition.Left, cursorPosition.Top);
        }

        private static string ReadFromHtml(string fileName)
        {
            return File.ReadAllText(fileName, Encoding.UTF8);
        }

        private static void WriteToHtml(string[] lines, string fileName)
        {
            File.WriteAllLines(fileName, lines, Encoding.UTF8);
        }
    }
}
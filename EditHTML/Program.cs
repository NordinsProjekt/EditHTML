using System.Text;
using ConsoleEditLogic;
using HTMLTagColorer;

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

            string content;
            string[] lines = [];
            try
            {
                using var sr = new StreamReader("index.html", Encoding.UTF8);
                content = sr.ReadToEnd();
                
                // Split content into lines to track line lengths
                lines = content.Split('\n');
                
                HtmlService.ParseHtml(content);
                Console.ResetColor();
                Console.SetCursorPosition(0, 0);
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
                
                // Get max left for current row (line length), default to 0 if out of bounds
                var maxLeftForCurrentRow = currentTop < lines.Length 
                    ? lines[currentTop].TrimEnd('\r').Length 
                    : 0;

                var isNavigationalKey = ReadConsoleService.IsNavigationKey(keyPress.Key);

                if(isNavigationalKey)
                {
                    var newCursorPosition = consoleCursorService.GetNewCursorPositionAfterNavigationKeys(
                        keyPress,
                        maxLeftForCurrentRow,
                        lines.Length - 1);

                    Console.SetCursorPosition(newCursorPosition.Left, newCursorPosition.Top);
                }
                else
                {
                    var newCursorPosition = EditConsoleTextService.EditConsoleLine(keyPress, lines, Console.CursorLeft, Console.CursorTop);
                    Console.SetCursorPosition(newCursorPosition.Left, newCursorPosition.Top);
                    consoleCursorService.SetCursorPosition(newCursorPosition.Left, newCursorPosition.Top);
                }
            }
        }
    }
}
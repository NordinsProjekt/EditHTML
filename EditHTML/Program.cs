using System.Text;
using ConsoleEditLogic;
using HTMLTagColorer;

namespace EditHTML
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var consoleCursorService = new ConsoleCursorService(Console.GetCursorPosition().Left, Console.GetCursorPosition().Top);
            Console.CursorVisible = true;
            //if (args.Length == 0)
            //{
            //    Console.WriteLine("How to use!\n<name of program> <index.html>");
            //    return;
            //}

            string content;
            try
            {
                using var sr = new StreamReader("index.html", Encoding.UTF8);
                content = sr.ReadToEnd();
                HtmlService.ParseHtml(content);
                Console.ResetColor();
            }
            catch (Exception e)
            {
                Console.ResetColor();
                Console.WriteLine(e.Message);
            }

            while (true)
            {
                var keyPress = Console.ReadKey(true);
                var newCursorPosition = consoleCursorService.GetNewCursorPosition(keyPress.Key);
                Console.SetCursorPosition(newCursorPosition.Left, newCursorPosition.Top);
            }
        }
    }
}
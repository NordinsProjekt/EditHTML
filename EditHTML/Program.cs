using System.Text;
using HTMLTagColorer;

namespace EditHTML
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
        }
    }
}
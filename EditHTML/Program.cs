using System.Text;

namespace EditHTML
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("How to use!\n<name of program> <index.html>");
                return;
            }

            string content;
            try
            {
                using var sr = new StreamReader(args[0], Encoding.UTF8);
                content = sr.ReadToEnd();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            var htmlKeywords = new List<string>
            {
                "html", "body", "head", "div",
            };

            WriteWithKeywordHighlighting(content, htmlKeywords);
            Console.ResetColor();
        }

        private static void WriteWithKeywordHighlighting(string text, List<string> keywords)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            int i = 0;
            while (i < text.Length)
            {
                bool matched = false;

                foreach (var kw in keywords)
                {
                    if (IsKeywordAt(text, i, kw))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(text.Substring(i, kw.Length));
                        i += kw.Length;
                        matched = true;
                        break;
                    }
                }

                if (!matched)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(text[i]);
                    i++;
                }
            }
        }

        private static bool IsKeywordAt(string text, int index, string keyword)
        {
            // Fast fail if keyword would overflow the remaining text
            if (index + keyword.Length > text.Length)
            {
                return false;
            }

            // Compare the range starting at index with keyword using OrdinalIgnoreCase
            return string.Compare(text, index, keyword, 0, keyword.Length, StringComparison.OrdinalIgnoreCase) == 0;
        }
    }
}
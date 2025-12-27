namespace HTMLTagColorer;

public static class HtmlService
{
    public static void ParseHtml(string htmlText)
    {
        var max = htmlText.Length;

        for (var i = 0; i < max; i++)
        {
            if (htmlText[i] == '<')
            {
                var tagLength = htmlText.GetHtmlTagLength(i);
                var tag = htmlText.GetHtmlKeyword(i).Split(' ')[0];

                Console.ForegroundColor = HtmlKeywords.DoesTagExist(tag) ? ConsoleColor.Green : ConsoleColor.White;

                Console.Write(htmlText.Substring(i, tagLength));
                i += tagLength - 1;
            }
            else
            {
                Console.Write(htmlText[i]);
            }
        }
    }
}
namespace HTMLTagColorer;

public static class ConsoleHtmlService
{
    public static void ParseHtml(string htmlText)
    {
        var max = htmlText.Length;
        for (var i = 0; i < max; i++)
        {
            if (htmlText[i] != '<')
            {
                var tagLength = htmlText.GetHtmlTagLength(i);
            }
            else
            {
                Console.Write(htmlText[i]);
            }
        }
    }
}

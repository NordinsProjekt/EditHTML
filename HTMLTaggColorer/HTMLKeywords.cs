namespace HTMLTagColorer;

public static class HtmlKeywords
{
    private static readonly List<string> Tags = ["html", "head", "body", "header", "footer", "main"];
    private static readonly List<string> Properties = ["class", "id"];

    public static bool DoesTagExist(string key)
    {
        return Tags.Contains(key.Replace("/", ""));
    }

    public static bool DoesPropertyExist(string key)
    {
        return Properties.Contains(key);
    }
}
namespace HTMLTagColorer;

public static class HtmlKeywords
{
    private static readonly List<string> Tags = [
        "html", "head", "body", "header", "footer", "main", "div", "a", 
        "h1", "h2", "h3", "h4", "h5", "h6", 
        "meta", "link", "span", "time", "article", "section", "p", "img", "title",
        "nav", "ul", "ol", "li", "table", "tr", "td", "th", "form", "input", "button",
        "script", "style", "br", "hr"
    ];
    
    private static readonly List<string> Properties = [
        "class", "id", "href", "lang", "charset", "name", "content", 
        "src", "alt", "width", "height", "rel", "type",
        "datetime", "aria-label", "aria-hidden", "role",
        "target", "style", "title", "placeholder", "value",
        "data-*", "tabindex", "disabled", "readonly"
    ];

    public static bool DoesTagExist(string key)
    {
        return Tags.Contains(key.Replace("/", ""));
    }

    public static bool DoesPropertyExist(string key)
    {
        // Check exact match first
        if (Properties.Contains(key)) return true;
        
        // Check for data-* attributes
        if (key.StartsWith("data-")) return true;
        
        // Check for aria-* attributes
        if (key.StartsWith("aria-")) return true;
        
        return false;
    }
}
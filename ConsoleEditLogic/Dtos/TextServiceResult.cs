namespace ConsoleEditLogic.Dtos;

public class TextServiceResult
{
    public CursorPosition CursorPosition { get; set; }
    public bool RedrawScreen { get; set; }

    public TextServiceResult(int left, int top, bool redrawScreen)
    {
        CursorPosition = new CursorPosition(left, top);
        RedrawScreen = redrawScreen;
    }
}

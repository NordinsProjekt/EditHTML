namespace ConsoleEditLogic.Dtos;

public class CursorPosition(int left, int top)
{
    public int Left { get; init; } = left;
    public int Top { get; init; } = top;
}
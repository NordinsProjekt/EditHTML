using ConsoleEditLogic.Dtos;

namespace ConsoleEditLogic;

public class ConsoleCursorService(int left = 0, int top = 0)
{
    private int _left = left;
    private int _top = top;

    public CursorPosition GetNewCursorPositionAfterNavigationKeys(ConsoleKeyInfo keyPressed, int currentMaxLeft, int currentMaxTop)
    {
        switch (keyPressed.Key)
        {
            case ConsoleKey.LeftArrow:
                if (_left - 1 >= 0)
                    _left -= 1;
                break;

            case ConsoleKey.RightArrow:
                if (_left + 1 <= currentMaxLeft)
                    _left += 1;
                break;

            case ConsoleKey.UpArrow:
                if (_top - 1 >= 0)
                    _top -= 1;
                break;

            case ConsoleKey.DownArrow:
                if (_top + 1 <= currentMaxTop)
                    _top += 1;
                break;

            case ConsoleKey.Home:
                _top = 0;
                break;

            case ConsoleKey.End:
                _top = currentMaxTop;
                break;

            case ConsoleKey.PageUp:
                if (_top - 20 >= 0)
                    _top -= 20;
                else
                    _top = 0;
                break;

            case ConsoleKey.PageDown:
                if (_top + 20 <= currentMaxTop)
                    _top += 20;
                else
                    _top = currentMaxTop;
                break;

            case ConsoleKey.Tab:
                if (keyPressed.Modifiers.HasFlag(ConsoleModifiers.Shift))
                {
                    // Shift+Tab - move left
                    _left = Math.Max(0, _left - 10);
                }
                else
                {
                    // Tab only - move right
                    if (_left + 10 <= currentMaxLeft) _left += 10;
                }
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(keyPressed), keyPressed, null);
        }

        return new CursorPosition(_left, _top);
    }

    public void SetCursorPosition(int left, int top)
    {
        if (left < 0 || top < 0) return;

        _left = left;
        _top = top;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

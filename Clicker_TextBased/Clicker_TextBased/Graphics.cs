using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicker_TextBased
{
    class Graphics
    {
        public static void Draw(int x, int y, string graphics)
        {
            int offsetX = 3;
            int offsetY = 0;
            Console.SetCursorPosition(x + offsetX, y + offsetY);
            Console.Write(graphics);
        }

        public static void DrawAfter(string graphics)
        {
            Console.Write(graphics);
        }
    }
}

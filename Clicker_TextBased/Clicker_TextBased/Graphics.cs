using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicker_TextBased
{
    /// <summary>
    /// Utility class used to draw on the console
    /// </summary>
    static class Graphics
    {
        static int offsetX = 3;
        static int offsetY = 0;

        public static void Draw(int x, int y, string graphics)
        {

            Console.SetCursorPosition(x + offsetX, y + offsetY);
            Console.Write(graphics);
        }

        public static void DrawAfter(string graphics)
        {
            Console.Write(graphics);
        }
    }
}

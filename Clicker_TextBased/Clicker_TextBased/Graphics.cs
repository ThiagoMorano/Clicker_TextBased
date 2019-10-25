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
            Console.SetCursorPosition(x, y);
            Console.Write(graphics);
        }
    }
}

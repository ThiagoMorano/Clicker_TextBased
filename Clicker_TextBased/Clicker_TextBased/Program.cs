using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clicker_TextBased
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetWindowSize(140, 32);
            Game game = new Game();
            game.Init();

            Console.CursorVisible = false;

            const double FPS = 24;
            double timeUntilNextFrame = 0.0d;
            Time.Init();
            while (!game.exit)
            {
                Time.UpdateSinceLastCycle();
                timeUntilNextFrame += Time.TimeSinceLastCycle;

                if (timeUntilNextFrame >= (1 / FPS))
                {
                    game.Update();

                    game.Draw();


                    Time.UpdateSinceLastFrame();
                    timeUntilNextFrame = 0.0d;
                }
            }

            Graphics.Draw(50, 25, "Press any key to exit...");
            Console.ReadKey();
        }
    }
}

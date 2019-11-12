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
            Console.SetWindowSize(140, 32);
            Console.CursorVisible = false;

            Console.WriteLine("The world needs to be hacked.");
            Console.WriteLine("Generates lines of code with [SpaceBar]");
            Console.WriteLine("Purchase items to help you with hacking the world.");
            Console.WriteLine("Good luck and happy hacking!");
            Console.WriteLine("Press any key to start...");

            Console.ReadKey(true);

            Console.Clear();
    

            Game game = new Game();
            game.Init();

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

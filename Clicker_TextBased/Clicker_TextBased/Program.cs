using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;


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
            Console.WriteLine("Keep your window maximized for best hacking experience.");
            Console.WriteLine("Good luck and happy hacking!");
            Console.WriteLine("Press any key to start...");

            Console.ReadKey(true);

            MaximizeConsole();

            Console.Clear();            
            Console.CursorVisible = false;

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

                    if (!game.win)
                        game.Draw();


                    Time.UpdateSinceLastFrame();
                    timeUntilNextFrame = 0.0d;
                }
            }

            Graphics.Draw(89, 35, "Press any key to exit...");
            Console.ReadKey();
        }

        //Snippet taken from https://stackoverflow.com/questions/22053112/maximizing-console-window-c-sharp/22053200
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);

        private static void MaximizeConsole()
        {
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3);
        }
        // end of snippet
    }
}

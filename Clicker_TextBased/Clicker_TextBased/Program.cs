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
            Time.Init();
            Player player = new Player();
            player.Init();

            Item item0 = new Item();

            player.Update();

            Console.WriteLine("Press Space to generate     |     Press A to purchase\n");
            Console.WriteLine("Currency: " + player.CurrentCurrencyValue.ToString("f3"));

            Console.CursorVisible = false;

            const double FPS = 30;
            bool exit = false;
            double timeUntilNextFrame = 0.0d;
            while (!exit)
            {
                Time.UpdateSinceLastCycle();
                timeUntilNextFrame += Time.TimeSinceLastCycle;

                if (timeUntilNextFrame >= (1 / FPS))
                {
                    // GAME UPDATE {

                    ConsoleKey inputReceived = ConsoleKey.Z; //Initialized with any value (non-nullable type)
                    bool inputWasRead = false;
                    while (Console.KeyAvailable)
                    {
                        if (inputWasRead)
                        {
                            Console.ReadKey(true);
                        }
                        else
                        {
                            inputReceived = Console.ReadKey(true).Key;
                            inputWasRead = true;
                        }
                    }

                    if (inputWasRead)
                    {
                        switch (inputReceived)
                        {
                            case ConsoleKey.Spacebar:
                                player.Click();
                                break;
                            case ConsoleKey.A:
                                player.AttemptToPurchase(item0);
                                break;
                            case ConsoleKey.Escape:
                                exit = true;
                                break;
                        }
                    }

                    Console.SetCursorPosition(0, Console.CursorTop - 1);

                    player.Update();
                    Console.WriteLine("Currency: " + player.CurrentCurrencyValue.ToString("f3"));

                    Time.UpdateSinceLastFrame();
                    timeUntilNextFrame = 0.0d;
                }
            }

            Console.ReadKey();
        }
    }
}

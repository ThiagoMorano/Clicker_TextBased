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
            Player player = new Player();

            Item item0 = new Item();
            Graph graph = new Graph();
            graph.AddNode(item0);

            Item item1 = new Item(5, 0.5d);
            graph.AddNode(item1);
            graph.AddEdgeFromToElement(item0, item1, 5);


            player.Update();

            Console.WriteLine("Press Space to generate");

            Console.CursorVisible = false;

            const double FPS = 30;
            bool exit = false;
            double timeUntilNextFrame = 0.0d;
            Time.Init();
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
                                if (graph.IsElementAvailableForPurchase(item0))
                                {
                                    if (player.AttemptToPurchase(item0))
                                        graph.VerifyConditionsRelatedToItem(item0, player.CountItemsOfType(item0));
                                }
                                break;
                            case ConsoleKey.S:
                                if (graph.IsElementAvailableForPurchase(item1))
                                    if (player.AttemptToPurchase(item1))
                                        graph.VerifyConditionsRelatedToItem(item1, player.CountItemsOfType(item1));
                                break;
                            case ConsoleKey.Escape:
                                exit = true;
                                break;
                        }
                    }

                    player.Update();
                    Graphics.Draw(0, 1, "Currency: " + player.CurrentCurrencyValue.ToString("f3"));
                    if (graph.IsElementAvailableForPurchase(item0))
                    {
                        Graphics.Draw(0, 3, "Press A to purchase Item0 for " + item0.Cost.ToString());
                        Graphics.Draw(0, 4, "Item0 : " + player.CountItemsOfType(item0).ToString() + " producing " + (player.CountItemsOfType(item0) * (double)item0.ItemGainPerSecond).ToString("f1"));
                    }
                    if (graph.IsElementAvailableForPurchase(item1))
                    {
                        Graphics.Draw(0, 7, "Press S to purchase Item1 for " + item1.Cost.ToString());
                        Graphics.Draw(0, 8, "Item1 : " + player.CountItemsOfType(item1).ToString() + " producing " + (player.CountItemsOfType(item1) * (double)item1.ItemGainPerSecond).ToString("f1"));
                    }


                    Time.UpdateSinceLastFrame();
                    timeUntilNextFrame = 0.0d;
                }
            }

            Graphics.Draw(40, 25, "Presse any key to exit...");
            Console.ReadKey();
        }
    }
}

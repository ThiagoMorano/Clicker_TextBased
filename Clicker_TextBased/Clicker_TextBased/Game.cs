using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicker_TextBased
{
    public enum Screen
    {
        items = 0, upgrades = 1
    }

    public class Game
    {
        public bool exit = false;
        public Screen currentScreen;
        bool _newItemAvailable = false;
        bool _newUpgradeAvailable = false;

        Player player;
        Graph graph;
        Item[] items;
        Upgrade[] upgrades;


        ConsoleKey[] inputToPurchaseItems;
        ConsoleKey[] inputToPurchaseUpgrades;

        public Game()
        {
            currentScreen = Screen.items;
            exit = false;
        }

        public void SwitchCurrentScreenTo(Screen screen)
        {
            currentScreen = screen;
        }

        private void SwitchCurrentInGameScreen()
        {
            if (currentScreen == Screen.items)
                currentScreen = Screen.upgrades;
            else if (currentScreen == Screen.upgrades)
                currentScreen = Screen.items;
        }


        public void Init()
        {
            player = new Player();
            items = new Item[6];
            upgrades = new Upgrade[6];
            graph = new Graph();

            inputToPurchaseItems = new[] { ConsoleKey.A, ConsoleKey.S, ConsoleKey.D, ConsoleKey.F, ConsoleKey.G, ConsoleKey.H };

            string[] descriptionItem = { "Lorem ipsum dolor sit amet,", "consectetur adipiscing elit,", "sed do eiusmod tempor incididunt" };

            items[0] = new Item(1.0d, 0.1d, "ItemName0", descriptionItem);
            graph.AddNode(items[0]);

            items[1] = new Item(5, 0.5d, "ItemName1", descriptionItem);
            graph.AddNode(items[1]);
            //graph.AddEdgeFromToElement(item0, item1, 5);

            items[2] = new Item(10, 1d, "ItemName2", descriptionItem);
            graph.AddNode(items[2]);

            items[3] = new Item(15, 1.5d, "ItemName3", descriptionItem);
            graph.AddNode(items[3]);

            items[4] = new Item(15, 1.5d, "ItemName4", descriptionItem);
            graph.AddNode(items[4]);

            items[5] = new Item(15, 1.5d, "ItemName5", descriptionItem);
            graph.AddNode(items[5]);

            upgrades[0] = new Upgrade(1.0d, items[1], 2.0f);
            graph.AddNode(upgrades[0]);

            upgrades[1] = new Upgrade(1.0d, items[2], 2.0f);
            graph.AddNode(upgrades[1]);

            upgrades[2] = new Upgrade(1.0d, items[3], 2.0f);
            graph.AddNode(upgrades[2]);

            upgrades[3] = new Upgrade(1.0d, items[4], 2.0f);
            graph.AddNode(upgrades[3]);

            //DrawHeadline();
        }
        public void Update()
        {
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
                    case ConsoleKey.Tab:
                        SwitchCurrentInGameScreen();
                        break;
                    case ConsoleKey.A:
                        if (graph.IsElementAvailableForPurchase(items[0]))
                        {
                            if (player.AttemptToPurchase(items[0]))
                                graph.VerifyConditionsRelatedToItem(items[0], player.CountItemsOfType(items[0]));
                        }
                        break;
                    case ConsoleKey.S:
                        if (graph.IsElementAvailableForPurchase(items[1]))
                            if (player.AttemptToPurchase(items[1]))
                                graph.VerifyConditionsRelatedToItem(items[1], player.CountItemsOfType(items[1]));
                        break;
                    case ConsoleKey.X:
                        if (graph.IsElementAvailableForPurchase(upgrades[0]))
                            if (!upgrades[0].HasBeenPurchased)
                                if (player.AttemptToPurchase(upgrades[0]))
                                    graph.VerifyConditionsRelatedToItem(upgrades[0], 1);
                        break;
                    case ConsoleKey.Escape:
                        exit = true;
                        break;
                }
            }

            player.Update();
        }

        public void Draw()
        {

            //DrawHeadline();

            //if (currentScreen == Screen.items)
            DrawItemsScreen();
            //else if (currentScreen == Screen.upgrades)
            DrawUpgradesScreen();


            Graphics.Draw(49, 10, player.CurrentCurrencyValue.ToString("f3"));
            Graphics.Draw(49, 11, "Lines of ~hacking~ Code");
        }

        private void DrawItemsScreen()
        {
            Graphics.Draw(10, 0, "Items");
            for (int i = 0; i < items.Length; i++)
            {
                if (graph.IsElementAvailableForPurchase(items[i]))
                {
                    Graphics.Draw(0, (i * 6) + 2, items[i].Name);
                    Graphics.Draw(17, (i * 6) + 2, items[i].Cost.ToString() + " LoC");
                    Graphics.Draw(29, (i * 6) + 2, inputToPurchaseItems[i].ToString());

                    Graphics.Draw(0, (i * 6) + 3, player.GetGainOfSingleItem(items[i]) + " LPS");
                    Graphics.Draw(17, (i * 6) + 3, "#" + player.CountItemsOfType(items[i]).ToString());

                    for (int j = 0; j < items[i].Description.Length; j++)
                    {
                        Graphics.Draw(0, (i * 6) + 4 + j, items[i].Description[j]);
                    }
                }
            }
        }

        private void DrawUpgradesScreen()
        {
            Graphics.Draw(99, 0, "Upgrades");
            if (graph.IsElementAvailableForPurchase(upgrades[0]))
            {
                Graphics.Draw(89, 2, "UpgradeName0");
                if (upgrades[0].HasBeenPurchased)
                    Graphics.Draw(106, 2, "[PURCHASED]      ");
                else
                {
                    Graphics.Draw(106, 2, upgrades[0].Cost.ToString() + " LoC");
                    Graphics.Draw(118, 2, "X");
                }

                Graphics.Draw(89, 3, "ItemName1    x" + upgrades[0].InfluencedItems[items[1]].ToString("f1"));

                Graphics.Draw(89, 4, "Lorem ipsum dolor sit amet,");
                Graphics.Draw(89, 5, "consectetur adipiscing elit,");
                Graphics.Draw(89, 6, "sed do eiusmod tempor incididunt");
            }
        }

        private void DrawHeadline()
        {
            if (currentScreen == Screen.items)
            {
                Graphics.Draw(0, 0, "_| Items |____|_Upgrades");
                if (_newUpgradeAvailable)
                    Graphics.DrawAfter("*");
                else
                    Graphics.DrawAfter("_");
                Graphics.DrawAfter("|____");
            }
            else if (currentScreen == Screen.upgrades)
            {
                Graphics.Draw(0, 0, "_|_Items");
                if (_newItemAvailable)
                    Graphics.DrawAfter("*");
                else
                    Graphics.DrawAfter("_");
                Graphics.DrawAfter("|____| Upgrades |____");
            }
        }
    }
}

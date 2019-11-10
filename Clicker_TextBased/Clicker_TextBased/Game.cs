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
        Item item0, item1, item2, item3;
        Upgrade upgrade0, upgrade1, upgrade2;

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

            item0 = new Item();
            graph = new Graph();
            graph.AddNode(item0);

            item1 = new Item(5, 0.5d);
            graph.AddNode(item1);
            graph.AddEdgeFromToElement(item0, item1, 5);

            upgrade0 = new Upgrade(1.0d, item0, 2.0f);
            graph.AddNode(upgrade0);

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
                    case ConsoleKey.X:
                        if (graph.IsElementAvailableForPurchase(upgrade0))
                            if (!upgrade0.HasBeenPurchased)
                                if (player.AttemptToPurchase(upgrade0))
                                    graph.VerifyConditionsRelatedToItem(upgrade0, 1);
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
            if (graph.IsElementAvailableForPurchase(item0))
            {
                Graphics.Draw(0, 2, "ItemName0");
                Graphics.Draw(17, 2, item0.Cost.ToString() + " LoC");
                Graphics.Draw(29, 2, "A");

                Graphics.Draw(0, 3, player.GetGainOfSingleItem(item0) + " LPS");
                Graphics.Draw(17, 3, "#" + player.CountItemsOfType(item0).ToString());

                Graphics.Draw(0, 4, "Lorem ipsum dolor sit amet,");
                Graphics.Draw(0, 5, "consectetur adipiscing elit,");
                Graphics.Draw(0, 6, "sed do eiusmod tempor incididunt");
            }
            if (graph.IsElementAvailableForPurchase(item1))
            {
                Graphics.Draw(0, 8, "ItemName1");
                Graphics.Draw(17, 8, item1.Cost.ToString() + " LoC");
                Graphics.Draw(29, 8, "S");

                Graphics.Draw(0, 9, player.GetGainOfSingleItem(item1) + " LPS");
                Graphics.Draw(17, 9, "#" + player.CountItemsOfType(item1).ToString());

                Graphics.Draw(0, 10, "Lorem ipsum dolor sit amet,");
                Graphics.Draw(0, 11, "consectetur adipiscing elit,");
                Graphics.Draw(0, 12, "sed do eiusmod tempor incididunt");
            }
        }

        private void DrawUpgradesScreen()
        {
            Graphics.Draw(99, 0, "Upgrades");
            if (graph.IsElementAvailableForPurchase(upgrade0))
            {
                Graphics.Draw(89, 2, "UpgradeName0");
                if (upgrade0.HasBeenPurchased)
                    Graphics.Draw(106, 2, "[PURCHASED]      ");
                else
                {
                    Graphics.Draw(106, 2, item0.Cost.ToString() + " LoC");
                    Graphics.Draw(118, 2, "X");
                }

                Graphics.Draw(89, 3, "ItemName0    x" + upgrade0.InfluencedItems[item0].ToString("f1"));

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

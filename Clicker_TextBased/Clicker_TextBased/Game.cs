using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicker_TextBased
{
    public class Game
    {
        public bool exit = false;
        public bool win = false;

        Player player;
        Graph graph;
        Item[] items;
        Upgrade[] upgrades;

        ConsoleKey inputClick;
        ConsoleKey inputExit;
        ConsoleKey DEMO_inputGenerateCurrency;
        ConsoleKey[] inputToPurchaseItems;
        ConsoleKey[] inputToPurchaseUpgrades;


        public Game()
        {
            exit = false;
            win = false;
        }


        public void Init()
        {
            player = new Player();
            items = new Item[6];
            upgrades = new Upgrade[4];
            graph = new Graph();

            inputClick = ConsoleKey.Spacebar;
            inputExit = ConsoleKey.Escape;
            DEMO_inputGenerateCurrency = ConsoleKey.D1;
            inputToPurchaseItems = new[] { ConsoleKey.Z, ConsoleKey.X, ConsoleKey.C, ConsoleKey.V, ConsoleKey.B, ConsoleKey.N };
            inputToPurchaseUpgrades = new[] { ConsoleKey.P, ConsoleKey.O, ConsoleKey.I, ConsoleKey.U };

            //Initialize items
            string[] descriptionItem0 = { "Hack an arduino robot arm that", "types hacking code for you" };
            items[0] = new Item(1.0d, 0.1d, "Robot Arm", descriptionItem0);
            graph.AddNode(items[0]);

            string[] descriptionItem1 = { "Leave a trojan process in", "a random PC that keeps hacking", "while it rests" };
            items[1] = new Item(5, 0.5d, "Zombie PC", descriptionItem1);
            graph.AddNode(items[1]);

            string[] descriptionItem2 = { "Use a RX modulator to conduct", "a mainframe cell direct to hack", "your hacking back in time" };
            // Quote from Kung Fury (2015)
            items[2] = new Item(10, 1d, "Hack Time Loop", descriptionItem2);
            graph.AddNode(items[2]);

            string[] descriptionItem3 = { "Give the Red Pill and recruit", "another hacker for you cause" };
            // References The Matrix (1999)
            items[3] = new Item(15, 1.5d, "The Red Pill", descriptionItem3);
            graph.AddNode(items[3]);

            string[] descriptionItem4 = { "Create a hack that is able to", "cut through any known Intrusion", "Countermeasures Electronics" };
            // References ICE concept created by William Gibson
            items[4] = new Item(15, 1.5d, "ICE Breaker", descriptionItem4);
            graph.AddNode(items[4]);

            string[] descriptionItem5 = { "\"Wintermute was hive mind, decision", "maker. Neuromancer was personality.", "Neuromancer was immortality\"" };
            // Quote from William Gibson's Neuromancer (1984)
            items[5] = new Item(15, 1.5d, "Allied AI", descriptionItem5);
            graph.AddNode(items[5]);

            // Initialize upgrades
            string[] descriptionUpgrade0 = { "Give keyboard gloves to your", "robot arms. It keeps them cozy", "and helps with typing" };
            upgrades[0] = new Upgrade(1.0d, items[1], 2.0f, "Keyboard Gloves", descriptionUpgrade0);
            graph.AddNode(upgrades[0]);

            string[] descriptionUpgrade1 = { "Your trojan disguises itself as", "pretty screensavers. People are", "more prone to leave it running" };
            upgrades[1] = new Upgrade(1.0d, items[2], 2.0f, "Nice Screensaver", descriptionUpgrade1);
            graph.AddNode(upgrades[1]);

            string[] descriptionUpgrade2 = { "Gain access to the Cyberspace.", "You and your allies are able to", "access its virtual physicality." };
            // References the concept of Cyberspace created by William Gibson and popularized by many other
            upgrades[2] = new Upgrade(1.0d, items[3], 2.0f, "Enter the Matrix", descriptionUpgrade2);
            graph.AddNode(upgrades[2]);

            string[] descriptionUpgrade3 = { "Finish what you've started and", "HACK THE WORLD!" };
            upgrades[3] = new Upgrade(1.0d, "Hack the World!", descriptionUpgrade3);
            graph.AddNode(upgrades[3]);


            //Initialize dependencies
            //graph.AddEdgeFromToElement(items[3], upgrades[2], 1);
            //graph.AddEdgeFromToElement(upgrades[2], items[4], 1);
            //graph.AddEdgeFromToElement(upgrades[2], items[5], 1);

            //graph.AddEdgeFromToElement(upgrades[2], upgrades[3], 1);

        }

        public void Update()
        {
            CheckInput();
            player.Update();
        }

        void CheckInput()
        {
            ConsoleKey inputReceived = ConsoleKey.PageUp; //Initialized with any value (non-nullable type)
            bool inputWasRead = false;
            while (Console.KeyAvailable) //Reads first key in buffer and flushes the rest
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
                CheckForSystemInput(inputReceived);
                CheckForItemInput(inputReceived);
                CheckForUpgradeInput(inputReceived);
            }
        }

        void CheckForSystemInput(ConsoleKey inputReceived)
        {
            if (inputReceived == inputExit)
            {
                exit = true;
            }
            else if (inputReceived == inputClick)
            {
                player.Click();
            }
            else if (inputReceived == DEMO_inputGenerateCurrency)
            {
                player.DEMO_AddCurrency(1000d);
            }
        }

        void CheckForItemInput(ConsoleKey inputReceived)
        {
            if (inputReceived == inputToPurchaseItems[0])
            {
                if (graph.IsElementAvailableForPurchase(items[0]))
                {
                    if (player.AttemptToPurchase(items[0]))
                        graph.VerifyConditionsRelatedToItem(items[0], player.CountItemsOfType(items[0]));
                }
            }
            else if (inputReceived == inputToPurchaseItems[1])
            {
                if (graph.IsElementAvailableForPurchase(items[1]))
                {
                    if (player.AttemptToPurchase(items[1]))
                        graph.VerifyConditionsRelatedToItem(items[1], player.CountItemsOfType(items[1]));
                }
            }
            else if (inputReceived == inputToPurchaseItems[2])
            {
                if (graph.IsElementAvailableForPurchase(items[2]))
                {
                    if (player.AttemptToPurchase(items[2]))
                        graph.VerifyConditionsRelatedToItem(items[2], player.CountItemsOfType(items[2]));
                }
            }
            else if (inputReceived == inputToPurchaseItems[3])
            {
                if (graph.IsElementAvailableForPurchase(items[3]))
                {
                    if (player.AttemptToPurchase(items[3]))
                        graph.VerifyConditionsRelatedToItem(items[3], player.CountItemsOfType(items[3]));
                }
            }
            else if (inputReceived == inputToPurchaseItems[4])
            {
                if (graph.IsElementAvailableForPurchase(items[4]))
                {
                    if (player.AttemptToPurchase(items[4]))
                        graph.VerifyConditionsRelatedToItem(items[4], player.CountItemsOfType(items[4]));
                }
            }
            else if (inputReceived == inputToPurchaseItems[5])
            {
                if (graph.IsElementAvailableForPurchase(items[5]))
                {
                    if (player.AttemptToPurchase(items[5]))
                        graph.VerifyConditionsRelatedToItem(items[5], player.CountItemsOfType(items[5]));
                }
            }
        }

        void CheckForUpgradeInput(ConsoleKey inputReceived)
        {
            if (inputReceived == inputToPurchaseUpgrades[0])
            {
                if (graph.IsElementAvailableForPurchase(upgrades[0]))
                    if (!upgrades[0].HasBeenPurchased)
                        if (player.AttemptToPurchase(upgrades[0]))
                            graph.VerifyConditionsRelatedToItem(upgrades[0], 1);
            }
            else if (inputReceived == inputToPurchaseUpgrades[1])
            {
                if (graph.IsElementAvailableForPurchase(upgrades[1]))
                    if (!upgrades[0].HasBeenPurchased)
                        if (player.AttemptToPurchase(upgrades[1]))
                            graph.VerifyConditionsRelatedToItem(upgrades[1], 1);
            }
            else if (inputReceived == inputToPurchaseUpgrades[2])
            {
                if (graph.IsElementAvailableForPurchase(upgrades[2]))
                    if (!upgrades[0].HasBeenPurchased)
                        if (player.AttemptToPurchase(upgrades[2]))
                            graph.VerifyConditionsRelatedToItem(upgrades[2], 1);
            }
            else if (inputReceived == inputToPurchaseUpgrades[3])
            {
                if (graph.IsElementAvailableForPurchase(upgrades[3]))
                    if (!upgrades[0].HasBeenPurchased)
                        if (player.AttemptToPurchase(upgrades[3]))
                        {
                            graph.VerifyConditionsRelatedToItem(upgrades[3], 1);
                            WinConditionReached();
                        }


            }
        }

        void WinConditionReached()
        {
            win = true;
            exit = true;
            Console.Clear();
            Graphics.Draw(49, 10, "YoU hAvE hAcKeD tHe WoRlD!");
        }

        public void Draw()
        {
            DrawItems();
            DrawUpgrades();


            Graphics.Draw(49, 10, player.CurrentCurrencyValue.ToString("f1"));
            Graphics.Draw(49, 11, "Lines of ~hacking~ Code");
        }

        private void DrawItems()
        {
            Graphics.Draw(10, 0, "Items");
            for (int i = 0; i < items.Length; i++)
            {
                if (graph.IsElementAvailableForPurchase(items[i]))
                {
                    Graphics.Draw(0, (i * 6) + 2, items[i].Name);
                    Graphics.Draw(17, (i * 6) + 2, items[i].Cost.ToString() + " LoC");
                    Graphics.Draw(29, (i * 6) + 2, inputToPurchaseItems[i].ToString());

                    Graphics.Draw(0, (i * 6) + 3, player.GetGainOfSingleItem(items[i]) + " LPS  ");
                    Graphics.Draw(17, (i * 6) + 3, "#" + player.CountItemsOfType(items[i]).ToString());

                    for (int j = 0; j < items[i].Description.Length; j++)
                    {
                        Graphics.Draw(0, (i * 6) + 4 + j, items[i].Description[j]);
                    }
                }
            }
        }

        private void DrawUpgrades()
        {
            Graphics.Draw(99, 0, "Upgrades");
            int linesOfInfluencedItems = 0;
            for (int i = 0; i < upgrades.Length; i++)
            {
                if (graph.IsElementAvailableForPurchase(upgrades[i]))
                {
                    Graphics.Draw(89, (i * 6) + 2 + linesOfInfluencedItems, upgrades[i].Name);
                    if (upgrades[i].HasBeenPurchased)
                        Graphics.Draw(106, (i * 6) + 2 + linesOfInfluencedItems, "[PURCHASED]  ");
                    else
                    {
                        Graphics.Draw(106, (i * 6) + 2 + linesOfInfluencedItems, upgrades[i].Cost.ToString() + " LoC");
                        Graphics.Draw(118, (i * 6) + 2 + linesOfInfluencedItems, inputToPurchaseUpgrades[i].ToString());
                    }

                    foreach (Item item in upgrades[i].InfluencedItems.Keys)
                    {
                        Graphics.Draw(89, (i * 6) + 3 + linesOfInfluencedItems, item.Name + "    x" + upgrades[i].InfluencedItems[item].ToString("f1"));
                        linesOfInfluencedItems++;
                    }

                    for (int j = 0; j < upgrades[i].Description.Length; j++)
                    {
                        Graphics.Draw(89, (i * 6) + 3 + j + linesOfInfluencedItems, upgrades[i].Description[j]);
                    }
                }
            }
        }
    }
}

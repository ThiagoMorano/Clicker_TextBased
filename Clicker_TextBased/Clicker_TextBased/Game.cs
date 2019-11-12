﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicker_TextBased
{
    public class Game
    {
        public bool exit = false;

        Player player;
        Graph graph;
        Item[] items;
        Upgrade[] upgrades;

        ConsoleKey[] inputToPurchaseItems;
        ConsoleKey[] inputToPurchaseUpgrades;

        public Game()
        {
            exit = false;
        }


        public void Init()
        {
            player = new Player();
            items = new Item[6];
            upgrades = new Upgrade[4];
            graph = new Graph();

            inputToPurchaseItems = new[] { ConsoleKey.A, ConsoleKey.S, ConsoleKey.D, ConsoleKey.F, ConsoleKey.G, ConsoleKey.H };
            inputToPurchaseUpgrades = new[] { ConsoleKey.X, ConsoleKey.C, ConsoleKey.V, ConsoleKey.B };

            string[] descriptionItem = { "Lorem ipsum dolor sit amet,", "consectetur adipiscing elit,", "sed do eiusmod tempor incididunt" };

            string[] descriptionItem0 = { "Hack an arduino robot arm that", "types hacking code for you" };
            items[0] = new Item(1.0d, 0.1d, "Robot Arm", descriptionItem0);
            graph.AddNode(items[0]);
            //graph.AddEdgeFromToElement(item0, item1, 5);


            string[] descriptionItem1 = { "Leave a trojan process in", "a random PC that keeps hacking", "while it rests" };
            items[1] = new Item(5, 0.5d, "Zombie PC", descriptionItem1);
            graph.AddNode(items[1]);


            string[] descriptionItem2 = { "Use a RX modulator to conduct", "a mainframe cell direct to hack", "your hacking back in time" };
            items[2] = new Item(10, 1d, "Hack Time Loop", descriptionItem2);
            graph.AddNode(items[2]);

            string[] descriptionItem3 = { "Give the Red Pill and recruit", "another hacker for you cause" };
            items[3] = new Item(15, 1.5d, "The Red Pill", descriptionItem3);
            graph.AddNode(items[3]);

            string[] descriptionItem4 = { "Create a hack that is able to", "cut through any known Intrusion", "Countermeasures Electronics" };
            items[4] = new Item(15, 1.5d, "ICE Breaker", descriptionItem4);
            graph.AddNode(items[4]);

            string[] descriptionItem5 = { "\"Wintermute was hive mind, decision", "maker. Neuromancer was personality.", "Neuromancer was immortality\n" };
            items[5] = new Item(15, 1.5d, "Allied AI", descriptionItem5);
            graph.AddNode(items[5]);


            string[] descriptionUpgrade0 = { "Give keyboard gloves to your", "robot arms. It keeps them cozy", "and helps with typing" };
            upgrades[0] = new Upgrade(1.0d, items[1], 2.0f, "Keyboard Gloves", descriptionUpgrade0);
            graph.AddNode(upgrades[0]);

            string[] descriptionUpgrade1 = { "Your trojan disguises itself as", "pretty screensavers. People are", "more prone to leave it running" };
            upgrades[1] = new Upgrade(1.0d, items[2], 2.0f, "Nice Screensaver", descriptionUpgrade1);
            graph.AddNode(upgrades[1]);

            string[] descriptionUpgrade2 = { "Gain access to the Cyberspace.", "You and your allies are able to", "access its virtual physicality." };
            upgrades[2] = new Upgrade(1.0d, items[3], 2.0f, "Enter the Matrix", descriptionUpgrade2);
            graph.AddNode(upgrades[2]);
            //graph.AddEdgeFromToElement(items[3], upgrades[2], 1);
            //graph.AddEdgeFromToElement(upgrades[2], items[4], 1);
            //graph.AddEdgeFromToElement(upgrades[2], items[5], 1);


            string[] descriptionUpgrade3 = { "Finish what you've started and", "HACK THE WORLD!" };
            upgrades[3] = new Upgrade(1.0d, "Hack the World!", descriptionUpgrade3);
            graph.AddNode(upgrades[3]);




            //graph.AddEdgeFromToElement(upgrades[2], upgrades[3], 1);

        }

        public void Update()
        {

            CheckInput();

            player.Update();
        }

        void CheckInput()
        {
            ConsoleKey inputReceived = ConsoleKey.Z; //Initialized with any value (non-nullable type)
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
                switch (inputReceived)
                {
                    case ConsoleKey.Spacebar:
                        player.Click();
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
        }

        public void Draw()
        {
            DrawItems();
            DrawUpgrades();


            Graphics.Draw(49, 10, player.CurrentCurrencyValue.ToString("f3"));
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

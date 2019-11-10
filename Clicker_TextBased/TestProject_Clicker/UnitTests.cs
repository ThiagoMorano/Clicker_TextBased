using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clicker_TextBased;

namespace TestProject_Clicker
{
    //Arrange
    //Act
    //Assert


    [TestClass]
    public class Graph_Tests
    {
        [TestMethod]
        public void GetNodeThatContainsElement_NodeNotYetAdded()
        {
            Graph graph = new Graph();
            Item item0 = new Item();

            Assert.IsNull(graph.GetNodeThatContainsElement(item0));
        }

        [TestMethod]
        public void AddNode_InsertionOfNodeByElement()
        {
            Graph graph = new Graph();
            Item item0 = new Item();

            graph.AddNode(item0);
            Node node = graph.GetNodeThatContainsElement(item0);

            Assert.ReferenceEquals(item0, node.Element);
        }

        [TestMethod]
        public void AddNode_NodesOfDifferentElementsAreDifferent()
        {
            Graph graph = new Graph();
            Item item0 = new Item();
            Item item1 = new Item();

            graph.AddNode(item0);
            graph.AddNode(item1);
            Node node0 = graph.GetNodeThatContainsElement(item0);
            Node node1 = graph.GetNodeThatContainsElement(item1);

            Assert.AreNotSame(node0, node1);
        }

        [TestMethod]
        public void AddNode_NodeOfElementAlreadyExists()
        {
            Graph graph = new Graph();
            Item item0 = new Item();

            graph.AddNode(item0);

            Assert.ThrowsException<ArgumentException>(() => graph.AddNode(item0));
        }

        [TestMethod]
        public void AddNode_ElementNull()
        {
            Graph graph = new Graph();

            Assert.ThrowsException<ArgumentNullException>(() => graph.AddNode(null));
        }


        [TestMethod]
        public void AddNode_and_AddEdge_FromElements()
        {
            Graph graph = new Graph();
            Item item0 = new Item();
            Item item1 = new Item();

            graph.AddNode(item0);
            graph.AddNode(item1);
            graph.AddEdgeFromToElement(item0, item1);

            Assert.ReferenceEquals(item1, graph.GetNodeThatContainsElement(item0).GetOutboundEdgeToElement(item1).EndNode);
            Assert.ReferenceEquals(item0, graph.GetNodeThatContainsElement(item0).GetOutboundEdgeToElement(item1).StartNode);
        }

        [TestMethod]
        public void AddEdgeFromToElement_AddConditionBetweenNodes()
        {
            Graph graph = new Graph();
            Item item0 = new Item();
            Item item1 = new Item();
            graph.AddNode(item0);
            graph.AddNode(item1);

            graph.AddEdgeFromToElement(item0, item1, 1);

            Assert.IsFalse(graph.GetNodeThatContainsElement(item0).GetOutboundEdgeToElement(item1).Condition.ConditionMet);
        }

        [TestMethod]
        public void VerifyCondition_ConditionShouldBeMet()
        {
            Graph graph = new Graph();
            Item item0 = new Item();
            Item item1 = new Item();
            graph.AddNode(item0);
            graph.AddNode(item1);
            long amountOfItemsRequiredInCondition = 2;
            graph.AddEdgeFromToElement(item0, item1, amountOfItemsRequiredInCondition);

            graph.VerifyConditionsRelatedToItem(item0, amountOfItemsRequiredInCondition);

            Assert.IsTrue(graph.GetNodeThatContainsElement(item0).GetOutboundEdgeToElement(item1).Condition.ConditionMet);
        }

        [TestMethod]
        public void VerifyCondition_ConditionNotYetMet()
        {
            Graph graph = new Graph();
            Item item0 = new Item();
            Item item1 = new Item();
            graph.AddNode(item0);
            graph.AddNode(item1);
            long itemsRequiredByCondition = 3;
            long currentAmountOfItems = 2;
            graph.AddEdgeFromToElement(item0, item1, itemsRequiredByCondition);

            graph.VerifyConditionsRelatedToItem(item0, currentAmountOfItems);

            Assert.IsFalse(graph.GetNodeThatContainsElement(item0).GetOutboundEdgeToElement(item1).Condition.ConditionMet);
        }

        [TestMethod]
        public void VerifyCondition_MultipleConditionsPartiallyMet()
        {
            Graph graph = new Graph();
            Item preconditionalItem0 = new Item(1.0d, 1.0d);
            Item preconditionalItem1 = new Item(1.0d, 1.0d);
            Item blockedItem = new Item(1.0d, 1.0d);
            graph.AddNode(preconditionalItem0);
            graph.AddNode(preconditionalItem1);
            graph.AddNode(blockedItem);
            long itemsRequiredByConditions = 1;
            graph.AddEdgeFromToElement(preconditionalItem0, blockedItem, itemsRequiredByConditions);
            graph.AddEdgeFromToElement(preconditionalItem1, blockedItem, itemsRequiredByConditions);

            long currentAmountOfItems = 1;
            graph.VerifyConditionsRelatedToItem(preconditionalItem0, currentAmountOfItems);

            Assert.IsFalse(graph.IsElementAvailableForPurchase(blockedItem));
        }

        [TestMethod]
        public void VerifyCondition_MultipleConditionsShouldBeFulfilled()
        {
            Graph graph = new Graph();
            Item preconditionalItem0 = new Item(1.0d, 1.0d);
            Item preconditionalItem1 = new Item(1.0d, 1.0d);
            Item blockedItem = new Item(1.0d, 1.0d);
            graph.AddNode(preconditionalItem0);
            graph.AddNode(preconditionalItem1);
            graph.AddNode(blockedItem);
            long itemsRequiredByConditions = 1;
            graph.AddEdgeFromToElement(preconditionalItem0, blockedItem, itemsRequiredByConditions);
            graph.AddEdgeFromToElement(preconditionalItem1, blockedItem, itemsRequiredByConditions);

            long currentAmountOfItems = 1;
            graph.VerifyConditionsRelatedToItem(preconditionalItem0, currentAmountOfItems);
            graph.VerifyConditionsRelatedToItem(preconditionalItem1, currentAmountOfItems);

            Assert.IsTrue(graph.IsElementAvailableForPurchase(blockedItem));
        }
    }

    [TestClass]
    public class Item_Tests
    {

        [TestMethod]
        public void Item_DefaultConstructor_ItemExists()
        {
            Item item = new Item();

            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void Item_ConstructorWithValues_CostProperlySet()
        {
            double cost = 1;
            double amountGenerated = 2;

            Item item = new Item(cost, amountGenerated);

            Assert.AreEqual(cost, item.Cost);
        }

        [TestMethod]
        public void Item_ConstructorWithValues_GainProperlySet()
        {
            double cost = 1;
            double amountGenerated = 2;

            Item item = new Item(cost, amountGenerated);

            Assert.AreEqual(amountGenerated, item.ItemGainPerSecond);
        }
    }

    [TestClass]
    public class Player_Tests
    {
        [TestMethod]
        public void PlayerConstructorWithValues_ProperlySetClickingValue()
        {
            double valueGeneratedByClicking = 1;
            Player player = new Player(valueGeneratedByClicking);

            Assert.AreEqual(valueGeneratedByClicking, player.ValueGeneratedByClick);
        }

        [TestMethod]
        public void PlayerClick_ProducesCorrectAmountOfCurrency()
        {
            double valueFromClicking = 1;
            Player player = new Player(valueFromClicking);

            double previousCurrency = player.CurrentCurrencyValue;
            player.Click();

            Assert.AreEqual(valueFromClicking, player.CurrentCurrencyValue - previousCurrency);
        }

        [TestMethod]
        public void Player_AttemptToPurchase_NotEnoughMoney()
        {
            Item item0 = new Item(1, 1);
            Player player = new Player();

            player.AttemptToPurchase(item0);

            Assert.AreEqual(0, player.CountItemsOfType(item0));
        }

        [TestMethod]
        public void Player_AttemptToPurchase_ProperlyPurchasesItem()
        {
            Item item0 = new Item(0, 1);
            Player player = new Player();

            player.AttemptToPurchase(item0);

            Assert.AreEqual(1, player.CountItemsOfType(item0));
        }

        [TestMethod]
        public void Player_AttemptToPurchase_PurchaseUnlocksNextItem()
        {
            Player player = new Player(2.0d);
            Graph graph = new Graph();
            Item item0 = new Item(1.0d, 1.0d);
            Item item1 = new Item();
            graph.AddNode(item0);
            graph.AddNode(item1);
            long amountOfItemsRequiredInCondition = 2;
            graph.AddEdgeFromToElement(item0, item1, amountOfItemsRequiredInCondition);

            player.Click();
            player.AttemptToPurchase(item0);
            if (player.AttemptToPurchase(item0))
            {
                graph.VerifyConditionsRelatedToItem(item0, player.CountItemsOfType(item0));
            }

            Assert.IsTrue(graph.GetNodeThatContainsElement(item0).GetOutboundEdgeToElement(item1).Condition.ConditionMet);
        }

        [TestMethod]
        public void Player_Update_GeneratesRightAmountOfCurrency()
        {
            Item item0 = new Item(0, 1);
            Player player = new Player();

            player.AttemptToPurchase(item0);
            //player.Update(1.0d);   NEED TO MOCK
        }

        [TestMethod]
        public void Player_AttemptToPurchase_PurchasesUpgrade()
        {
            Upgrade upgrade = new Upgrade();
            Player player = new Player();

            Assert.IsTrue(player.AttemptToPurchase(upgrade));
        }

        [TestMethod]
        public void Player_GetValueGeneratedByItem_CorrectValue()
        {
            double valueOfClick = 1.0d;
            Player player = new Player(valueOfClick);
            double valueGeneratedByItem = 1.0d;
            Item item0 = new Item(1.0d, valueGeneratedByItem);

            player.Click();
            player.AttemptToPurchase(item0);

            Assert.AreEqual(valueGeneratedByItem, player.GetGainOfItemsOfTypeInInventory(item0));
        }

        [TestMethod]
        public void Player_GetValueGeneratedByItem_ItemNotFoundReturns0()
        {
            Player player = new Player();

            Assert.AreEqual(0.0d, player.GetGainOfItemsOfTypeInInventory(new Item()));
        }

        [TestMethod]
        public void Player_GetValueGeneratedByItem_ProperlyCountsUpgrade()
        {
            double valueOfClick = 2.0d;
            Player player = new Player(valueOfClick);
            Item item = new Item(1.0d, 1.0d);
            float multiplier = 2.0f;
            Upgrade upgrade = new Upgrade(1.0d, item, multiplier);

            player.Click();
            player.AttemptToPurchase(item);
            player.AttemptToPurchase(upgrade);

            Assert.AreEqual(item.ItemGainPerSecond * multiplier, player.GetGainOfItemsOfTypeInInventory(item));
        }
    }

    [TestClass]
    public class Upgrade_Tests
    {
        [TestMethod]
        public void Upgrade_Constructor_ItemExists()
        {
            Upgrade upgrade = new Upgrade();

            Assert.IsNotNull(upgrade);
        }

        [TestMethod]
        public void Upgrade_Constructor_CostParameter()
        {
            double costOfItem = 1.0d;
            Upgrade upgrade = new Upgrade(costOfItem);

            Assert.AreEqual(costOfItem, upgrade.Cost);
        }

        [TestMethod]
        public void Upgrade_Constructor_SingleItem()
        {
            Item item = new Item();
            Upgrade upgrade = new Upgrade(1.0d, item, 1.0f);

            Assert.AreEqual(1.0f, upgrade.InfluencedItems[item]);
        }

        [TestMethod]
        public void Upgrade_AddInfluencedItem_SingleItem()
        {
            Item item = new Item();
            Upgrade upgrade = new Upgrade();

            upgrade.AddInfluencedItem(item, 1.0f);

            Assert.AreEqual(1.0f, upgrade.InfluencedItems[item]);
        }

        [TestMethod]
        public void Upgrade_AddInfluencedItem_ListOfItems()
        {
            Item item0 = new Item();
            Item item1 = new Item();
            Item item2 = new Item();
            Upgrade upgrade = new Upgrade(1.0d);

            Item[] items = { item0, item1, item2 };
            float[] multipliers = { 0.0f, 1.0f, 2.0f };
            upgrade.AddInfluencedItem(items, multipliers);

            Assert.AreEqual(0.0f, upgrade.InfluencedItems[item0]);
            Assert.AreEqual(1.0f, upgrade.InfluencedItems[item1]);
            Assert.AreEqual(2.0f, upgrade.InfluencedItems[item2]);
        }

        [TestMethod]
        public void Upgrade_HasBeenPurchased_SetToTrueAfterPlayerPurchase()
        {
            Player player = new Player(1.0d);
            Upgrade upgrade = new Upgrade(1.0d, new Item(), 1.0f);

            player.Click();
            player.AttemptToPurchase(upgrade);

            Assert.IsTrue(upgrade.HasBeenPurchased);
        }
    }

    [TestClass]
    public class Game_Tests
    {
        [TestMethod]
        public void Game_Constructor_DefaultScreenIsItem()
        {
            Game game = new Game();

            Screen currentScreen = game.currentScreen;

            Assert.AreEqual(Screen.items, currentScreen);
        }

        [TestMethod]
        public void Game_SwitchCurrentScreenTo_ShouldSwitchProperly()
        {
            Game game = new Game();
            Screen screen = Screen.upgrades;
            
            game.SwitchCurrentScreenTo(screen);

            Assert.AreEqual(screen, game.currentScreen);
        }
    }


    [TestClass]
    public class Input_Tests
    {

        String functionExample()
        {
            return "value";
        }

        [TestMethod]
        public void Input_RegisterInput_ProperlyRegisters()
        {

            //Input.RegisterInput(ConsoleKey.A, () => functionExample);
        }
    }
}

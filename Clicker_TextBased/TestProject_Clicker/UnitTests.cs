using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clicker_TextBased;

namespace TestProject_Clicker
{
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
            //Arrange
            Graph graph = new Graph();
            Item item0 = new Item();
            Item item1 = new Item();
            //Act
            graph.AddNode(item0);
            graph.AddNode(item1);
            graph.AddEdgeFromToElement(item0, item1);
            //Assert
            Assert.ReferenceEquals(item1, graph.GetNodeThatContainsElement(item0).GetOutboundEdgeToElement(item1).EndNode);
            Assert.ReferenceEquals(item0, graph.GetNodeThatContainsElement(item0).GetOutboundEdgeToElement(item1).StartNode);
        }

        [TestMethod]
        public void AddEdgeFromToElement_AddConditionBetweenNodes()
        {
            //Arrange
            Graph graph = new Graph();
            Item item0 = new Item();
            Item item1 = new Item();
            graph.AddNode(item0);
            graph.AddNode(item1);

            //Act
            graph.AddEdgeFromToElement(item0, item1, 1);

            //Assert
            Assert.IsFalse(graph.GetNodeThatContainsElement(item0).GetOutboundEdgeToElement(item1).Condition.ConditionMet);
        }

        [TestMethod]
        public void VerifyCondition_ConditionShouldBeMet()
        {
            //Arrange
            Graph graph = new Graph();
            Item item0 = new Item();
            Item item1 = new Item();
            graph.AddNode(item0);
            graph.AddNode(item1);
            long amountOfItems = 2;
            graph.AddEdgeFromToElement(item0, item1, amountOfItems);

            //Act
            graph.VerifyConditionsRelatedToItem(item0, amountOfItems);

            //Assert
            Assert.IsTrue(graph.GetNodeThatContainsElement(item0).GetOutboundEdgeToElement(item1).Condition.ConditionMet);
        }

        [TestMethod]
        public void VerifyCondition_ConditionNotYetMet()
        {
            //Arrange
            Graph graph = new Graph();
            Item item0 = new Item();
            Item item1 = new Item();
            graph.AddNode(item0);
            graph.AddNode(item1);
            long itemsRequiredByCondition = 3;
            long amountOfItems = 2;
            graph.AddEdgeFromToElement(item0, item1, itemsRequiredByCondition);

            //Act
            graph.VerifyConditionsRelatedToItem(item0, amountOfItems);

            //Assert
            Assert.IsFalse(graph.GetNodeThatContainsElement(item0).GetOutboundEdgeToElement(item1).Condition.ConditionMet);
        }
    }
}

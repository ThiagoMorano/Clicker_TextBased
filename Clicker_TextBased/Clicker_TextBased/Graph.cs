using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicker_TextBased
{
    public class Graph
    {
        Dictionary<Element, Node> _nodes;
        List<Edge> _edges; // Should this be kept?

        public Graph()
        {
            _nodes = new Dictionary<Element, Node>();
            _edges = new List<Edge>();
        }

        /// <summary>
        /// Add a node that contains an element
        /// </summary>
        /// <param name="element"></param>
        public void AddNode(Element element)
        {
            try
            {
                _nodes.Add(element, new Node(element));
            }
            catch (ArgumentException ex)
            {
                if (ex is ArgumentNullException)
                {
                    throw (new ArgumentNullException("Element provided is null"));
                }
                else
                {
                    throw (new ArgumentException("Graph already has a node with element " + element.ToString()));
                }
            }
        }

        /// <summary>
        /// Returns node with element. Null if there's no such node.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public Node GetNodeThatContainsElement(Element element)
        {
            if (_nodes.ContainsKey(element))
                return _nodes[element];
            else
                return null;
        }

        /// <summary>
        /// Adds an edge from node with startElemento to node with endElement.
        /// </summary>
        /// <param name="startElement"></param>
        /// <param name="endElement"></param>
        /// <param name="amountRequiredInCondition"></param>
        public void AddEdgeFromToElement(Element startElement, Element endElement, long amountRequiredInCondition = 1)
        {
            if (_nodes.ContainsKey(startElement) && _nodes.ContainsKey(endElement))
            {
                Edge edge = new Edge(_nodes[startElement], _nodes[endElement], amountRequiredInCondition);
                _nodes[startElement].AddOutboundEdge(edge);
                _nodes[endElement].AddInboundEdge(edge);
                _edges.Add(edge);
            }
        }

        /// <summary>
        /// Verifies if conditions of outbound edges from node with element would be met with amountOFItems.
        /// Sets the 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="amountOfItems"></param>
        public void VerifyConditionsRelatedToItem(Element element, long amountOfItems)
        {
            if (_nodes.ContainsKey(element))
            {
                foreach (Edge edge in _nodes[element].OutboundEdges)
                {
                    edge.VerifyIfConditionMet(amountOfItems);
                }
            }
            else
            {
                throw (new KeyNotFoundException("Node with element" + element.ToString() + "not found"));
            }
        }

        /// <summary>
        /// Returns whether the element is available for purchase. Throws an exception if the graph doesn't have a node with given element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool IsElementAvailableForPurchase(Element element)
        {
            if (_nodes.ContainsKey(element))
            {
                return _nodes[element].AvailableForPurchase;
            }
            else
            {
                throw (new KeyNotFoundException("Node with element" + element.ToString() + "not found"));
            }
        }
    }

    public class Node
    {
        public Element Element { get; }
        public List<Edge> OutboundEdges { get; }
        List<Edge> _inboundEdges;
        internal bool AvailableForPurchase { get; set; } = true;

        internal Node(Element element)
        {
            Element = element;
            OutboundEdges = new List<Edge>();
            _inboundEdges = new List<Edge>();
        }

        public void AddOutboundEdge(Edge edge)
        {
            if (!OutboundEdges.Contains(edge))
            {
                OutboundEdges.Add(edge);
            }
        }

        public void AddInboundEdge(Edge edge)
        {
            AvailableForPurchase = false; // To add an inbound edge means the item will be locked until the related condition is met
            if (!_inboundEdges.Contains(edge))
            {
                _inboundEdges.Add(edge);
            }
        }

        /// <summary>
        /// Returns the outbound edge until a element. Returns null if no edge exists.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public Edge GetOutboundEdgeToElement(Element element)
        {
            Edge edgeToElement = null;
            foreach (Edge edge in OutboundEdges)
            {
                if (edge.EndNode.Element.Equals(element))
                    edgeToElement = edge;
            }
            return edgeToElement;
        }

        /// <summary>
        /// Sets node as available for purchase
        /// </summary>
        internal void MakeNodeAvailableForPurchase()
        {
            AvailableForPurchase = true;
            //Raise event about condition met
        }
    }

    public class Edge
    {
        public Node StartNode { get; }
        public Node EndNode { get; }
        public Condition Condition { get; }


        internal Edge(Node start, Node end)
        {
            StartNode = start;
            EndNode = end;
            Condition = new Condition(start.Element);
        }
        internal Edge(Node start, Node end, long amountRequired = 1)
        {
            StartNode = start;
            EndNode = end;
            Condition = new Condition(start.Element, amountRequired);
        }

        internal void VerifyIfConditionMet(long amountOfItems)
        {
            if (!Condition.ConditionMet)
            {
                if (Condition.RefreshConditionStatus(amountOfItems))
                {
                    EndNode.MakeNodeAvailableForPurchase();
                }
            }
        }
    }

    public class Condition
    {
        Element _associatedElement;
        long _amountRequired;

        public bool ConditionMet { get; private set; } = false;

        internal Condition(Element associatedElement, long amountRequired = 1)
        {
            _associatedElement = associatedElement;
            _amountRequired = amountRequired;
        }

        /// <summary>
        /// Refreshes the conditions based on the amount of items
        /// </summary>
        /// <param name="currentAmountOfItems"></param>
        /// <returns>Status of condition</returns>
        internal bool RefreshConditionStatus(long currentAmountOfItems)
        {
            if (currentAmountOfItems >= _amountRequired)
            {
                ConditionMet = true;
                return true;
            }
            return false;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicker_TextBased
{

    /// <summary>
    /// In-game elements that can be purchased by the player
    /// </summary>
    public class Element
    {
        protected double _cost;
        protected string _name;
        protected string[] _description;

        public double Cost { get { return _cost; } }
        public string Name { get { return _name; } }
        public string[] Description { get { return _description; } }
    }

    /// <summary>
    /// Elements that generate currency over time
    /// </summary>
    public class Item : Element
    {
        double _itemGainPerSecond;

        public double ItemGainPerSecond { get { return _itemGainPerSecond; } }


        public Item()
        {
            _itemGainPerSecond = 0.1d;
            _cost = 1.0d;
        }

        public Item(double costToPurchase, double gainPerSecond)
        {
            _cost = costToPurchase;
            _itemGainPerSecond = gainPerSecond;
        }

        public Item(double costToPurchase, double gainPerSecond, string name, string[] description)
        {
            _cost = costToPurchase;
            _itemGainPerSecond = gainPerSecond;
            _name = name;
            _description = description;
        }
    }

    /// <summary>
    /// Elements that influence the production over time of items
    /// </summary>
    public class Upgrade : Element
    {

        Dictionary<Item, float> _influencedItems;
        public bool HasBeenPurchased { get; set; } = false;

        public Dictionary<Item, float> InfluencedItems { get { return _influencedItems; } }


        public Upgrade(double costToPurchase = 0.0d)
        {
            _cost = costToPurchase;
            _influencedItems = new Dictionary<Item, float>();
        }

        public Upgrade(double costToPurchase, Item item, float multiplier)
        {
            _cost = costToPurchase;

            _influencedItems = new Dictionary<Item, float>();
            _influencedItems.Add(item, multiplier);
        }

        public Upgrade(double costToPurchase, Item item, float multiplier, string name, string[] description)
        {
            _cost = costToPurchase;

            _influencedItems = new Dictionary<Item, float>();
            _influencedItems.Add(item, multiplier);

            _name = name;
            _description = description;
        }

        public Upgrade(double costToPurchase, string name, string[] description)
        {
            _cost = costToPurchase;

            _influencedItems = new Dictionary<Item, float>();

            _name = name;
            _description = description;
        }

        public void AddInfluencedItem(Item item, float multiplier)
        {
            _influencedItems.Add(item, multiplier);
        }
        public void AddInfluencedItem(Item[] items, float[] multipliers)
        {
            if (items.Length == multipliers.Length)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    _influencedItems.Add(items[i], multipliers[i]);
                }
            }
            else
                throw (new ArgumentOutOfRangeException("Expected same amount of items and multipliers"));
        }
    }
}

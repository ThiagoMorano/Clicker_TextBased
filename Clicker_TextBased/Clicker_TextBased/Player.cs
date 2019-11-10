using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicker_TextBased
{
    public class Player
    {
        double _currentCurrencyValue;
        double _valueGeneratedByClick;
        Dictionary<Item, long> _inventory;
        Dictionary<Item, float> _itemGainMultiplier;

        public double CurrentCurrencyValue { get { return _currentCurrencyValue; } }
        public double ValueGeneratedByClick { get { return _valueGeneratedByClick; } }


        public Player()
        {
            _currentCurrencyValue = 0;
            _valueGeneratedByClick = 0.1d;
            _inventory = new Dictionary<Item, long>();
            _itemGainMultiplier = new Dictionary<Item, float>();
        }
        public Player(double valueGeneratedByClicking)
        {
            _valueGeneratedByClick = valueGeneratedByClicking;
            _inventory = new Dictionary<Item, long>();
            _itemGainMultiplier = new Dictionary<Item, float>();
        }

        public void Click()
        {
            _currentCurrencyValue += _valueGeneratedByClick;
        }

        public void Update()
        {
            GenerateCurrencyFromItems(Time.DeltaTime);
        }

        /// <summary>
        /// Generate currency based on items currently present in the inventory 
        /// </summary>
        /// <param name="elapsedTime"></param>
        private void GenerateCurrencyFromItems(double elapsedTime)
        {
            foreach (Item item in _inventory.Keys)
            {
                _currentCurrencyValue += GetGainOfItemsOfTypeInInventory(item) * elapsedTime;
            }
        }

        public double GetGainOfItemsOfTypeInInventory(Item item)
        {
            if (_inventory.ContainsKey(item))
            {
                return _inventory[item] * item.ItemGainPerSecond * _itemGainMultiplier[item];
            }
            else
            {
                return 0.0d;
                //throw (new KeyNotFoundException("Player doesn't have item given as parameter"));
            }
        }
        

        public double GetGainOfSingleItem(Item item)
        {
            if (_itemGainMultiplier.ContainsKey(item))
            {
                return item.ItemGainPerSecond * _itemGainMultiplier[item];
            }
            else
            {
                return item.ItemGainPerSecond;
            }
        }

        /// <summary>
        /// Purchase the element if player has enough currency 
        /// </summary>
        /// <param name="element"></param>
        public bool AttemptToPurchase(Element element)
        {
            if (element.Cost < CurrentCurrencyValue || Math.Abs(CurrentCurrencyValue - element.Cost) < 0.05)
            {
                Purchase(element);
                return true;
            }
            return false;
        }

        private void Purchase(Element element)
        {
            _currentCurrencyValue -= element.Cost;
            if (element is Item)
                AddItemIntoInventory(element as Item);
            else if (element is Upgrade)
                AddMultiplier(element as Upgrade);
        }

        private void AddItemIntoInventory(Item item)
        {
            if (_inventory.ContainsKey(item))
            {
                _inventory[item]++;
            }
            else
            {
                _inventory.Add(item, 1);
                if (!_itemGainMultiplier.ContainsKey(item))
                    _itemGainMultiplier.Add(item, 1);
            }
        }

        private void AddMultiplier(Upgrade upgrade)
        {
            foreach (Item item in upgrade.InfluencedItems.Keys)
            {
                if (!_itemGainMultiplier.ContainsKey(item))
                    _itemGainMultiplier.Add(item, upgrade.InfluencedItems[item]);
                else
                    _itemGainMultiplier[item] *= upgrade.InfluencedItems[item];
            }
            upgrade.HasBeenPurchased = true;
        }

        /// <summary>
        /// Returns amount of item in player's inventory
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public long CountItemsOfType(Item item)
        {
            if (_inventory.ContainsKey(item))
                return _inventory[item];
            else
                return 0;
        }
    }
}

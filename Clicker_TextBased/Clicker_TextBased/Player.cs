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

        public double CurrentCurrencyValue { get { return _currentCurrencyValue; } }
        public double ValueGeneratedByClick { get { return _valueGeneratedByClick; } }


        public Player()
        {
            _currentCurrencyValue = 0;
            _valueGeneratedByClick = 0.1d;
            _inventory = new Dictionary<Item, long>();
        }
        public Player(double valueGeneratedByClicking)
        {
            _valueGeneratedByClick = valueGeneratedByClicking;
            _inventory = new Dictionary<Item, long>();
        }


        public void Update()
        {
            GenerateCurrencyFromItems(Time.DeltaTime);
        }

        public void Click()
        {
            _currentCurrencyValue += _valueGeneratedByClick;
        }

        /// <summary>
        /// Generate currency based on items currently present in the inventory 
        /// </summary>
        /// <param name="elapsedTime"></param>
        private void GenerateCurrencyFromItems(double elapsedTime)
        {
            foreach (Item item in _inventory.Keys)
            {
                _currentCurrencyValue += item.ItemGainPerSecond * (double)_inventory[item] * elapsedTime;
            }
        }

        /// <summary>
        /// Purchase the item if player has enough currency 
        /// </summary>
        /// <param name="item"></param>
        public bool AttemptToPurchase(Item item)
        {
            if (item.Cost < CurrentCurrencyValue || Math.Abs(CurrentCurrencyValue - item.Cost) < 0.05)
            {
                Purchase(item);
                return true;
            }
            return false;
        }

        private void Purchase(Item item)
        {
            _currentCurrencyValue -= item.Cost;
            AddIntoInventory(item);
        }

        private void AddIntoInventory(Item item)
        {
            if (_inventory.ContainsKey(item))
            {
                _inventory[item]++;
            }
            else
            {
                _inventory.Add(item, 1);
            }
        }

        public long CountItemsOfType(Item item)
        {
            if (_inventory.ContainsKey(item))
                return _inventory[item];
            else
                return 0;
        }
    }
}

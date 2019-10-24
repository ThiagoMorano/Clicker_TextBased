using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicker_TextBased
{
    class Item
    {
        double _itemGainPerSecond;
        double _cost;
//        string _graphics;

        public double ItemGainPerSecond { get { return _itemGainPerSecond; } }
        public double Cost { get { return _cost; } }

        public Item()
        {
            _itemGainPerSecond = 0.2d;
            _cost = 1.0d;
        }
    }
}

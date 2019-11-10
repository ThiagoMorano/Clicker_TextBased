using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicker_TextBased
{
    public class GUI
    {
        public bool newItemAvailable = false;
        public bool newUpgradeAvailable = false;


        public void SwitchScreenTo(Screen screen)
        {

        }

        public void Draw()
        {
            //DrawHeadline();

            /*Graphics.Draw(0, 1, "Currency: " + player.CurrentCurrencyValue.ToString("f3"));
            if (graph.IsElementAvailableForPurchase(item0))
            {
                Graphics.Draw(0, 3, "Press A to purchase Item0 for " + item0.Cost.ToString());
                Graphics.Draw(0, 4, "Item0 : " + player.CountItemsOfType(item0).ToString() + " producing " + player.GetValueGeneratedByItem(item0).ToString("f1"));
            }*/
        }

        public void DrawItem(Item item)
        {

        }

        /* void DrawHeadline()
         {
             if (Game.CurrentScreen == Screen.items)
             {
                 Graphics.Draw(0, 0, "_| Items |____|_Upgrades");
                 if (newUpgradeAvailable)
                     Graphics.DrawAfter("*");
                 else
                     Graphics.DrawAfter("_");
                 Graphics.DrawAfter("|____");
             }
             else if (Game.CurrentScreen == Screen.upgrades)
             {
                 Graphics.Draw(0, 0, "_|_Items");
                 if (newItemAvailable)
                     Graphics.DrawAfter("*");
                 else
                     Graphics.DrawAfter("_");
                 Graphics.DrawAfter("|____| Upgrades |____");
             }

         }*/
    }
}

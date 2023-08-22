using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedAdventureGame
{
    public class Player
    {
        private int inventorySize;
        public List<IPortable> Inventory { get; set; }
        public MapLocation Location { get; set; }
        public int MaxInventory { get; set; }
        Player(MapLocation location)
        {
            //Set the player's starting location.
            Location = location;

            inventorySize = 1;
            MaxInventory = 6;
            Inventory = new List<IPortable>(MaxInventory);
        }
        public bool AddInventoryItem(IPortable item)
        {
            if (inventorySize <= MaxInventory)
            {
                Inventory.Add(item);
                inventorySize += 1;
                return true;
            }
            else
                return false;
            
        }

        public void RemoveInventoryItem(IPortable item)
        {
            if (inventorySize > 0)
            {
                Inventory.Remove(item);
                inventorySize -= 1;
            }
                
        }
        private void Calc()
        {

        }

    }
}

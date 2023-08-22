using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedAdventureGame
{
    public class InventoryItem : GameObject, IPortable
    {
        public int Size { get; set; }

        public InventoryItem(string desc):base(desc)
        {
            
        }
    }
}

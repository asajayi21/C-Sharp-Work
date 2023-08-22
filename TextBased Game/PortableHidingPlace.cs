using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedAdventureGame
{
    public class PortableHidingPlace : GameObject, IHidingPlace, IPortable
    {
        private GameObject item;
        public int Size { get; set; }
        public GameObject HiddenObject { get; set; }

        public GameObject Search()
        {
            return new GameObject("");
        }
    }
}

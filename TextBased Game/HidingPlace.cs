using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedAdventureGame
{
    public class HidingPlace : GameObject, IHidingPlace
    {
        private GameObject hiddenObject;
        public GameObject HiddenObject 
        { 
            set
            {
                hiddenObject = value;
            }

            get
            {
                return hiddenObject;
            }
        
        }

        public GameObject Search()
        {
            var tempObject = hiddenObject;
            hiddenObject = null;
            return tempObject;
        }

        public HidingPlace(string description): base(description)
        {
            
        }
    }
}

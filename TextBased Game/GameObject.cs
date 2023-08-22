using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedAdventureGame
{
    public class GameObject
    {
        public string Description { get; set; }

        public GameObject()
        {
            Description = "";
        }
        
        public GameObject(string description)
        {
            Description = description;
        }

        public override string ToString()
        {
            return Description;
        }
    }
}

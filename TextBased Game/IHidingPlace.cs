using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedAdventureGame
{
    public interface IHidingPlace
    {
        GameObject HiddenObject { get; set; }

        GameObject Search();
    }
}

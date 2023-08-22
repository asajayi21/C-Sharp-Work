// TravelOption
// Programer: Rob Garner (rgarner7@cnm.edu)
// Date: 25 May 2016
// Represents a travel option.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace TextBasedAdventureGame
{
    /// <summary>
    /// Class that represents the game. 
    /// Has a map and location of player.
    /// </summary>
    class Map
    {
        /// <summary>
        /// List of map locations.
        /// </summary>
        public List<MapLocation> Locations { get; set; }

        /// <summary>
        /// Player's location.
        /// </summary>
        public MapLocation PlayerLocation { get; set; }

        /// <summary>
        /// Constructor that creates the game map.
        /// </summary>
        public Map()
        {
            //Create map locations first
            Locations = new List<MapLocation>();           
            Locations.Add(new MapLocation("You are on a road leading to a town."));
            Locations.Add(new MapLocation("You are on a road in front of a saloon."));
            Locations.Add(new MapLocation("You are in a saloon."));
            Locations.Add(new MapLocation("You are on a road in front of a jail."));
            Locations.Add(new MapLocation("You are in a jail."));
            Locations.Add(new MapLocation("You are on a road in front of a general store."));
            Locations.Add(new MapLocation("You are in a general store."));

            //Now add travel options to each map location

            //Road outside town
            Locations[0].TravelOptions.Add(new TravelOption("A town is to the west of you.",Locations[1]));
            Locations[0].Item.Add(new InventoryItem("Broken Rifle"));

            //Road in front of salloon
            Locations[1].TravelOptions.Add(new TravelOption("The road out of town is to the east of you.", Locations[0]));            
            Locations[1].TravelOptions.Add(new TravelOption("A saloon is to the north of you.", Locations[2]));            
            Locations[1].TravelOptions.Add(new TravelOption("A road leading further into town is to the west of you.", Locations[3]));

            //Saloon
            Locations[2].TravelOptions.Add(new TravelOption("The saloon door leads out to the street.", Locations[1]));
            Locations[2].Item.Add(new InventoryItem("Backpack"));

            //Road in front of jail
            Locations[3].TravelOptions.Add(new TravelOption("A road is to the east of you.", Locations[1]));
            Locations[3].TravelOptions.Add(new TravelOption("A jail is to the north of you.", Locations[4]));
            Locations[3].TravelOptions.Add(new TravelOption("A road leading further into town is to the west of you.", Locations[5]));

            //Jail
            Locations[4].TravelOptions.Add(new TravelOption("The jail door leads out to the street.", Locations[3]));
            Locations[4].Item.Add(new InventoryItem("Pocket lint"));

            //Road in front of general store
            Locations[5].TravelOptions.Add(new TravelOption("A road is to the east of you.", Locations[3]));
            Locations[5].TravelOptions.Add(new TravelOption("A general store is to the north of you.", Locations[6]));

            //Jail
            Locations[6].TravelOptions.Add(new TravelOption("The store door leads out to the street.", Locations[5]));

            //Set the player's starting location.
            PlayerLocation = Locations[0];

            
            HidingPlace rock = new HidingPlace("Large Rock");
            rock.HiddenObject = new InventoryItem("Snow Globe");
            Locations[1].Item.Add(rock);

            
        }

    }
}

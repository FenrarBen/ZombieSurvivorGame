using System;
using System.Xml.Serialization;

namespace ZombieSurvivor
{
    public class Room
    {
        [XmlElement("X")]
        public int X { get; set; }
        [XmlElement("Y")]
        public int Y { get; set; }
        [XmlElement("NAME")]
        public string Name { get; set; }
        [XmlElement("DESCRIPTION")]
        public string Description { get; set; }
        [XmlElement("EXITNORTH")]
        public bool ExitNorth { get; set; }
        [XmlElement("EXITEAST")]
        public bool ExitEast { get; set; }
        [XmlElement("EXITSOUTH")]
        public bool ExitSouth { get; set; }
        [XmlElement("EXITWEST")]
        public bool ExitWest { get; set; }
        public bool HasZombie { get; set; }
        public bool HasAmmo { get; set; }
        public bool HasHealth { get; set; }
        public bool HasGun { get; set; }
        public bool HasVictory = false;

        public Room()
        {

        }

        public static Room SetRoom(string desc, string name, bool north, bool east, bool south, bool west, bool zombie, bool ammo, bool health, bool gun)
        {
            Room theRoom = new Room();

            theRoom.Description = desc;
            theRoom.Name = name;
            theRoom.ExitNorth = north;
            theRoom.ExitEast = east;
            theRoom.ExitSouth = south;
            theRoom.ExitWest = west;
            theRoom.HasZombie = zombie;
            theRoom.HasAmmo = ammo;
            theRoom.HasHealth = health;
            theRoom.HasGun = gun;

            return theRoom;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ZombieSurvivor
{
    [XmlRoot("RoomCollection")]
    public class RoomCollection
    {
        [XmlArray("ROOMS")]
        [XmlArrayItem("ROOM")]
        public List<Room> rooms { get; set; }
    }
}

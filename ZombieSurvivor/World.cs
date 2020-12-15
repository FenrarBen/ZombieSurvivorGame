using System;
using System.IO;
using System.Xml.Serialization;

namespace ZombieSurvivor
{
    class World
    {
        public Room[,] Board = new Room[8,5];
        public Random rnd = new Random();

        public World()
        {
            // Deserialize our room info from the XML file into a List of rooms (see: RoomCollections.cs)
            RoomCollection rooms = null;
            using (FileStream reader = new FileStream("zombies.xml", FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(RoomCollection));
                rooms = (RoomCollection)serializer.Deserialize(reader);
                reader.Close();
            }

            // Go through Room list to set up each room onto the game board
            for (int i = 0; i < rooms.rooms.Count; i++)
            {
                // Each room has a 10% chance to have health
                int random = rnd.Next(1, 10);
                if (random > 9)
                {
                    rooms.rooms[i].HasHealth = true;
                }
                else
                {
                    rooms.rooms[i].HasHealth = false;
                }

                // Each room has a 50% chance to have a zombie
                random = rnd.Next(1, 10);
                if (random > 5)
                {
                    rooms.rooms[i].HasZombie = true;
                }
                else
                {
                    rooms.rooms[i].HasZombie = false;
                }

                // Each room has a 20% chance to have ammo
                random = rnd.Next(1, 10);
                if (random > 8)
                {
                    rooms.rooms[i].HasAmmo = true;
                }
                else
                {
                    rooms.rooms[i].HasAmmo = false;
                }

                // Each room has a 10% chance to have a gun
                random = rnd.Next(1, 10);
                if (random > 9)
                {
                    rooms.rooms[i].HasGun = true;
                }
                else
                {
                    rooms.rooms[i].HasGun = false;
                }

                // The armory has a special 50% chance to have a gun
                random = rnd.Next(1, 10);
                if (random > 5 && i == 5)
                {
                    rooms.rooms[i].HasGun = true;
                }

                // Set the current room details from the RoomCollection list
                Board[rooms.rooms[i].X, rooms.rooms[i].Y] = Room.SetRoom(rooms.rooms[i].Description, rooms.rooms[i].Name, 
                                                            rooms.rooms[i].ExitNorth, rooms.rooms[i].ExitEast, rooms.rooms[i].ExitSouth, rooms.rooms[i].ExitWest,
                                                            rooms.rooms[i].HasZombie, rooms.rooms[i].HasAmmo, rooms.rooms[i].HasHealth, rooms.rooms[i].HasGun);
            }

            // Select one of three random victory rooms where you can win the game
            int rando = rnd.Next(1, 3);
            if (rando == 1)
            {
                Board[2, 4].HasVictory = true;
            }
            else if (rando == 2)
            {
                Board[6, 0].HasVictory = true;
            }
            else if (rando == 3)
            {
                Board[7, 4].HasVictory = true;
            }
            
        }

        public void GetRoom(int x, int y)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(Board[x,y].Name);

            Console.ForegroundColor = ConsoleColor.White;
            //Console.WriteLine(Board[x,y].Description); // For future expansion, give the area more details
            //Console.WriteLine();
            ShowItems(x, y);
            Console.WriteLine();
            ShowExits(x, y);

        }

        public void ShowItems(int x, int y)
        {
            if (Board[x, y].HasZombie == true)
            {
                Console.WriteLine("There is a zombie here");
            }

            if (Board[x,y].HasAmmo == true)
            {
                Console.WriteLine("There is some ammo here");
            }

            if (Board[x, y].HasHealth == true)
            {
                Console.WriteLine("There is some health here");
            }

            if (Board[x, y].HasGun == true)
            {
                Console.WriteLine("There is a gun here");
            }

            if (x == 2 && y == 4)
            {
                Console.WriteLine("There is a tunnel here");
            }

            if (x == 6 && y == 0)
            {
                Console.WriteLine("There is a boat on the docks");
            }

            if (x == 7 && y == 4)
            {
                Console.WriteLine("There is a helicopter on the pad");
            }
        }

        public void ShowExits(int x, int y)
        {
            string exits = "Obvious Exits: ";
            if (Board[x,y].ExitNorth == true)
            {
                exits += "North ";
            }

            if (Board[x, y].ExitSouth == true)
            {
                exits += "South ";
            }

            if (Board[x, y].ExitEast == true)
            {
                exits += "East ";
            }

            if (Board[x, y].ExitWest == true)
            {
                exits += "West ";
            }
            exits = exits.Remove(exits.Length - 1, 1);
            Console.WriteLine(exits);
        }
    }
}

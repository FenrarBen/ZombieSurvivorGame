using System;

namespace ZombieSurvivor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("The world is over.  The final surprise of 2020: zombie apocalypse.\n" +
                              "You decide the safest thing to do is to get out of the city.\n" +
                              "But the dead surround the city.  You must find a possible escape route.\n" +
                              "To do so, type in commands to move, attack zombies, and collect items.\n\n" +
                              "type Help to see a list of all commands.");

            World world = new World();
            Player player = new Player(world);
            world.GetRoom(player.CurrentX, player.CurrentY);

            // Main game loop
            bool exit = false;
            while (!exit)
            {
                // First, check player HP.  If <= 0, player is dead.  Game Over!
                if (player.Health < 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You died!  Game over man!  Game over!");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }

                // Check for victory condition
                if (player.Victory == true)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("What would you like to do?");
                Console.ForegroundColor = ConsoleColor.Blue;

                string command = Console.ReadLine().ToUpper();
                command = CommandShortcuts.GetCommand(command);

                switch (command)
                {
                    case "MOVE NORTH":
                        player.MoveNorth(world);
                        break;
                    case "MOVE EAST":
                        player.MoveEast(world);
                        break;
                    case "MOVE SOUTH":
                        player.MoveSouth(world);
                        break;
                    case "MOVE WEST":
                        player.MoveWest(world);
                        break;
                    case "ATTACK ZOMBIE":
                        player.AttackZombie(world);
                        break;
                    case "SHOOT ZOMBIE":
                        player.ShootZombie(world);
                        break;
                    case "TAKE AMMO":
                        player.TakeAmmo(world);
                        break;
                    case "TAKE HEALTH":
                        player.TakeHealth(world);
                        break;
                    case "TAKE GUN":
                        player.TakeGun(world);
                        break;
                    case "EXIT":
                        exit = true;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You give up.  Zombies attack.  It isn't pretty.");
                        break;
                    case "SHOW STATS":
                        player.ShowStatus();
                        break;
                    case "LOOK":
                        world.GetRoom(player.CurrentX, player.CurrentY);
                        break;
                    case "HELP":
                        player.Help();
                        break;
                    case "BOAT":
                        player.Boat();
                        break;
                    case "HELICOPTER":
                        player.Helicopter();
                        break;
                    case "TUNNEL":
                        player.Tunnel();
                        break;
                    default:
                        Console.WriteLine("That is not a valid command.");
                        break;
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

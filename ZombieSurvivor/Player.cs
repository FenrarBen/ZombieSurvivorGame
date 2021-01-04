using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ZombieSurvivor
{
    class Player
    {
        private int MaxHealth { get; }
        public int Health { get; set; }
        private int Ammo { get; set; }
        private bool HasGun { get; set; }
        public int CurrentX { get; set; }
        public int CurrentY { get; set; }
        private Room CurrentRoom { get; set; }
        public bool Victory = false;
        public static System.Timers.Timer timer;

        public Player(World world)
        {
            MaxHealth = 100;
            Health = 75;
            Ammo = 0;
            HasGun = false;
            CurrentX = 0;
            CurrentY = 0;
            CurrentRoom = world.Board[0, 0];
        }

        public void MoveNorth(World world)
        {
            Console.ForegroundColor = ConsoleColor.White;
            if (CurrentRoom.ExitNorth == true && CurrentRoom.HasZombie == false)
            {

                Console.WriteLine("You go North into... ");
                Thread.Sleep(1500);
                Console.Clear();
                CurrentY--;

                ShowStatus();
                world.GetRoom(CurrentX, CurrentY);
                CurrentRoom = world.Board[CurrentX, CurrentY];
            }
            else if (CurrentRoom.ExitNorth == false)
            {
                Console.WriteLine("You can't go that way.");
            }
            else if (CurrentRoom.ExitNorth == true && CurrentRoom.HasZombie == true)
            {
                Console.WriteLine("The zombie blocks your path.  Deal with it first.");
            }
        }

        public void MoveSouth(World world)
        {
            Console.ForegroundColor = ConsoleColor.White;
            if (CurrentRoom.ExitSouth == true && CurrentRoom.HasZombie == false)
            {
                Console.WriteLine("You go South into... ");
                Thread.Sleep(1500);
                Console.Clear();
                CurrentY++;

                ShowStatus();
                world.GetRoom(CurrentX, CurrentY);
                CurrentRoom = world.Board[CurrentX, CurrentY];
            }
            else if (CurrentRoom.ExitSouth == false)
            {
                Console.WriteLine("You can't go that way.");
            }
            else if (CurrentRoom.ExitSouth == true && CurrentRoom.HasZombie == true)
            {
                Console.WriteLine("The zombie blocks your path.  Deal with it first.");
            }
        }

        public void MoveEast(World world)
        {
            Console.ForegroundColor = ConsoleColor.White;
            if (CurrentRoom.ExitEast == true && CurrentRoom.HasZombie == false)
            {
                Console.WriteLine("You go East into... ");
                Thread.Sleep(1500);
                Console.Clear();
                CurrentX++;

                ShowStatus();
                world.GetRoom(CurrentX, CurrentY);
                CurrentRoom = world.Board[CurrentX, CurrentY];
            }
            else if (CurrentRoom.ExitEast == false)
            {
                Console.WriteLine("You can't go that way.");
            }
            else if (CurrentRoom.ExitEast == true && CurrentRoom.HasZombie == true)
            {
                Console.WriteLine("The zombie blocks your path.  Deal with it first.");
            }
        }

        public void MoveWest(World world)
        {
            Console.ForegroundColor = ConsoleColor.White;
            if (CurrentRoom.ExitWest == true && CurrentRoom.HasZombie == false)
            {
                Console.WriteLine("You go West into... ");
                Thread.Sleep(1500);
                Console.Clear();
                CurrentX--;

                ShowStatus();
                world.GetRoom(CurrentX, CurrentY);
                CurrentRoom = world.Board[CurrentX, CurrentY];
            }
            else if (CurrentRoom.ExitWest == false)
            {
                Console.WriteLine("You can't go that way.");
            }
            else if (CurrentRoom.ExitWest == true && CurrentRoom.HasZombie == true)
            {
                Console.WriteLine("The zombie blocks your path.  Deal with it first.");
            }
        }

        public void AttackZombie(World world)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            if (CurrentRoom.HasZombie == true)
            {
                Console.WriteLine("You attempt to kill the zombie with your knife...");

                int score = world.rnd.Next(10);
                if (score >= 4)
                {
                    world.Board[CurrentX, CurrentY].HasZombie = false;
                    CurrentRoom.HasZombie = false;
                    Console.WriteLine("You smash your knife into the zombie's head, obliterating it's brain.");
                    Console.WriteLine("The zombie falls down, once more dead.");
                }

                if (score < 4)
                {
                    Console.WriteLine("You miss the zombie with your knife!");
                    int damage = world.rnd.Next(5, 15);

                    if (damage >= 10)
                    {
                        Console.WriteLine($"The zombie attacks!  It lands a DEVASTATING blow.  You lose {damage} Health");
                    }
                    else
                    {
                        Console.WriteLine($"The zombie attacks!  You lose {damage} HP!");
                    }

                    Health = Health - damage;
                }
            }

            else
            {
                Console.WriteLine("There is no zombie here.");
            }
        }

        public void ShootZombie(World world)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;

            if (CurrentRoom.HasZombie == false)
            {
                Console.WriteLine("There is no zombie here.");
            }
            else if (HasGun == false)
            {
                Console.WriteLine("You don't have a gun!");
            }
            else if (Ammo == 0)
            {
                Console.WriteLine("You have no ammo!");
            }
            else if (CurrentRoom.HasZombie == true && Ammo > 0 && HasGun == true)
            {
                Console.WriteLine("You take aim with your pistol...");
                Console.WriteLine("SPLAT!  You take the zombie out.  Good shootin'.");
                Ammo--;
                CurrentRoom.HasZombie = false;
                world.Board[CurrentX, CurrentY].HasZombie = false;
            }
        }

        public void TakeAmmo(World world)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            if (CurrentRoom.HasAmmo == true)
            {
                int newAmmo = world.rnd.Next(1, 3);
                Console.WriteLine($"You pick up the ammo and gain {newAmmo} bullets.  Nice!");
                Ammo = Ammo + newAmmo;
                CurrentRoom.HasAmmo = false;
                world.Board[CurrentX, CurrentY].HasAmmo = false;
            }

            else
            {
                Console.WriteLine("There isn't any ammo here.");
            }
        }

        public void TakeHealth(World world)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            if (CurrentRoom.HasHealth == true && Health < MaxHealth)
            {
                int newHealth = world.rnd.Next(5, 20);
                if (Health + newHealth > MaxHealth)
                {
                    newHealth = MaxHealth - Health;
                }
                Console.WriteLine($"You pick up the food and eat it, restoring {newHealth} Health.");
                Health = Health + newHealth;
                if (Health > MaxHealth)
                {
                    Health = MaxHealth;
                }

                CurrentRoom.HasHealth = false;
                world.Board[CurrentX, CurrentY].HasHealth = false;
            }

            else if (CurrentRoom.HasHealth == true && Health == MaxHealth)
            {
                Console.WriteLine("You don't need any more Health right now.");
            }

            else if (CurrentRoom.HasHealth == false)
            {
                Console.WriteLine("There isn't any food around here.");
            }
        }

        public void TakeGun(World world)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            if (CurrentRoom.HasGun == true && HasGun == false)
            {
                Console.WriteLine("You pick up the gun and feel safer already.  But watch your ammo supply!");
                CurrentRoom.HasGun = false;
                world.Board[CurrentX, CurrentY].HasGun = false;
                HasGun = true;
            }

            else if (CurrentRoom.HasGun == true && HasGun == true)
            {
                Console.WriteLine("You already have a gun.  Leave this one for another survivor.");
            }

            else if (CurrentRoom.HasGun == false)
            {
                Console.WriteLine("There isn't a gun around here.");
            }
        }

        public void ShowStatus()
        {
            string gun;
            if (HasGun == true)
            {
                gun = "Pistol";
            }

            else
            {
                gun = "None";
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Health: {Health}/{MaxHealth}  Ammo: {Ammo}  Gun: {gun}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Help()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("The object of the game is to escape the city filled with zombies.\n" +
                              "To do so, you must utilize the following commands: \n\n" +
                              "move north: moves your character north if possible at your current location.\n" +
                              "shortcuts: north, go north, n\n\n" +
                              "move south: moves your character south if possible at your current location.\n" +
                              "shortcuts: south, go south, s\n\n" +
                              "move east: moves your character east if possible at your current location.\n" +
                              "shortcuts: east, go east, e\n\n" +
                              "move west: moves your character west if possible at your current location.\n" +
                              "shortcuts: west, go west, w\n\n" +
                              "take gun: picks up a gun at your current location.\n\n" +
                              "take ammo: picks up the ammo at your current location.\n\n" +
                              "take health: picks up the food/health item at your current location.\n\n" +
                              "show stats: displays your current health, ammo, and if you have a gun\n" +
                              "shortcuts: stats, status, show status\n\n" +
                              "attack zombie: attack a zombie with your knife. 60% chance to succeed.\n" +
                              "shortcuts: attack, att\n\n" +
                              "shoot zombie: shoot the zombie. 100% chance to succeed. Requies that you have a gun and ammo.\n" +
                              "shortcuts: shoot\n\n" +
                              "look: show the curren location and items again.\n\n" +
                              "exit: quit the game\n\n" +
                              "boat: get on the boat\n\n" +
                              "tunnel: go through the tunnel\n\n" +
                              "helicopter: get in the helicopter\n\n" +
                              "help: display these commands again\n");
        }

        public void Boat()
        {
            if (CurrentX == 6 && CurrentY == 0 && CurrentRoom.HasVictory == true)
            {
                Victory = true;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("You get on the boat.  The keys are in ignition.  You close your eyes and hope this works.\n" +
                                  "You turn the key and the boat comes rumbling to life.\n" +
                                  "You take off into the sea.  You're safe!\n" +
                                  "At least for now.\n" +
                                  "Congratulations!  You win!");
            }
            else if (CurrentX == 6 && CurrentY == 0 && CurrentRoom.HasVictory == false)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("You get on the boat.  The keys are in ignition.  You close your eyes and hope this works.\n" +
                                  "You turn the key...\n" +
                                  "Nothing happens.  Something is wrong.\n" +
                                  "You'll have to find another way out of the city.\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("You don't see a boat here.");
            }
        }

        public void Tunnel()
        {
            if (CurrentX == 2 && CurrentY == 4 && CurrentRoom.HasVictory == true)
            {
                Victory = true;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("You hold your breath and start into the tunnel.  It's dark.\n" +
                                  "But at least it seems clear.\n" +
                                  "Each step you take sounds way too loud.\n" +
                                  "You reach the other side.  You've done it!  You're out!\n" +
                                  "You're safe.  At least for now.\n" +
                                  "Congratulations!  You win!");
            }
            else if (CurrentX == 2 && CurrentY == 4 && CurrentRoom.HasVictory == false)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("You hold your breath and start into the tunnel.  It's dark.\n" +
                                  "You go a few feet and run into rubble.\n" +
                                  "It looks like the tunnel has collapsed.\n" +
                                  "You'll have to find another way out of the city.\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("You don't see a tunnel here.");
            }
        }

        public void Helicopter()
        {
            if (CurrentX == 7 && CurrentY == 4 && CurrentRoom.HasVictory == true)
            {
                Victory = true;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("You get in the helicopter and smile to yourself.\n" +
                                  "Good thing you took that helicopter certification class.\n" +
                                  "You start the ignition and the helicopter comes to life.\n" +
                                  "This is going to work!\n" +
                                  "You slowly ascend into the air and head into the distance.\n" +
                                  "You're safe.  At least for now.\n" +
                                  "Congratulation!  You win!");
            }
            else if (CurrentX == 7 && CurrentY == 4 && CurrentRoom.HasVictory == false)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("You don't know how to fly a helicopter!\n" +
                                  "You'll have to find another way out of the city\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("You don't see a helicopter here.");
            }
        }
    }
}

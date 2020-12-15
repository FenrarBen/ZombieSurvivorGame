using System;
using System.Collections.Generic;
using System.Text;

namespace ZombieSurvivor
{
    static class CommandShortcuts
    {
        static public string GetCommand(string input)
        {
            string output = input;

            if (output == "GO NORTH" || output == "N" || output == "NORTH")
            {
                output = "MOVE NORTH";
            }

            if (output == "GO EAST" || output == "E" || output == "EAST")
            {
                output = "MOVE EAST";
            }

            if (output == "GO WEST" || output == "W" || output == "WEST")
            {
                output = "MOVE WEST";
            }

            if (output == "GO SOUTH" || output == "S" || output == "SOUTH")
            {
                output = "MOVE SOUTH";
            }

            if (output == "STATS" || output == "STATUS" || output == "SHOW STATUS")
            {
                output = "SHOW STATS";
            }

            if (output == "SHOOT")
            {
                output = "SHOOT ZOMBIE";
            }

            if (output == "ATTACK" || output == "ATT")
            {
                output = "ATTACK ZOMBIE";
            }

            return output;
        }
    }
}

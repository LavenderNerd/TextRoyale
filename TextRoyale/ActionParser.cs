using System;
using System.Collections.Generic;

namespace TextRoyale
{
    class ActionParser
    {
        public ActionParser()
        {
            //*crickets*
        }

        public bool Parse(string action, int currentPlayer, ref Entity[] players, List<ConsoleColor> playerColors) 
        {
            //Create list of individual action words
            List<string> actions =  new List<string>(action.Split(" "));

            if (actions.Count > 0)
            {
                switch (actions[0])
                {
                    case "help":
                        Console.WriteLine("Commands:\nattack <player>\nheal\nhelp\nlist");
                        break;
                    case "list":
                        for (int i = 0; i < players.Length; i += 1)
                        {
                            Console.ForegroundColor = playerColors[i];
                            players[i].PrintInfo();
                        }
                        Console.ForegroundColor = playerColors[currentPlayer];
                        break;
                    case "heal":
                        //Checks if player is below max HP
                        if (!players[currentPlayer].IsMaxHP())
                        {
                            players[currentPlayer].ReceiveDamage(-1);
                            Console.WriteLine("You healed for 1 HP!");
                            return true;
                        }
                        Console.WriteLine("You're already at max HP!");
                        break;
                    case "attack":
                        //Checks if there's an argument for attacking
                        if (actions.Count > 1)
                        {
                            string match = ListToString(actions, " ", 1);
                            //Creates a match string to check player names against
                            for(int i = 0; i < players.Length; i += 1)
                            {
                                //Match the command to a player
                                if(players[i].Name.ToLower() == match.ToLower())
                                {
                                    //Checks if player is alive
                                    if (players[i].IsAlive())
                                    {

                                        return true;
                                    }
                                    Console.WriteLine("That player isn't alive! Type 'list' for a list of players and their status.");
                                    return false;
                                }
                            }
                            Console.WriteLine("That player doesn't exist! Make sure to spell the player's name correcty, type 'list' for a list of players.");
                            return false;
                        }
                        Console.WriteLine("You need to specify someone to attack. Type 'help' for a list of actions and syntax.");
                        break;
                    default:
                        Console.WriteLine("Invalid action! Type 'help' for a list of actions and syntax.");
                        break;
                }
            }
            return false;
        }

        public string ListToString(List<string> list, string delimiter, int startPosition, int endPosition = -1)
        {
            string str = "";

            if (endPosition < 0) { endPosition = list.Count; }

            for (int i = startPosition; i < endPosition; i += 1)
            {
                str += list[i];
                if (i < list.Count - 1) { str += delimiter; }
            }
            return str;
        }
    }
}

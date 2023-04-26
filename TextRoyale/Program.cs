using System;
using System.Collections.Generic;

namespace TextRoyale
{

    class Program
    {
        static Entity[] players;
        static int playerCount;
        static ActionParser actionParser;

        static public Random rand = new Random();

        //Fill player color list w/ colors that look nice on black background
        static List<ConsoleColor> playerColors = new List<ConsoleColor>(new ConsoleColor[]{ 
            ConsoleColor.Green,
            ConsoleColor.Cyan,
            ConsoleColor.Gray,
            ConsoleColor.Red,
            ConsoleColor.Yellow,
            ConsoleColor.DarkYellow
        });

        static void Main(string[] args)
        {
            //Initialize action parser
            actionParser = new ActionParser();

            //Set starting color to white, and randomize player colors list
            Console.ForegroundColor = ConsoleColor.White;
            playerColors = Randomize(playerColors);

            // Get player count
            bool correctInput = false;
            while (!correctInput)
            {
                //If incorrect data recieved, or int out of bounds, keep looping
                correctInput = true;
                try
                {
                    Console.Write("Enter number of players (2-"+playerColors.Count+"): ");
                    playerCount = Convert.ToInt32(Console.ReadLine());

                    if (playerCount < 2)
                    {
                        correctInput = false;
                        Console.Write("Needs to be at least 2! ");
                    }
                    else if(playerCount > playerColors.Count)
                    {
                        correctInput = false;
                        Console.Write("Too many, needs to be less than "+playerColors.Count+"! ");
                    }
                }
                catch (Exception e)
                {
                    correctInput = false;
                    Console.Write("Try again! ");
                }
            }
            //Initialize players array w/ size of player count
            players = new Entity[playerCount];

            //Load each index in players with a default player (Fun note: this also makes it so no one can choose the name 'default')
            for (int i = 0; i < playerCount; i += 1)
            {
                players[i] = new Entity("default", 10, 3, 5);
            }

            //Each layer chooses a name
            for (int i = 0; i < playerCount; i += 1)
            {
                Console.Write("\nPlayer "+Convert.ToString(i+1)+", enter your name: ");
                bool newName = false;
                string testName ="";

                //Keep looping while chosen name is invalid (already chosen or blank)
                while (!newName)
                {
                    testName = Console.ReadLine();
                    newName = true;
                    
                    //Tests new name against all other names
                    for (int j = 0; j < playerCount; j += 1)
                    {
                        if (players[j].Name.ToLower() == testName.ToLower()) {
                            Console.Write("That name already exists, try another: ");
                            newName = false; 
                        }
                    }
                    //Tests if new name is blank
                    if(testName == "") {
                        Console.Write("You entered nothing for a name, try again: ");
                        newName = false; 
                    }
                }
                players[i].Name = testName;
                Console.WriteLine("Player " + (i + 1) + "'s name is " + players[i].Name + "!");
            }

            //Player 1 recieves 10 damage for TESTING AND SCIENCE!
            players[0].ReceiveDamage(10);

            //Game loop, keeps going as long as 2 players are alive
            while (AlivePlayers() >= 2)
            {
                //Loops through all players
                for (int i = 0; i < playerCount; i += 1)
                {
                    //Player action if they are alive
                    if (players[i].IsAlive())
                    {
                        //Changes color to player color
                        Console.ForegroundColor = playerColors[i];
                        Console.WriteLine("\nPlayer " + Convert.ToString(i + 1) + "'s Turn: ");
                        players[i].PrintInfo();

                        //Repeat if action failed
                        bool actionSuccess = false;
                        while (!actionSuccess)
                        {
                            Console.Write("-------------------------\nEnter your action: ");

                            //Runs action in action parser, see 'ActionParser.cs'
                            actionSuccess = actionParser.Parse(Console.ReadLine(), i, ref players, playerColors);
                        }
                    }
                }
            }

            //Yup ;-;
            Console.WriteLine("Everyone died! Sorry bud!");
        }

        //Function to check how many players are alive
        static public int AlivePlayers()
        {
            int alivePlayers = 0;

            for (int i = 0; i < playerCount; i += 1)
            {
                if (players[i].IsAlive()) { alivePlayers++; }
            }

            return alivePlayers;
        }

        //Function to randomize player list
        static public List<ConsoleColor> Randomize(List<ConsoleColor> list)
        {
            List<ConsoleColor> oldList = new List<ConsoleColor>(list);
            List<ConsoleColor> newList = new List<ConsoleColor>();

            while(oldList.Count > 0)
            {
                int randNum = rand.Next(oldList.Count);
                newList.Add(oldList[randNum]);
                oldList.RemoveAt(randNum);
            }

            return newList;
        }
    }
}

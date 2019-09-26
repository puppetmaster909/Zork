using System;
using System.Collections.Generic;

namespace Zork
{
    class Program
    {
        private static readonly string[,] Rooms = {
            { "Rocky Trail", "South of House", "Canyon View" },
            { "Forest", "West of House", "Behind House" },
            { "Dense Wooods", "North of House", "Clearing" }
        };

        //private static int LocationColumn = 1;
        //private static int LocationRow = 2;

        private static (int Row, int Column) Location = (1,1);
        private static List<Commands> Directions = new List<Commands>()
        {
            Commands.NORTH,
            Commands.SOUTH,
            Commands.EAST,
            Commands.WEST
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            Commands command = Commands.UNKNOWN;

            while (command != Commands.QUIT)
            {
                Console.WriteLine(Rooms[Location.Row, Location.Column]);
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                string outputString;
                switch(command)
                {
                    case Commands.QUIT:
                        outputString = "Thank you for playing!";
                        break;

                    case Commands.LOOK:
                        outputString = "This is an open field west of a white house, with a boarded front door. \nA rubber mat saying 'Welcome to Zork!' lies by the door.";
                        break;

                   // Expanded cases
                   {/*case Commands.NORTH:
                        outputString = "You moved NORTH";
                        break;

                    case Commands.SOUTH:
                        outputString = "You moved SOUTH";
                        break;

                    case Commands.EAST:
                        outputString = "You moved EAST";
                        break;

                    case Commands.WEST:
                        outputString = "You Moved WEST";
                        break;*/
                   }

                    // Fall-through cases
                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        bool movedSuccesfully = Move(command);
                        if (movedSuccesfully)
                        {
                            outputString = $"You moved {command}.";
                        }
                        else
                        {
                            outputString = "The way is shut!";
                        }
                        break;

                    default:
                        outputString = "Unknown command";
                        break;
                }
                Console.WriteLine(outputString);

                //Move(command);
            }
        }

        // Parses a command from input string
        private static Commands ToCommand(string commandString)
        {
            return Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;
        }

        // Expression-bodied method version
        //private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;

        // Handles moving between rooms
        private static bool Move(Commands command)
        {
            bool result;

            if (!Directions.Contains(command))
            {
                throw new ArgumentException();
            }

            switch (command)
            {
                case Commands.SOUTH when Location.Row > 0:
                    Location.Row--;
                    result = true;
                    break;
                case Commands.NORTH when Location.Row < Rooms.GetLength(Location.Column) - 1:
                    Location.Row++;
                    result = true;
                    break;
                case Commands.EAST when Location.Column < Rooms.GetLength(Location.Row) - 1:
                    Location.Column++;
                    result = true;
                    break;
                case Commands.WEST when Location.Column > 0:
                    Location.Column--;
                    result = true;
                    break;
                default:
                    result = false;
                    //throw new ArgumentException();
                    break;
            }

            return result;
        }

        private static void SpawnPlayer(string roomName)
        {
            (Location.Row, Location.Column) = IndexOf(Rooms, roomName);
            if ((Location.Row, Location.Column) == (-1, -1));
            {
                throw new Exception($"Did not find room: {roomName}");
            }
        }

        private static (int Row, int Column) IndexOf(string[,] values, string valueToFind)
        {

            for (int row = 0; row < values.GetLength(0); row++)
            {
                for (int column = 0; column < values.GetLength(1); column++)
                {
                    if (valueToFind == values[row, column])
                    {
                        return (row, column);
                    }
                }
            }

            return (-1, -1);

        }

    }
}
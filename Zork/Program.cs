using System;
using System.Collections.Generic;

namespace Zork
{
    class Program
    {
        private static readonly string[] _Rooms = { "Forest", "West of House", "Behind House", "Clearing", "Canyon View" };
        private static int _currentRoom = 1;
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
                Console.WriteLine(_Rooms[_currentRoom]);
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

                Move(command);
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
                case Commands.NORTH:
                case Commands.SOUTH:
                    result = false;
                    break;
                case Commands.EAST when _currentRoom < _Rooms.Length - 1:
                    _currentRoom++;
                    result = true;
                    break;
                case Commands.WEST when _currentRoom > 0:
                    _currentRoom--;
                    result = true;
                    break;
                default:
                    throw new ArgumentException();
            }

            return result;
        }
    }
}
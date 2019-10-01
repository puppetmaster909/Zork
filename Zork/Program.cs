using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Zork
{
    class Program
    {
        private static Room[,] Rooms = {
            { new Room("Rocky Trail"), new Room("South of House"), new Room("Canyon View") },
            { new Room("Forest"), new Room("West of House"), new Room("Behind House") },
            { new Room("Dense Woods"), new Room("North of House"), new Room("Clearing") }
        };

        private static void InitializeRooms(string roomsFileName)
        {
            Rooms = JsonConvert.DeserializeObject<Room[,]>(File.ReadAllText(roomsFileName));
        }

        private static (int Row, int Column) Location = (1, 1);

        public static Room CurrentRoom
        {
            get
            {
                return Rooms[Location.Row, Location.Column];
            }
        }

        private static List<Commands> Directions = new List<Commands>()
        {
            Commands.NORTH,
            Commands.SOUTH,
            Commands.EAST,
            Commands.WEST
        };

        private enum Fields
        {
            Name = 0,
            Description
        }

        private enum CommandLineArguments
        {
            RoomsFileName = 0
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            string defaultRoomsFileName = "Rooms.json";
            string roomsFileName = (args.Length > 0 ? args[(int)CommandLineArguments.RoomsFileName] : defaultRoomsFileName);
            InitializeRooms(roomsFileName);

            Location = IndexOf(Rooms, "West of House");
            Assert.IsTrue(Location != (-1, -1));

            Room previousRoom = null;
            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                Console.WriteLine(CurrentRoom);

                if (previousRoom != CurrentRoom)
                {
                    Console.WriteLine(CurrentRoom.Description);
                    previousRoom = CurrentRoom;
                }

                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                switch (command)
                {
                    case Commands.QUIT:
                        Console.WriteLine("Thank you for playing!");
                        break;

                    case Commands.LOOK:
                        Console.WriteLine(CurrentRoom.Description);
                        break;

                    // Fall-through cases
                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        bool movedSuccesfully = Move(command);
                        if (movedSuccesfully)
                        {
                            //Console.WriteLine($"You moved {command}.");
                        }
                        else
                        {
                            Console.WriteLine("The way is shut!");
                        }
                        break;

                    default:
                        Console.WriteLine("Unknown command");
                        break;
                }
            }
        }

        // Parses a command from input string
        private static Commands ToCommand(string commandString)
        {
            return Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;
        }

        // Handles moving between rooms
        private static bool Move(Commands command)
        {
            Assert.IsTrue(IsDirection(command), "Invalid direction.");

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
                case Commands.NORTH when Location.Row < Rooms.GetLength(0) - 1:
                    Location.Row++;
                    result = true;
                    break;
                case Commands.EAST when Location.Column < Rooms.GetLength(1) - 1:
                    Location.Column++;
                    result = true;
                    break;
                case Commands.WEST when Location.Column > 0:
                    Location.Column--;
                    result = true;
                    break;
                default:
                    result = false;
                    break;
            }

            return result;
        }

        private static (int Row, int Column) IndexOf(Room[,] values, string valueToFind)
        {
            for (int row = 0; row < values.GetLength(0); row++)
            {
                for (int column = 0; column < values.GetLength(1); column++)
                {
                    if (valueToFind == values[row, column].Name)
                    {
                        return (row, column);
                    }
                }
            }

            return (-1, -1);

        }

        private static bool IsDirection(Commands command) => Directions.Contains(command);

    }
}
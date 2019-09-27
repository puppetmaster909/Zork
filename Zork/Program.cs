using System;
using System.Collections.Generic;

namespace Zork
{
    class Program
    {
        private static readonly Room[,] Rooms = {
            { new Room("Rocky Trail"), new Room("South of House"), new Room("Canyon View") },
            { new Room("Forest"), new Room("West of House"), new Room("Behind House") },
            { new Room("Dense Woods"), new Room("North of House"), new Room("Clearing") }
        };

        private static readonly Dictionary<string, Room> RoomMap;

        static Program()
        {
            RoomMap = new Dictionary<string, Room>();
            foreach (Room room in Rooms)
            {
                RoomMap.Add(room.Name, room);
            }
        }

        private static void InitializeRoomDescriptions()
        {
            RoomMap["Rocky Trail"].Description = "You are on a rock-strewn trail.";                                                                                 // Rocky Trail
            RoomMap["South of House"].Description = "You are facing the south side of a whilte house. There is no door here, and all the windows are barred.";      // South of House
            RoomMap["Canyon View"].Description = "You are at the top of the Great Canyon on its south wall.";                                                       // Canyon View
            RoomMap["Forest"].Description = "This is a forest, with trees in all directions around you.";                                                           // Forest
            RoomMap["West of House"].Description = "This is an open field west of a white house, with a boarded front door.";                                       // West of House
            RoomMap["Behind House"].Description = "You are behind the white house. In one corner of the house there is a small window which is slightly ajar.";     // Behind House
            RoomMap["Dense Woods"].Description = "This is a dimly lit forest, with large trees all around. To the east, there appears to be sunlight.";             // Dense Woods
            RoomMap["North of House"].Description = "You are facing the north side of a white house. There is no door here, and all the windows are barred.";       // North of House
            RoomMap["Clearing"].Description = "You are in a clearing, with a forest surrounding you on the west and south.";                                        // Clearing
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

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");
            InitializeRoomDescriptions();

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
                //Console.WriteLine(outputString);

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

        /*private static void SpawnPlayer(string roomName)
        {
            (Location.Row, Location.Column) = IndexOf(Rooms, roomName);
            if ((Location.Row, Location.Column) == (-1, -1));
            {
                throw new Exception($"Did not find room: {roomName}");
            }
        }*/

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
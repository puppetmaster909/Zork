using System;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            string inputString = Console.ReadLine();
            inputString = inputString.ToUpper();

            switch (inputString)
            {
                case "QUIT":
                    Console.WriteLine("Thank you for playing.");
                    break;

                case "LOOK":
                    Console.WriteLine("This is an open field west of a white house, with a boarded front door.\nA rubber mat saying 'Welcome to Zork!'  lies by the door");
                    break;

                default:
                    Console.WriteLine("Unrecognized command");
                    break;
            }
        }
    }
}

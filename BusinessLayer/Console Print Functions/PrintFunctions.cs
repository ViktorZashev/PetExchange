using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public static class PrintFunctions
    {
        public static void PrintInitialMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("Choose a command:");
            Console.WriteLine("1 ---> Delete all entries in database");
            Console.WriteLine("2 ---> Seed database with example values");
            Console.WriteLine("3 ---> Sign up");
            Console.WriteLine("4 ---> Log in");
            Console.WriteLine("0 ---> Exit program");
            Console.WriteLine("");
            Console.Write("Enter a command: ");
        }
        public static void PrintLoginMenu()
        {
            Console.WriteLine("Enter: [username] [password]");
        }
        public static void PrintIncorrectUsernameMessage()
        {
            Console.WriteLine("No such username was found in database! Try again.");
            PrintLoginMenu();
        }
        public static void PrintIncorrectPasswordMessage()
        {
            Console.WriteLine("Password was incorrect! Try again.");
            Console.Write("Password: ");
        }
        public static void PrintSignupMenu()
        {
            Console.WriteLine("REGISTER:");
            Console.WriteLine("Enter: [username] [password]");
            Console.WriteLine("Enter: [Name]");
            Console.WriteLine("Enter: [Town Name]");
            Console.WriteLine("Enter: [Contact Information]");
        }
        public static void PrintTitle()
        {
            Console.WriteLine(ASCIIArt.Title);
        }
        public static void PrintInvalidCommandError()
        {
            Console.WriteLine("Invalid command! Try again.");
            Console.Write("Enter a command: ");
        }
        public static void PrintSuccessMessage()
        {
            Console.WriteLine("Operation completed successfully!");
        }
    }
}

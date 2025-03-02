using BusinessLayer;
using BusinessLayer.Database_Functions;
using BusinessLayer.Functions;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ConsolePresentationLayer
{
    public class MainPageController
    {
        public MainPageController() 
        { 
            PrintFunctions.PrintTitle();
			PrintFunctions.PrintArt(ASCIIArt.Dog3);
			PrintFunctions.PrintInitialMenu();
            while (true)
            {
                int command;
                while (true)
                {
                    try
                    {
                        command = int.Parse(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        PrintFunctions.PrintInvalidCommandError();
                    }
                }
                if (command == 0) { break; }
                HandleCommand(command);
                PrintFunctions.PrintInitialMenu();
            }
        }

        private void HandleCommand(int command) 
        {
            switch (command)
            {
                case 1:
                    SignupUserFunction();
                    break;
                case 2:
                    var loggedUser = LoginUserFunction();
                    if (loggedUser == null) break;
                    var loggedUserController = new LoggedUserController(loggedUser);
                    break;
            }
        }
        private User LoginUserFunction()
        {
            PrintFunctions.PrintLoginMenu();
            var input = Console.ReadLine().Split();
            if (input[0] == "0") return null;
            string username = input[0];
            string password = input[1];
            int code;
            while (true)
            {
                code = DatabaseFunctions.CheckUserReturnsCode(username, password);
                if(code == 0) { 
                    PrintFunctions.PrintIncorrectUsernameMessage();
                    input = Console.ReadLine().Split();
                    if (input[0] == "0") break;
                    username = input[0];
                    password = input[1];
                }
                if (code == 1)
                {
                    PrintFunctions.PrintIncorrectPasswordMessage();
                    password = Console.ReadLine();
                    if (password == "0") break;
                }
                if(code == 2)
                {
                    PrintFunctions.PrintSuccessMessage();
                    var loggedUser = UserService.ReturnUser(username, password);
                    return loggedUser;
                }
            }
            return null;
        }
        private void SignupUserFunction()
        {
            PrintFunctions.PrintSignupMenu();
            var usernameAndPassword = Console.ReadLine().Split();
            var name = Console.ReadLine();
            var townName = Console.ReadLine();
            Town town = TownService.RetrieveTown(townName);
            var contactInfo = Console.ReadLine();
            var EnteredaAdminPassword = Console.ReadLine();
            bool adminStatus = false;
            while (true)
            {
                if (EnteredaAdminPassword == "") break;
                if(EnteredaAdminPassword == DatabaseFunctions.adminPassword)
                {
                    adminStatus = true;
                    break;
                }
                else
                {
                    PrintFunctions.PrintWrongAdminPassMessage();
                    EnteredaAdminPassword = Console.ReadLine();
                }
            }
            var newUser = new User(town, new List<Pet>(), name, "photoPath", adminStatus, contactInfo, usernameAndPassword[0], usernameAndPassword[1]);
            UserService.Create(newUser);
            PrintFunctions.PrintSuccessMessage();
        }
    }
}

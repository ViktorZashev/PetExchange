using BusinessLayer;
using BusinessLayer.Database_Functions;
using BusinessLayer.Functions;
using BusinessLayer.Models;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ConsolePresentationLayer
{
    public class LoggedUserController
    {
        public static User LoggedUser;
        public LoggedUserController(User user) 
        {
            LoggedUser = user;
			PrintFunctions.PrintArt(ASCIIArt.Fish1);
			PrintFunctions.PrintLoggedUserMenu();
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
                PrintFunctions.PrintLoggedUserMenu();
            }
        }

        private void HandleCommand(int command) 
        {
            switch(command)
            {
                case 1: // Show Pets
                    PrintFunctions.PrintPets(PetService.ReturnAllPets(LoggedUser));
                    break;
                case 2: // Register Pet
                    RegisterPetComponent();
                    break;
                case 3: // Delete Pet
                    DeletePetComponent();
                    break;
                case 4: // Update Pet
                    UpdatePetComponent();
                    break;
                case 5: // View all public offers in town
                    ViewAvailablePublicOffers();
                    break;
                case 6: // Register pet as public offer
                    RegisterPetAsPublicOffer();
                    break;
                case 7: // Delete public offer
                    DeletePublicOffer();
                    break;
				case 8: // Show all your requests
					ShowAllRequests();
					break;
				case 9: // Create a request
					CreateRequest();
					break;
				case 10: // Delete a request
					DeleteRequest();
					break;
			}
        }

		private void DeleteRequest()
		{
			PrintFunctions.PrintDeleteRequestMesage();
			while (true)
			{
				try
				{
					var petName = Console.ReadLine();
                    if (petName == "0") break;
					UserRequestsService.DeleteRequest(LoggedUser, petName);
					PrintFunctions.PrintSuccessMessage();
					break;
				}
				catch
				{
					PrintFunctions.PrintPetNameNotFoundASRequestMessage();
				}
			}
		}

		private void CreateRequest()
		{
			PrintFunctions.PrintRequestPetMessage();
			while (true)
			{
				try
				{
					var petName = Console.ReadLine();
                    if (petName == "0") break;
                    UserRequestsService.CreateRequest(LoggedUser, petName);
					PrintFunctions.PrintSuccessMessage();
					break;
				}
				catch
				{
					PrintFunctions.PrintPetNameNotAsPublicOfferMessage();
				}
			}
		}

		private void ShowAllRequests()
		{
            var allRequests = UserRequestsService.ReadAll(LoggedUser);
            PrintFunctions.PrintRequests(allRequests);
		}

		private void DeletePublicOffer()
		{
			PrintFunctions.PrintDeletePetPublicOfferMessage();
			while (true)
			{
				try
				{
					var petName = Console.ReadLine();
                    if (petName == "0") break;
                    PublicOfferService.DeleteByPetName(petName, LoggedUser);
					PrintFunctions.PrintSuccessMessage();
					break;
				}
				catch
				{
					PrintFunctions.PrintPetNameNotFoundMessage();
				}
			}
		}

		private void RegisterPetAsPublicOffer()
		{
			PrintFunctions.PrintRegisterPetAsPublicOfferMessage();
			while (true)
			{
				try
				{
					var petName = Console.ReadLine();
                    if (petName == "0") break;
                    PublicOfferService.RegisterPet(petName, LoggedUser);
					PrintFunctions.PrintSuccessMessage();
					break;
				}
				catch
				{
					PrintFunctions.PrintPetNameAlreadyIsPublicOrDoesntExist();
				}
			}
		}

		private void ViewAvailablePublicOffers()
		{
            var town = TownService.Read(LoggedUser.TownId);
            var availableOffers = PublicOfferService.ReadAll().Where(x => x.TownId == town.Id).ToList();
            PrintFunctions.DisplayOffers(availableOffers, town);
		}

		private void RegisterPetComponent()
        {
            PrintFunctions.PrintPetRegistrationMenu();
            var name = Console.ReadLine();
            var animalType = Console.ReadLine();
            int age;
            while (true)
            {
                try
                {
                    age = int.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    PrintFunctions.PrintIncorrectAgeMessage();
                }
            }

            bool includesCage = false;
            char input;
            while (true)
            {
                try
                {
                    input = char.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    PrintFunctions.PrintIncorrectCharMessage();
                }
            }
            if (input == 'Y') includesCage = true;
            if (input == 'N') includesCage = false;
            var description = Console.ReadLine();
  
            var newpet = new Pet(LoggedUser,name,"photo path",age,animalType,description,includesCage);
            PetService.Create(newpet);
            PrintFunctions.PrintSuccessMessage();
        }
        private static void DeletePetComponent()
        {
            PrintFunctions.PrintDeletePetMessage();
            while (true)
            {
                try
                {
                    var petName = Console.ReadLine();
                    if (petName == "0") break;
                    PetService.Delete(petName, LoggedUser);
                    PrintFunctions.PrintSuccessMessage();
                    break;
                }
                catch
                {
                    PrintFunctions.PrintPetNameNotFoundMessage();
                }
            }
        }
        private static void UpdatePetComponent()
        {
            PrintFunctions.UpdatePetMessageMenu();
            var name = Console.ReadLine();
            var animalType = Console.ReadLine();
            int age;
            while (true)
            {
                try
                {
                    age = int.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    PrintFunctions.PrintIncorrectAgeMessage();
                }
            }

            bool includesCage = false;
            char input;
            while (true)
            {
                try
                {
                    input = char.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    PrintFunctions.PrintIncorrectCharMessage();
                }
            }
            if (input == 'Y') includesCage = true;
            if (input == 'N') includesCage = false;
            var description = Console.ReadLine();
            if (!PetService.CheckPetExists(name))
            {
                PrintFunctions.PrintPetNameNotFoundMessage();
                UpdatePetComponent();
                return;
            }
            var oldPet = PetService.ReturnPetByname(name);
            var updatedPet = new Pet(oldPet.Id,LoggedUser, name, "photo path", age, animalType, description, includesCage);
            PetService.Update(updatedPet);
            PrintFunctions.PrintSuccessMessage();
        }
    }
}

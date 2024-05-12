using BusinessLayer.Functions;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public static class PrintFunctions
    {
		#region Log in User Menu 
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
            Console.WriteLine("Enter 0 to to back!");
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
        public static void PrintNeededCountryDataMessage()
        {
            Console.WriteLine("Enter country name for town!");
            Console.Write("Country Name: ");
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
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(ASCIIArt.Title);
			Console.ForegroundColor = ConsoleColor.White;
		}
	
        public static void PrintInvalidCommandError()
        {
            Console.WriteLine("Invalid command! Try again.");
            Console.Write("Enter a command: ");
        }
        public static void PrintSuccessMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Operation completed successfully!");
			Console.ForegroundColor = ConsoleColor.White;
		}
		#endregion

		#region Logged User Functions
		public static void PrintLoggedUserMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("Choose a command:");
            Console.WriteLine("1 ---> Show pets");
            Console.WriteLine("2 ---> Register pet");
            Console.WriteLine("3 ---> Delete pet");
            Console.WriteLine("4 ---> Update pet");
            Console.WriteLine("5 ---> See all public offers in your area");
            Console.WriteLine("6 ---> Register pet as public offer");
            Console.WriteLine("7 ---> Delete public offer");
			Console.WriteLine("8 ---> Show all your requests");
			Console.WriteLine("9 ---> Create a request for a public offer pet");
            Console.WriteLine("10 ---> Delete a request for a public offer pet");
            Console.WriteLine("0 ---> Back to main menu");
            Console.WriteLine("");
            Console.Write("Enter a command: ");
        }

        public static void PrintPets(List<Pet> pets)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (pets == null ||  pets.Count == 0)
            {
                Console.WriteLine("You have no pets saved in database");
            }
            else
            {
                Console.WriteLine("");
                foreach(Pet p in pets)
                {
                    Console.WriteLine("Pet Name: " + p.Name);
                    Console.WriteLine("Animal Type: " + p.AnimalType);
                    Console.WriteLine("Age: " + p.Age);
                    Console.Write("Comes with cage: " );
                    if (p.IncludesCage) Console.WriteLine("Yes");
                    else Console.WriteLine("No");
                    Console.WriteLine("Description: " + p.Description);
					Console.WriteLine("");
				}
            }
            Console.ForegroundColor = ConsoleColor.White; 
		}
		public static void PrintPet(Pet pet)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			if (pet == null)
			{
                throw new Exception("Pet is null!");
			}
			else
			{
				Console.WriteLine("");
				Console.WriteLine("Pet Name: " + pet.Name);
				Console.WriteLine("Animal Type: " + pet.AnimalType);
				Console.WriteLine("Age: " + pet.Age);
				Console.Write("Comes with cage: ");
				if (pet.IncludesCage) Console.WriteLine("Yes");
				else Console.WriteLine("No");
				Console.WriteLine("Description: " + pet.Description);
				Console.WriteLine("");
				
			}
			Console.ForegroundColor = ConsoleColor.White;
		}

		public static void PrintPetRegistrationMenu()
        {
            Console.WriteLine("REGISTER PET:");
            Console.WriteLine("Enter: [Name]");
            Console.WriteLine("Enter: [Animal Type]");
            Console.WriteLine("Enter: [Age]");
            Console.WriteLine("Enter: [Comes with cage](Y Or N)");
            Console.WriteLine("Enter: [Description]");
        }

        public static void PrintIncorrectAgeMessage()
        {
            Console.WriteLine("Incorrect data format! Enter an integer.");
            Console.Write("Age: ");
        }
        public static void PrintIncorrectCharMessage()
        {
            Console.WriteLine("Incorrect data format! Enter Y or N.");
            Console.Write("Comes with cage: ");
        }
        public static void PrintDeletePetMessage()
        {
            Console.WriteLine("Enter pet name to be deleted: ");
            Console.WriteLine("Enter 0 to go back.");

        }
        public static void PrintDeletePetPublicOfferMessage()
        {
			Console.WriteLine("Enter to pet name to be deleted as Public Offer: ");
            Console.WriteLine("Enter 0 to go back.");
        }
		public static void PrintRegisterPetMessage()
		{
			Console.WriteLine("Enter pet name to be registered: ");
            Console.WriteLine("Enter 0 to go back.");
        }
		public static void PrintRegisterPetAsPublicOfferMessage()
		{
			Console.WriteLine("Enter pet name to be registered as Public Offer: ");
            Console.WriteLine("Enter 0 to go back.");
        }
		public static void PrintPetNameNotFoundMessage()
        {
            Console.WriteLine("No such pet registered in your profile! Try again.");
        }
		public static void PrintPetNameAlreadyIsPublicOrDoesntExist()
		{
			Console.WriteLine("No such pet registered in your profile OR This pet already is logged as Public Offer! Try again.");
		}
		public static void UpdatePetMessageMenu()
        {
            Console.WriteLine("UPDATE PET BY NAME:");
            Console.WriteLine("Enter: [Name]");
            Console.WriteLine("Enter: [Animal Type]");
            Console.WriteLine("Enter: [Age]");
            Console.WriteLine("Enter: [Comes with cage](Y Or N)");
            Console.WriteLine("Enter: [Description]");
        }
        public static void PetNameNotFoundMessage()
        {
            Console.WriteLine("No such pet name was found in database! Try Again.");
            Console.WriteLine();
        }

		public static void DisplayOffers(List<PublicOffer> offers, Town town)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Town: " + town.Name);
			if (offers == null || offers.Count == 0)
			{
				Console.WriteLine("There are no public offers in your town!");
			}
			else
			{
				Console.WriteLine("");
				foreach (var  offer in offers)
				{
                    var offerPet = offer.Pet;
                    Console.WriteLine("Pet information:");
                    PrintPet(offerPet);
                    Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.Green;
				}
			}
			Console.ForegroundColor = ConsoleColor.White;
		}

		public static void PrintDeleteRequestMesage()
		{
            Console.WriteLine("Enter pet name linked to the request you wish to delete!");
            Console.WriteLine("Enter 0 to go back.");
            Console.Write("Pet Name: ");
		}

		public static void PrintPetNameNotFoundASRequestMessage()
		{
            Console.WriteLine("No such pet name found in your existing requests. Try again!");
		}

		public static void PrintRequestPetMessage()
		{
            Console.WriteLine("Enter the pet's name you wish to request!");
            Console.WriteLine("Enter 0 to go back.");
            Console.Write("Pet Name: ");
		}

		public static void PrintPetNameNotAsPublicOfferMessage()
		{
			Console.WriteLine("Error. Try again!");
			Console.Write("Pet Name: ");
		}

        public static void PrintRequests(List<UserRequests> requests)
        {
			Console.ForegroundColor = ConsoleColor.Green;
			if (requests.Count == 0) Console.WriteLine("You have no requests registered in system!");
            else
            {
                foreach (var request in requests)
                {
                    Console.WriteLine();
                    var pet = PetService.Read(request.PublicOffer.PetId);
                    Console.WriteLine("Pet Name: " + pet.Name);
                    Console.Write("Accepted: ");
                    if (request.IsAccepted == true) Console.WriteLine("Yes");
                    else Console.WriteLine("No");
                    Console.WriteLine();
                }
            }
			Console.ForegroundColor = ConsoleColor.White;
		}

        #endregion

        public static void PrintArt(string art)
        {
            Console.WriteLine(art);
        }

        public static void PrintCantRequestOwnPetMessage()
        {
            Console.WriteLine("You can't create a request for your own pet! Try again.!");
        }
    }
}

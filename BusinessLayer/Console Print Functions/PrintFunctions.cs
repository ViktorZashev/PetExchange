using BusinessLayer.Models;
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
            Console.WriteLine("0 ---> Back to main menu");
            Console.WriteLine("");
            Console.Write("Enter a command: ");
        }

        public static void PrintPets(List<Pet> pets)
        {
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
        }
		public static void PrintPet(Pet pet)
		{
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
            Console.Write("Enter pet name to be deleted: ");
        }
        public static void PrintDeletePetPublicOfferMessage()
        {
			Console.Write("Enter to pet name to be deleted as Public Offer: ");
		}
		public static void PrintRegisterPetMessage()
		{
			Console.Write("Enter pet name to be registered: ");
		}
		public static void PrintRegisterPetAsPublicOfferMessage()
		{
			Console.Write("Enter pet name to be registered as Public Offer: ");
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
				}
			}
		}
		#endregion
	}
}

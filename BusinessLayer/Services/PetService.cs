using BusinessLayer.Models;
using DataLayer;
using DataLayer.ProjectDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Functions
{
	public static class PetService
	{
		private static readonly PetExchangeDbContext _ProjectContext = _ProjectContext = new PetExchangeDbContext();
		public static PetDbContext _PetContext = new PetDbContext(_ProjectContext);

		public static void Create(Pet pet)
		{
			// Validation
			_PetContext.Create(pet);
		}
		public static void Create(List<Pet> entities)
		{
			foreach (var pet in entities)
			{
				Create(pet);
			}
		}
		public static Pet Read(Guid idt, bool useNav = true)
		{

			return _PetContext.Read(idt, useNav);
		}

		public static List<Pet> ReadAll(bool useNav = true)
		{

			return _PetContext.ReadAll(useNav);
		}
		public static void Update(Pet pet)
		{
			// validation
			_PetContext.Update(pet);
		}
		public static void Delete(string name, User user)
		{
			// validation
			var foundPet = ReturnAllPets().Where(x => x.Name == name && x.User.Id == user.Id).FirstOrDefault();
			if (foundPet == null)
			{
				throw new Exception("No such pet exists!");
			}
			_PetContext.Delete(foundPet.Id);
		}
		public static void Delete(Guid id)
		{
			_PetContext.Delete(id);
		}
		public static void DeleteAll()
		{
			var Pets = ReadAll();
			foreach (var Pet in Pets)
			{
				Delete(Pet.Id);
			}
		}
		public static bool CheckPetExists(string name)
		{
			if (ReturnAllPets().Any(x => x.Name == name))
			{
				return true;
			}
			else return false;
		}
		public static List<Pet> ReturnAllPets(User user)
		{
			var allPets = _PetContext.ReadAll(true);
			if (allPets.Count == 0) return new List<Pet>();
			else
			{
				var usersPets = new List<Pet>();
				foreach (var pet in allPets)
				{
					if (pet.User == null) continue;
					if (pet.User.Id == user.Id)
					{
						usersPets.Add(pet);
					}
				}
				return usersPets;
			}
		}
		public static List<Pet> ReturnAllPets()
		{
			if (!_PetContext.ReadAll(true).Exists(x => true))
			{
				return new List<Pet>();
			}
			else
			{
				return _PetContext.ReadAll(true).ToList();

			}
		}
		public static Pet ReturnPetByname(string name)
		{
			var pets = _PetContext.ReadAll().ToList();
			return pets.Where(x => x.Name == name).FirstOrDefault();
		}
		public static void OutputPets()
		{
			var pets = ReadAll();
			foreach (Pet pet in pets)
			{
				Console.WriteLine();
				Console.WriteLine("Name: " + pet.Name);
				Console.WriteLine("User Id: " + pet.User.Id);
				Console.WriteLine("Age: " + pet.Age);
				Console.WriteLine("Animal Type: " + pet.AnimalType);
				Console.WriteLine("Includes Cage: " + pet.IncludesCage);
				Console.WriteLine("Description: " + pet.Description);
				Console.WriteLine();
			}
		}
	}
}

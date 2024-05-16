using BusinessLayer.Functions;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetExchangeTests.BusinessLayer
{
    public class TownServiceTests : BusinessLayerTestsManagement
    {
		[Test]
		public void Create_Adds_New_Town()
		{
			// Arrange
			var country = new Country { Id = Guid.NewGuid(), Name = "Test Country" };
			var town = new Town { Id = Guid.NewGuid(), Name = "Test Town", Country = country, CountryId = country.Id };
			db.Countries.Add(country);
			db.SaveChanges();

			// Act
			TownService.Create(town);

			// Assert
			var result = db.Towns.FirstOrDefault(t => t.Id == town.Id);
			Assert.IsNotNull(result, "The new town should be added.");
			Assert.That(result.Id, Is.EqualTo(town.Id), "The IDs should match.");
		}

		[Test]
		public void Create_Adds_List_Of_Towns()
		{
			// Arrange
			var country = new Country { Id = Guid.NewGuid(), Name = "Test Country" };
			var town1 = new Town { Id = Guid.NewGuid(), Name = "Test Town 1", Country = country, CountryId = country.Id };
			var town2 = new Town { Id = Guid.NewGuid(), Name = "Test Town 2", Country = country, CountryId = country.Id };
			db.Countries.Add(country);
			db.SaveChanges();
			var newTowns = new List<Town> { town1, town2 };

			// Act
			TownService.Create(newTowns);

			// Assert
			var result = db.Towns.Count();
			Assert.That(result, Is.EqualTo(2), "Both towns should be added.");
		}

		[Test]
		public void Read_Returns_Town_By_Id()
		{
			// Arrange
			var country = new Country { Id = Guid.NewGuid(), Name = "Test Country" };
			var town = new Town { Id = Guid.NewGuid(), Name = "Test Town", Country = country, CountryId = country.Id };
			db.Countries.Add(country);
			db.Towns.Add(town);
			db.SaveChanges();

			// Act
			var result = TownService.Read(town.Id);

			// Assert
			Assert.IsNotNull(result, "The town should be found.");
			Assert.That(result.Id, Is.EqualTo(town.Id), "The IDs should match.");
		}

		[Test]
		public void ReadAll_Returns_All_Towns()
		{
			// Arrange
			var country = new Country { Id = Guid.NewGuid(), Name = "Test Country" };
			var town1 = new Town { Id = Guid.NewGuid(), Name = "Test Town 1", Country = country, CountryId = country.Id };
			var town2 = new Town { Id = Guid.NewGuid(), Name = "Test Town 2", Country = country, CountryId = country.Id };
			db.Countries.Add(country);
			db.Towns.AddRange(town1, town2);
			db.SaveChanges();

			// Act
			var results = TownService.ReadAll();

			// Assert
			Assert.That(results.Count, Is.EqualTo(2), "Both towns should be returned.");
		}

		[Test]
		public void Update_Modifies_Existing_Town()
		{
			// Arrange
			var country = new Country { Id = Guid.NewGuid(), Name = "Test Country" };
			var town = new Town { Id = Guid.NewGuid(), Name = "Test Town", Country = country, CountryId = country.Id };
			db.Countries.Add(country);
			db.Towns.Add(town);
			db.SaveChanges();

			var updatedName = "Updated Test Town";
			town.Name = updatedName;

			// Act
			TownService.Update(town);

			// Assert
			var result = db.Towns.FirstOrDefault(t => t.Id == town.Id);
			Assert.IsNotNull(result, "The town should be found.");
			Assert.That(result.Name, Is.EqualTo(updatedName), "The name should be updated.");
		}

		[Test]
		public void Delete_Removes_Town()
		{
			// Arrange
			var country = new Country { Id = Guid.NewGuid(), Name = "Test Country" };
			var town = new Town { Id = Guid.NewGuid(), Name = "Test Town", Country = country, CountryId = country.Id };
			db.Countries.Add(country);
			db.Towns.Add(town);
			db.SaveChanges();

			// Act
			TownService.Delete(town.Id);

			// Assert
			var result = db.Towns.FirstOrDefault(t => t.Id == town.Id);
			Assert.IsNull(result, "The town should be deleted.");
		}

		[Test]
		public void DeleteAll_Removes_All_Towns()
		{
			// Arrange
			var country = new Country { Id = Guid.NewGuid(), Name = "Test Country" };
			var town1 = new Town { Id = Guid.NewGuid(), Name = "Test Town 1", Country = country, CountryId = country.Id };
			var town2 = new Town { Id = Guid.NewGuid(), Name = "Test Town 2", Country = country, CountryId = country.Id };
			db.Countries.Add(country);
			db.Towns.AddRange(town1, town2);
			db.SaveChanges();

			// Act
			TownService.DeleteAll();

			// Assert
			var result = db.Towns.Count();
			Assert.That(result, Is.EqualTo(0), "All towns should be deleted.");
		}

		[Test]
		public void CheckIfExists_Returns_True_If_Town_Exists()
		{
			// Arrange
			var country = new Country { Id = Guid.NewGuid(), Name = "Test Country" };
			var town = new Town { Id = Guid.NewGuid(), Name = "Test Town", Country = country, CountryId = country.Id };
			db.Countries.Add(country);
			db.Towns.Add(town);
			db.SaveChanges();

			// Act
			var result = TownService.CheckIfExists("Test Town");

			// Assert
			Assert.IsTrue(result, "The town should exist.");
		}

		[Test]
		public void CheckIfExists_Returns_False_If_Town_Does_Not_Exist()
		{
			// Arrange
			// Database is empty because of setup method

			// Act
			var result = TownService.CheckIfExists("Non-Existent Town");

			// Assert
			Assert.IsFalse(result, "The town should not exist.");
		}

		[Test]
		public void RetrieveTown_Returns_Town_By_Name()
		{
			// Arrange
			var country = new Country { Id = Guid.NewGuid(), Name = "Test Country" };
			var town = new Town { Id = Guid.NewGuid(), Name = "Test Town", Country = country, CountryId = country.Id };
			db.Countries.Add(country);
			db.Towns.Add(town);
			db.SaveChanges();

			// Act
			var result = TownService.RetrieveTown("Test Town");

			// Assert
			Assert.IsNotNull(result, "The town should be found.");
			Assert.That(result.Name, Is.EqualTo(town.Name), "The names should match.");
		}

		[Test]
		public void RetrieveTown_Throws_Exception_If_Town_Not_Found()
		{
			// Arrange
			// Database is empty because of setup method

			// Act & Assert
			var ex = Assert.Throws<IndexOutOfRangeException>(() => TownService.RetrieveTown("Non-Existent Town"));
			Assert.That(ex.Message, Is.EqualTo("No such town is found!"));
		}
	}
}

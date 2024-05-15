using BusinessLayer.Models;
using DataLayer.ProjectDbContext;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.ModelsDbContext;

namespace DataLayer
{
	public class PetDbContext : IDb<Pet, Guid>
	{
		private readonly PetExchangeDbContext _dbcontext;

		public PetDbContext(PetExchangeDbContext dbcontext)
		{
			_dbcontext = dbcontext;
		}
		public void Create(Pet entity)
		{
			try
			{

				var _existingUser = _dbcontext.Users.FirstOrDefault(c => c.Id == entity.User.Id);
				if (_existingUser != null)
				{

					entity.User = _existingUser;
					entity.User.Id = _existingUser.Id;
				}

				_dbcontext.Pets.Add(entity);
				_dbcontext.SaveChanges();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		public Pet Read(Guid id, bool useNavigationalProperties = true)
		{
			Pet foundPet = _dbcontext.Pets.Where(x => x.Id == id).FirstOrDefault();
			Guid userId = foundPet.UserId;

			if (useNavigationalProperties)
			{
				foundPet.User = _dbcontext.Users.Where(x => x.Id == userId).FirstOrDefault();
			}

			return foundPet;
		}

		public List<Pet> ReadAll(bool useNavigationalProperties = true)
		{
			List<Pet> foundPets = _dbcontext.Pets.ToList();

			if (useNavigationalProperties)
			{
				for (int i = 0; i < foundPets.Count; i++)
				{
					foundPets[i] = Read(foundPets[i].Id);
				}
			}

			return foundPets;
		}

		public void Update(Pet entity, bool useNavigationalProperties = false)
		{
			try
			{
				var foundEntity = Read(entity.Id);

				if (foundEntity == null)
				{
					throw new ArgumentException("Entity with id:" + entity.Id + " doesn't exist in the database!");
				}

				_dbcontext.Entry(foundEntity).CurrentValues.SetValues(entity);
				_dbcontext.SaveChanges();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void Delete(Guid id)
		{
			try
			{
				var foundEntity = Read(id);

				if (foundEntity == null)
				{
					throw new ArgumentException("Entity with id:" + id + " doesn't exist in the database!");
				}
				_dbcontext.Pets.Remove(foundEntity);
				_dbcontext.SaveChanges();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}

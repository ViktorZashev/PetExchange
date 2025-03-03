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
	public class UserDbContext : IDb<User, Guid>
	{
		private readonly PetExchangeDbContext _dbcontext;

		public UserDbContext(PetExchangeDbContext dbcontext)
		{
			_dbcontext = dbcontext;
		}

		public void Create(User entity)
		{
			try
			{
				var existingUser = _dbcontext.Users.FirstOrDefault(x => x.Id == entity.Id);
				if (existingUser != null)
				{
					throw new ArgumentException("Entered user already exists in database!");
				}
				var _existingTown = _dbcontext.Towns.FirstOrDefault(c => c.Id == entity.Town.Id);
				if (_existingTown != null)
				{
					entity.Town = _existingTown;
				}
				else
				{
					TownDbContext townContext = new TownDbContext(_dbcontext);
					townContext.Create(entity.Town);
				}
				_dbcontext.Users.Add(entity);
				_dbcontext.SaveChanges();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public User Read(Guid id, bool useNavigationalProperties = true)
		{
			User foundUser = _dbcontext.Users.Where(x => x.Id == id).FirstOrDefault();
			if (foundUser == null) return null;
			if (useNavigationalProperties)
			{
                Guid townId = foundUser.TownId;
                foundUser.Town = _dbcontext.Towns.Where(x => x.Id == townId).FirstOrDefault();
			}
			return foundUser;
		}

		public List<User> ReadAll(bool useNavigationalProperties = true)
		{
			List<User> foundUsers = _dbcontext.Users.ToList();

			if (useNavigationalProperties)
			{
				for (int i = 0; i < foundUsers.Count; i++)
				{
					foundUsers[i] = Read(foundUsers[i].Id);
				}
			}

			return foundUsers;
		}

		public void Update(User entity, bool useNavigationalProperties = false)
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
				_dbcontext.Users.Remove(foundEntity);
				_dbcontext.SaveChanges();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public bool CheckUsernameExists(string username)
		{
			var foundEntity = ReadAll().Where(x => x.UserName == username).FirstOrDefault();

			if (foundEntity == null)
			{
				return false;
			}
			return true;
		}
	}
}

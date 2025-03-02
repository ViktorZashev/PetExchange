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
	public class UserRequestsDbContext(PetExchangeDbContext dbcontext) : IDb<UserRequests, Guid>
	{
		private readonly PetExchangeDbContext _dbcontext = dbcontext;

        public void Create(UserRequests? entity)
		{
            ArgumentNullException.ThrowIfNull(entity);
            try
			{
				var allPublicOffers = _dbcontext.PublicOffers.ToList();
				if (!allPublicOffers.Any(x => x.Id == entity.PublicOfferId))
				{
					throw new System.ArgumentException();
				}
				_dbcontext.Requests.Add(entity);
				_dbcontext.SaveChanges();
			}
			catch
			{

			}
		}

		public UserRequests? Read(Guid id, bool useNavigationalProperties = true)
		{
			UserRequests foundRequests = _dbcontext.Requests.Where(x => x.Id == id).FirstOrDefault();

			if (foundRequests == null) return null;
			return foundRequests;
		}

		public List<UserRequests> ReadAll(bool useNavigationalProperties = true)
		{
			List<UserRequests> foundRequests = _dbcontext.Requests.ToList();

			if (useNavigationalProperties)
			{
				for (int i = 0; i < foundRequests.Count; i++)
				{
					foundRequests[i] = Read(foundRequests[i].Id);
				}
			}

			return foundRequests;
		}

		public void Update(UserRequests entity, bool useNavigationalProperties = false)
		{
			try
			{
				var foundEntity = Read(entity.Id) ?? throw new ArgumentException("Entity with id:" + entity.Id + " doesn't exist in the database!");
                _dbcontext.Entry(foundEntity).CurrentValues.SetValues(entity);
				_dbcontext.SaveChanges();
			}
			catch
			{
			}
		}

		public void Delete(Guid id)
		{
			try
			{
				var foundEntity = Read(id) ?? throw new ArgumentException("Entity with id:" + id + " doesn't exist in the database!");
                _dbcontext.Requests.Remove(foundEntity);
				_dbcontext.SaveChanges();
			}
			catch 
			{

			}
		}
	}
}

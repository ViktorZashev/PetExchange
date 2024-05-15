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
	public class UserRequestsDbContext : IDb<UserRequests, Guid>
	{
		private readonly PetExchangeDbContext _dbcontext;

		public UserRequestsDbContext(PetExchangeDbContext dbcontext)
		{
			_dbcontext = dbcontext;
		}

		public void Create(UserRequests entity)
		{
			try
			{
				if(entity == null)
				{
					throw new ArgumentNullException("User Request is null");
				}
				var existingRequest = _dbcontext.Requests.Where(x => x.Id == entity.Id).FirstOrDefault();

				if(existingRequest != null)
				{
					throw new ArgumentException("User Request already exist in database!");
				}
				var _existingOffer = _dbcontext.PublicOffers.FirstOrDefault(c => c.Id == entity.PublicOffer.Id);
				if (_existingOffer == null)
				{
					throw new ArgumentException("Public Offer included in user Request doesn't exist in database!");
				}
				_dbcontext.Requests.Add(entity);
				_dbcontext.SaveChanges();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public UserRequests Read(Guid id, bool useNavigationalProperties = true)
		{
			UserRequests foundRequests = _dbcontext.Requests.Where(x => x.Id == id).FirstOrDefault();

			if (foundRequests == null) return null;
			if (useNavigationalProperties)
			{
                Guid publicOfferId = foundRequests.PublicOfferId;
                foundRequests.PublicOffer = _dbcontext.PublicOffers.Where(x => x.Id == publicOfferId).FirstOrDefault();
			}
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
				_dbcontext.Requests.Remove(foundEntity);
				_dbcontext.SaveChanges();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}

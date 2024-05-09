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
    public class UserRequestsDbContext : IDb<UserRequests,Guid>
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
				var _existingOffer = _dbcontext.PublicOffers.FirstOrDefault(c => c.Id == entity.PublicOffer.Id);
				if (_existingOffer != null)
				{

					entity.PublicOffer = _existingOffer;
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
			try
			{
				UserRequests foundRequests = _dbcontext.Requests.Where(x => x.Id == id).FirstOrDefault();
				Guid publicOfferId = foundRequests.PublicOfferId;
				if (useNavigationalProperties)
				{
					foundRequests.PublicOffer = _dbcontext.PublicOffers.Where(x => x.Id == publicOfferId).FirstOrDefault();
				}
				return foundRequests;
			}
			catch (Exception)
			{
				throw;
			}
		}

        public List<UserRequests> ReadAll(bool useNavigationalProperties = true)
        {
			try
			{
				List<UserRequests> foundRequests = _dbcontext.Requests.ToList();

				if (useNavigationalProperties)
				{
					foreach (var request in foundRequests)
					{
						Guid publicOfferId = request.PublicOfferId;
						request.PublicOffer = _dbcontext.PublicOffers.Where(x => x.Id == publicOfferId).FirstOrDefault();
					}
				}
				return foundRequests;
			}
			catch (Exception)
			{
				throw;
			}

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
            catch(Exception ex) 
            {
                    throw ex;
            }
        }
    }
}

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
    public class PublicOfferDbContext : IDb<PublicOffer, Guid>
    {
        private readonly PetExchangeDbContext _dbcontext;

        public PublicOfferDbContext(PetExchangeDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Create(PublicOffer entity)
        {
            try
            {
                _dbcontext.PublicOffers.Add(entity);
                _dbcontext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public PublicOffer Read(Guid id, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<PublicOffer> query = _dbcontext.PublicOffers;

                if (useNavigationalProperties)
                {
                    query.Include(p => p.Pet);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return query.SingleOrDefault(e => e.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PublicOffer> ReadAll(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<PublicOffer> query = _dbcontext.PublicOffers;

                if (useNavigationalProperties) 
                {
                    query.Include(p => p.Pet);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return query.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(PublicOffer entity, bool useNavigationalProperties = false)
        {
            try
            {
                var foundEntity = Read(entity.Id, false, false);

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
                var foundEntity = Read(id, false, false);

                if (foundEntity == null)
                {
                    throw new ArgumentException("Entity with id:" + id + " doesn't exist in the database!");
                }
                _dbcontext.PublicOffers.Remove(foundEntity);
                _dbcontext.SaveChanges();
            }
            catch(Exception ex) 
            {
                    throw ex;
            }
        }
    }
}

using BusinessLayer.Models;
using DataLayer.ProjectDbContext;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ModelsDbContext
{
    public class CountryDbContext : IDb<Country, Guid>
    {
        private readonly PetExchangeDbContext _dbcontext;

        // validation

        public CountryDbContext(PetExchangeDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Create(Country entity)
        {
            try
            {
                _dbcontext.Countries.Add(entity);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Country Read(Guid id, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Country> query = _dbcontext.Countries;

                if (useNavigationalProperties) // No navigational Properties Exist
                {

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

        public List<Country> ReadAll(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Country> query = _dbcontext.Countries;

                if (useNavigationalProperties) // No navigational Properties Exist
                {

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

        public void Update(Country entity, bool useNavigationalProperties = false)
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
                _dbcontext.Countries.Remove(foundEntity);
                _dbcontext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

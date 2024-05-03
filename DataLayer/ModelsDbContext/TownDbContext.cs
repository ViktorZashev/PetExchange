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
    public class TownDbContext : IDb<Town, Guid>
    {
        private readonly PetExchangeDbContext _dbcontext;

        public TownDbContext(PetExchangeDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Create(Town entity)
        {
            try
            {
                if (_dbcontext.Towns.Any(c => c.Name == entity.Name))
                {
                    
                    throw new InvalidOperationException("A town with the same name already exists.");
                }
                var _existingCountry = _dbcontext.Countries.FirstOrDefault(c => c.Id == entity.Country.Id);
                if (_existingCountry != null)
                {
                   
                    entity.Country = _existingCountry;
                }
                _dbcontext.Towns.Add(entity);
                _dbcontext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
       
        public Town Read(Guid id, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Town> query = _dbcontext.Towns;

                if (useNavigationalProperties)
                {
                    query.Include(p => p.Country);
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

        public List<Town> ReadAll(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Town> query = _dbcontext.Towns;

                if (useNavigationalProperties) 
                {
                    query.Include(p => p.Country);
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

        public void Update(Town entity, bool useNavigationalProperties = false)
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
                _dbcontext.Towns.Remove(foundEntity);
                _dbcontext.SaveChanges();
            }
            catch(Exception ex) 
            {
                    throw ex;
            }
        }
    }
}

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
                _dbcontext.Pets.Add(entity);
                _dbcontext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public Pet Read(Guid id, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Pet> query = _dbcontext.Pets;

                if (useNavigationalProperties)
                {
                    query.Include(p => p.User);
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

        public List<Pet> ReadAll(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Pet> query = _dbcontext.Pets;

                if (useNavigationalProperties) 
                {
                    query.Include(p => p.User);
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

        public void Update(Pet entity, bool useNavigationalProperties = false)
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
                _dbcontext.Pets.Remove(foundEntity);
                _dbcontext.SaveChanges();
            }
            catch(Exception ex) 
            {
                    throw ex;
            }
        }
    }
}

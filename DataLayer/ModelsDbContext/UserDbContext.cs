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
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public User Read(Guid id, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<User> query = _dbcontext.Users;

                if (useNavigationalProperties)
                {
                    query.Include(p => p.Town);
                    query.Include(p => p.Pets);
                    query.Include(p => p.Requests);
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

        public List<User> ReadAll(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<User> query = _dbcontext.Users;

                if (useNavigationalProperties) 
                {
                    query.Include(p => p.Town);
                    query.Include(p => p.Pets);
                    query.Include(p => p.Requests);
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

        public void Update(User entity, bool useNavigationalProperties = false)
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
                _dbcontext.Users.Remove(foundEntity);
                _dbcontext.SaveChanges();
            }
            catch(Exception ex) 
            {
                    throw ex;
            }
        }

        public bool CheckUsernameExists(string username)
        {
            var foundEntity = ReadAll().Where(x => x.Username == username).FirstOrDefault();

            if (foundEntity == null)
            {
                return false;
            }
            return true;
        }

        public bool CheckPasswordCorrect(string username, string password)
        {
            var foundEntity = ReadAll().Where(x => x.Username == username && x.Password == password).FirstOrDefault();

            if (foundEntity == null)
            {
                return false;
            }
            return true;
        }
    }
}

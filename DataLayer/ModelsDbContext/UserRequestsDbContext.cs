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
                _dbcontext.Requests.Add(entity);
                _dbcontext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public UserRequests Read(Guid id, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<UserRequests> query = _dbcontext.Requests;

                if (useNavigationalProperties)
                {
                    query.Include(p => p.PublicOffer);
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

        public List<UserRequests> ReadAll(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<UserRequests> query = _dbcontext.Requests;

                if (useNavigationalProperties) 
                {
                    query.Include(p => p.PublicOffer);
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

        public void Update(UserRequests entity, bool useNavigationalProperties = false)
        {
            try
            {
                var foundEntity = Read(entity.Id, false, false);

                if (foundEntity != null)
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

                if (foundEntity != null)
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

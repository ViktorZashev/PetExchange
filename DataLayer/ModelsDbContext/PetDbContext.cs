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
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataLayer
{
	public class PetDbContext(PetExchangeDbContext dbcontext) : IDbWithNav<Pet, Guid>
	{
		private readonly PetExchangeDbContext _dbcontext = dbcontext;

        public async Task CreateAsync(Pet entity)
		{
			try
			{
				await _dbcontext.Pets.AddAsync(entity);
				await _dbcontext.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}


		public async Task<Pet>? ReadAsync(Guid id, bool useNavigationalProperties = false, bool isReadOnly = true)
		{
            try
            {
                IQueryable<Pet> query = _dbcontext.Pets;
                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }
                if (useNavigationalProperties)
                {
                    query = query.Include(e => e.User);
                }
                return await query.SingleOrDefaultAsync(e => e.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

		public async Task<List<Pet>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
		{
            try
            {
                IQueryable<Pet> query = _dbcontext.Pets;

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }
                if (useNavigationalProperties)
                {
                    query = query.Include(e => e.User);
                }
                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Pet pet, bool useNavigationalProperties = true)
        {
            try
            {
                Pet petFromDb = await ReadAsync(pet.Id, useNavigationalProperties, false);

                if (petFromDb is null)
                {
                    throw new ArgumentException("Pet with id = " + pet.Id + "does not exist!");
                }
                if (useNavigationalProperties) _dbcontext.Pets.Update(pet); // updates all linked entities
                else
                {
                    _dbcontext.Pets.Entry(petFromDb).CurrentValues.SetValues(pet); // updates only the core entity
                }

                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteAsync(Guid key)
        {
            try
            {
                Pet pet = await ReadAsync(key, false, false);

                if (pet is null)
                {
                    throw new ArgumentException("Pet with id = " + key + " does not exist!");
                }

                _dbcontext.Pets.Remove(pet);
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

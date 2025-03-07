﻿using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
	public class PetDbContext : IDbWithNav<Pet, Guid>
	{
		private PetExchangeDbContext _dbcontext;
        public PetDbContext(PetExchangeDbContext context)
        {
            _dbcontext = context;
        }

        public async Task<List<Pet>> ReadAllWithFilterAsync(string name, string petType, string gender, int page = 1, int pageSize = 10, bool useNavigationalProperties = true, bool isReadOnly = true)
        {
            try
            {
                var allPets = await ReadAllAsync(useNavigationalProperties, isReadOnly);
                // filtering
                var filteredPets = allPets.Where(x =>
                        (String.IsNullOrWhiteSpace(name) || x.Name.Contains(name)
                        && (String.IsNullOrWhiteSpace(petType) || x.PetType.ToString() == petType)
                        && (String.IsNullOrWhiteSpace(gender) || x.Gender.ToString() == gender)))
                        .ToList();
                // paging
                filteredPets = filteredPets.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return filteredPets;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region CRUD
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

        public async Task CreateAsync(List<Pet> pets)
        {
            try
            {
                foreach (var pet in pets)
                {
                    await CreateAsync(pet);
                }
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
        #endregion
    }
}

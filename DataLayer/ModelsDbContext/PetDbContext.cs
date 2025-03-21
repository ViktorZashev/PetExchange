using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Data;

namespace DataLayer
{
	public class PetDbContext : IDbWithNav<Pet, Guid>
	{
		private PetExchangeDbContext _dbcontext;
		public PetDbContext(PetExchangeDbContext context)
		{
			_dbcontext = context;
		}
        public async Task<List<Pet>> ReadAllWithFilterAsync(string name, string petBreed, string petType, string gender, string ownerName, int page, int pageSize, bool useNavigationalProperties = true, bool isReadOnly = true)
        {
            try
            {
                var allPets = await ReadAllAsync(useNavigationalProperties, isReadOnly);
                // filtering
                var filteredPets = allPets.Where(x =>
                (String.IsNullOrWhiteSpace(name) || x.Name.ToLower().Contains(name.ToLower()))
                && (String.IsNullOrWhiteSpace(petBreed) || x.Breed.ToLower().Contains(petBreed.ToLower()))
                && (String.IsNullOrWhiteSpace(petType) || x.PetType.ToDescriptionString().ToLower().Contains(petType.ToLower()))
                && (String.IsNullOrWhiteSpace(gender) || x.Gender.ToDescriptionString().ToLower().Contains(gender.ToLower()))
                && (String.IsNullOrWhiteSpace(ownerName) || x.User.Name.ToLower().Contains(ownerName.ToLower()))
				).ToList();
                // paging
                filteredPets = filteredPets.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return filteredPets;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Pet>> ReadAllWithFilterAsyncOfUser(Guid userId, string name, string petBreed, string petType, string gender, int page, int pageSize, bool useNavigationalProperties = true, bool isReadOnly = true)
        {
            try
            {
                var allPets = await ReadAllAsync(useNavigationalProperties, isReadOnly);
                // filtering
                var filteredPets = allPets.Where(x =>
                (String.IsNullOrWhiteSpace(name) || x.Name.ToLower().Contains(name.ToLower()))
                && (String.IsNullOrWhiteSpace(petBreed) || x.Breed.ToLower().Contains(petBreed.ToLower()))
                && (String.IsNullOrWhiteSpace(petType) || x.PetType.ToDescriptionString().ToLower().Contains(petType.ToLower()))
                && (String.IsNullOrWhiteSpace(gender) || x.Gender.ToDescriptionString().ToLower().Contains(gender.ToLower()))
				&& x.UserId == userId
				&& x.IsActive == true
                ).ToList();
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
					query = query.Include(e => e.User).ThenInclude(e => e.Town);
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
					query = query.Include(e => e.User).ThenInclude(e => e.Town);
				}
				return await query.ToListAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<List<Pet>> ReadWithFiltersAsync(List<PetTypeEnum> types, List<GenderEnum> genders, List<PetAgeEnum> ages, bool? withCage)
		{
			try
			{
				IQueryable<Pet> query = _dbcontext.Pets;
				query = query.AsNoTrackingWithIdentityResolution();
				query = query.Include(e => e.User).ThenInclude(e => e.Town);
				query = query.Where(e => e.AdoptedOn == null && e.IsActive);
				if (types != null && types.Count > 0)
				{
					query = query.Where(e => types.Contains(e.PetType));
				}
				if (genders != null && genders.Count > 0)
				{
					query = query.Where(e => genders.Contains(e.Gender));
				}
				//Needed if only one of the age enums is requestes
				if (ages != null && ages.Count == 1)
				{
					var adultDate = DateTime.Now.AddDays(-90);
					if(ages[0] == PetAgeEnum.Young){ 
						query = query.Where(e => e.Birthday >= adultDate);
					}
					else if(ages[0] == PetAgeEnum.Adult){ 
						query = query.Where(e => e.Birthday < adultDate);
					}
					
				}
				if(withCage is not null && withCage.Value){ 
					query = query.Where(e => e.IncludesCage);
				}

				return await query.ToListAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<List<Pet>> Read4NewestAsync()
		{
			try
			{
				IQueryable<Pet> query = _dbcontext.Pets;

				query = query.AsNoTrackingWithIdentityResolution();
				query = query.Include(e => e.User).ThenInclude(e => e.Town);
				query = query.Where(e => e.AdoptedOn == null && e.IsActive);
				query = query.OrderByDescending(e => e.AddedOn);
				query = query.Take(4);
				return await query.ToListAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<List<Pet>> Read4OldestAsync()
		{
			try
			{
				IQueryable<Pet> query = _dbcontext.Pets;

				query = query.AsNoTrackingWithIdentityResolution();
				query = query.Include(e => e.User).ThenInclude(e => e.Town);
				query = query.Where(e => e.AdoptedOn == null && e.IsActive);
				query = query.OrderBy(e => e.AddedOn);
				query = query.Take(4);
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

				pet.IsActive = false;
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

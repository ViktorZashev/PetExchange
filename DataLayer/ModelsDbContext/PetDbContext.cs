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
        
        public async Task<List<Pet>> ReadAllWithFilterAsync(string name, string petBreed, string petType, 
            string gender, string ownerName, int page, int pageSize, bool useNavigationalProperties = true, 
            bool isReadOnly = true)
        {
            var allPets = await ReadAllAsync(useNavigationalProperties, isReadOnly);
            // прилагане на филтри
            var filteredPets = allPets.Where(x =>
            (String.IsNullOrWhiteSpace(name) || x.Name.ToLower().Contains(name.ToLower()))
            && (String.IsNullOrWhiteSpace(petBreed) || x.Breed.ToLower().Contains(petBreed.ToLower()))
            && (String.IsNullOrWhiteSpace(petType) || x.PetType.ToDescriptionString().ToLower().Contains(petType.ToLower()))
            && (String.IsNullOrWhiteSpace(gender) || x.Gender.ToDescriptionString().ToLower().Contains(gender.ToLower()))
            && (String.IsNullOrWhiteSpace(ownerName) || x.User.Name.ToLower().Contains(ownerName.ToLower()))
            ).ToList();
            // странициране
            filteredPets = filteredPets.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return filteredPets;
        }

        public async Task<List<Pet>> ReadAllWithFilterAsyncOfUser(Guid userId, string name, string petBreed, 
        string petType, string gender, int page, int pageSize, bool useNavigationalProperties = true, bool isReadOnly = true)
        {
            var allPets = await ReadAllAsync(useNavigationalProperties, isReadOnly);
            // прилагане на филтри
            var filteredPets = allPets.Where(x =>
            (String.IsNullOrWhiteSpace(name) || x.Name.ToLower().Contains(name.ToLower()))
            && (String.IsNullOrWhiteSpace(petBreed) || x.Breed.ToLower().Contains(petBreed.ToLower()))
            && (String.IsNullOrWhiteSpace(petType) || x.PetType.ToDescriptionString().ToLower().Contains(petType.ToLower()))
            && (String.IsNullOrWhiteSpace(gender) || x.Gender.ToDescriptionString().ToLower().Contains(gender.ToLower()))
            && x.UserId == userId
            && x.IsActive == true
            ).ToList();
            // странициране
            filteredPets = filteredPets.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return filteredPets;
        }

        public async Task<List<Pet>> ReadWithFiltersAsync(List<PetTypeEnum> types, List<GenderEnum> genders,
            List<PetAgeEnum> ages,
            bool? withCage, int page = 1, int pageSize = 8)
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
            //Необходимо е само, ако един филтър за възраст е избран
            if (ages != null && ages.Count == 1)
            {
                var adultDate = DateTime.Now.AddDays(-90);
                if (ages[0] == PetAgeEnum.Young)
                {
                    query = query.Where(e => e.Birthday >= adultDate);
                }
                else if (ages[0] == PetAgeEnum.Adult)
                {
                    query = query.Where(e => e.Birthday < adultDate);
                }
            }
            if (withCage is not null && withCage.Value)
            {
                query = query.Where(e => e.IncludesCage);
            }

            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<List<Pet>> Read4NewestAsync()
        {
            IQueryable<Pet> query = _dbcontext.Pets;

            query = query.AsNoTrackingWithIdentityResolution();
            query = query.Include(e => e.User).ThenInclude(e => e.Town);
            query = query.Where(e => e.AdoptedOn == null && e.IsActive);
            query = query.OrderByDescending(e => e.AddedOn);
            query = query.Take(4);
            return await query.ToListAsync();
        }

        public async Task<List<Pet>> Read4OldestAsync()
        {
            IQueryable<Pet> query = _dbcontext.Pets;

            query = query.AsNoTrackingWithIdentityResolution();
            query = query.Include(e => e.User).ThenInclude(e => e.Town);
            query = query.Where(e => e.AdoptedOn == null && e.IsActive);
            query = query.OrderBy(e => e.AddedOn);
            query = query.Take(4);
            return await query.ToListAsync();
        }

        #region CRUD
        public async Task CreateAsync(Pet entity)
        {
            await _dbcontext.Pets.AddAsync(entity);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task CreateAsync(List<Pet> pets)
        {
            foreach (var pet in pets)
            {
                await CreateAsync(pet);
            }
        }

        public async Task<Pet>? ReadAsync(Guid id, bool useNavigationalProperties = true, bool isReadOnly = true)
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

        public async Task<List<Pet>> ReadAllAsync(bool useNavigationalProperties = true, bool isReadOnly = true)
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

        public async Task UpdateAsync(Pet pet, bool useNavigationalProperties = true)
        {
            Pet petFromDb = await ReadAsync(pet.Id, useNavigationalProperties, false);

            if (petFromDb is null)
            {
                throw new ArgumentException("Pet with id = " + pet.Id + "does not exist!");
            }
            if (useNavigationalProperties) _dbcontext.Pets.Update(pet); // актуализира всички навигационни свойства
            else
            {
                _dbcontext.Pets.Entry(petFromDb).CurrentValues.SetValues(pet); // актуализира само текущият запис
            }

            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid key)
        {
            Pet pet = await ReadAsync(key, false, false);

            if (pet is null)
            {
                throw new ArgumentException("Pet with id = " + key + " does not exist!");
            }

            pet.IsActive = false;
            await _dbcontext.SaveChangesAsync();
        }
        #endregion
    }
}

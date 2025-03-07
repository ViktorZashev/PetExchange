using DataLayer;

namespace BusinessLayer
{
	public class PetService : IDbWithNav<Pet,Guid>
	{
		public PetDbContext _PetContext;

        public PetService(PetExchangeDbContext _ProjectContext)
        {
            _PetContext = new PetDbContext(_ProjectContext);
        }
        public async Task CreateAsync(Pet entity)
        {
            await _PetContext.CreateAsync(entity);
        }
        public async Task CreateAsync(List<Pet> pets)
        {
            await _PetContext.CreateAsync(pets);
        }

        public async Task<Pet>? ReadAsync(Guid id, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await _PetContext.ReadAsync(id,useNavigationalProperties, isReadOnly);
        }

        public async Task<List<Pet>>? ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await _PetContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task UpdateAsync(Pet entity, bool useNavigationalProperties = false)
        {
            await _PetContext.UpdateAsync(entity, useNavigationalProperties);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _PetContext.DeleteAsync(id);
        }
    }
	
}

using DataLayer;

namespace BusinessLayer
{
    public class PublicOfferService : IDbWithNav<PublicOffer, Guid>
    {
        public  PublicOfferDbContext _PublicOfferContext;

        public PublicOfferService(PetExchangeDbContext _ProjectContext)
        {
            _PublicOfferContext = new PublicOfferDbContext(_ProjectContext);
        }
        public async Task CreateAsync(PublicOffer entity)
        {
            await _PublicOfferContext.CreateAsync(entity);
        }
        public async Task CreateAsync(List<PublicOffer> offers)
        {
            await _PublicOfferContext.CreateAsync(offers);
        }
        public async Task<PublicOffer>? ReadAsync(Guid id, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await _PublicOfferContext.ReadAsync(id, useNavigationalProperties, isReadOnly);
        }

        public async Task<List<PublicOffer>>? ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await _PublicOfferContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task UpdateAsync(PublicOffer entity, bool useNavigationalProperties = false)
        {
            await _PublicOfferContext.UpdateAsync(entity, useNavigationalProperties);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _PublicOfferContext.DeleteAsync(id);
        }
    }
}

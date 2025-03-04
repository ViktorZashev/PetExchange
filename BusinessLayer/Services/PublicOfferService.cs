using BusinessLayer.Models;
using DataLayer;
using DataLayer.ModelsDbContext;
using DataLayer.ProjectDbContext;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Functions
{
    public class PublicOfferService(PetExchangeDbContext _ProjectContext) : IDbWithNav<PublicOffer, Guid>
    {
        public  PublicOfferDbContext _PublicOfferContext = new(_ProjectContext);

        public async Task CreateAsync(PublicOffer entity)
        {
            await _PublicOfferContext.CreateAsync(entity);
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

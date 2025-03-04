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
	public class PublicOfferDbContext : IDbWithNav<PublicOffer, Guid>
	{
		private readonly PetExchangeDbContext _dbcontext;

        public PublicOfferDbContext(PetExchangeDbContext context)
        {
            _dbcontext = context;
        }
        public async Task CreateAsync(PublicOffer entity)
        {
            try
            {
                await _dbcontext.PublicOffers.AddAsync(entity);
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<PublicOffer>? ReadAsync(Guid id, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<PublicOffer> query = _dbcontext.PublicOffers;
                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }
                if (useNavigationalProperties)
                {
                    query = query.Include(e => e.Pet).Include(e => e.Requests);
                }
                return await query.SingleOrDefaultAsync(e => e.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PublicOffer>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<PublicOffer> query = _dbcontext.PublicOffers;

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }
                if (useNavigationalProperties)
                {
                    query = query.Include(e => e.Pet).Include(e => e.Requests);
                }
                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(PublicOffer publicOffer, bool useNavigationalProperties = true)
        {
            try
            {
                PublicOffer publicOfferFromDb = await ReadAsync(publicOffer.Id, useNavigationalProperties, false);

                if (publicOfferFromDb is null)
                {
                    throw new ArgumentException("Public offer with id = " + publicOffer.Id + "does not exist!");
                }
                if (useNavigationalProperties) _dbcontext.PublicOffers.Update(publicOffer); // updates all linked entities
                else
                {
                    _dbcontext.PublicOffers.Entry(publicOfferFromDb).CurrentValues.SetValues(publicOffer); // updates only the core entity
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
                PublicOffer publicOffer = await ReadAsync(key, false, false);

                if (publicOffer is null)
                {
                    throw new ArgumentException("Public offer with id = " + key + " does not exist!");
                }

                _dbcontext.PublicOffers.Remove(publicOffer);
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

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
	public class PublicOfferDbContext : IDb<PublicOffer, Guid>
	{
		private readonly PetExchangeDbContext _dbcontext;

		public PublicOfferDbContext(PetExchangeDbContext dbcontext)
		{
			_dbcontext = dbcontext;
		}

		public void Create(PublicOffer entity)
		{
			try
			{
				var _existingPet = _dbcontext.Pets.FirstOrDefault(c => c.Id == entity.Pet.Id);

				if (_existingPet != null)
				{

					entity.Pet = _existingPet;
				}

				_dbcontext.PublicOffers.Add(entity);
				_dbcontext.SaveChanges();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		public PublicOffer Read(Guid id, bool useNavigationalProperties = true)
		{
			PublicOffer foundOffer = _dbcontext.PublicOffers.Where(x => x.Id == id).FirstOrDefault();
			Guid petId = foundOffer.PetId;

			if (useNavigationalProperties)
			{
				foundOffer.Pet = _dbcontext.Pets.Where(x => x.Id == petId).FirstOrDefault();
			}

			return foundOffer;
		}

		public List<PublicOffer> ReadAll(bool useNavigationalProperties = true)
		{

			List<PublicOffer> foundOffers = _dbcontext.PublicOffers.ToList();

			if (useNavigationalProperties)
			{
				for (int i = 0; i < foundOffers.Count; i++)
				{
					foundOffers[i] = Read(foundOffers[i].Id);
				}
			}
			return foundOffers;
		}

		public void Update(PublicOffer entity, bool useNavigationalProperties = false)
		{
			try
			{
				var foundEntity = Read(entity.Id);

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
				var foundEntity = Read(id);

				if (foundEntity == null)
				{
					throw new ArgumentException("Entity with id:" + id + " doesn't exist in the database!");
				}
				_dbcontext.PublicOffers.Remove(foundEntity);
				_dbcontext.SaveChanges();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}

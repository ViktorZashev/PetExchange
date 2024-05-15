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
	public class TownDbContext : IDb<Town, Guid>
	{
		private readonly PetExchangeDbContext _dbcontext;

		public TownDbContext(PetExchangeDbContext dbcontext)
		{
			_dbcontext = dbcontext;
		}

		public void Create(Town entity)
		{
			try
			{
				if (_dbcontext.Towns.Any(c => c.Name == entity.Name))
				{

					return; // A town with this name already exists
				}
				var _existingCountry = _dbcontext.Countries.FirstOrDefault(c => c.Id == entity.Country.Id);
				if (_existingCountry != null)
				{

					entity.Country = _existingCountry;
				}
				else
				{
					CountryDbContext countryContext = new CountryDbContext(_dbcontext);
					countryContext.Create(entity.Country);
				}
				_dbcontext.Towns.Add(entity);
				_dbcontext.SaveChanges();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		public Town Read(Guid id, bool useNavigationalProperties = true)
		{
			Town foundTown = _dbcontext.Towns.Where(x => x.Id == id).FirstOrDefault();
			Guid countryId = foundTown.CountryId;

			if (useNavigationalProperties)
			{
				foundTown.Country = _dbcontext.Countries.Where(x => x.Id == countryId).FirstOrDefault();
			}

			return foundTown;
		}

		public List<Town> ReadAll(bool useNavigationalProperties = true)
		{
			List<Town> foundTowns = _dbcontext.Towns.ToList();

			if (useNavigationalProperties)
			{
				for (int i = 0; i < foundTowns.Count; i++)
				{
					foundTowns[i] = Read(foundTowns[i].Id);
				}
			}
			return foundTowns;
		}

		public void Update(Town entity, bool useNavigationalProperties = true)
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
				_dbcontext.Towns.Remove(foundEntity);
				_dbcontext.SaveChanges();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public bool CheckExists(string name)
		{
			var Towns = ReadAll();
			if (Towns.Any(x => x.Name == name))
			{
				return true;
			}
			else return false;
		}
	}
}

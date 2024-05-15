using BusinessLayer.Models;
using DataLayer.ProjectDbContext;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ModelsDbContext
{
	public class CountryDbContext : IDb<Country, Guid>
	{
		private readonly PetExchangeDbContext _dbcontext;

		public CountryDbContext(PetExchangeDbContext dbcontext)
		{
			_dbcontext = dbcontext;
		}

		public void Create(Country entity)
		{
			try
			{
				_dbcontext.Countries.Add(entity);
				_dbcontext.SaveChanges();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public Country Read(Guid id, bool useNavigationalProperties = true)
		{

			Country foundCountry = _dbcontext.Countries.Where(x => x.Id == id).FirstOrDefault();

			if (useNavigationalProperties) // does nothing because there are no relations from country table
			{

			}

			return foundCountry;
		}

		public List<Country> ReadAll(bool useNavigationalProperties = true)
		{

			List<Country> foundCountries = _dbcontext.Countries.ToList();

			if (useNavigationalProperties) // does nothing because there are no relations from country table
			{

			}

			return foundCountries;
		}

		public void Update(Country entity, bool useNavigationalProperties = false)
		{
			var foundEntity = Read(entity.Id);

			if (foundEntity == null)
			{
				throw new ArgumentException("Entity with id:" + entity.Id + " doesn't exist in the database!");
			}

			_dbcontext.Entry(foundEntity).CurrentValues.SetValues(entity);
			_dbcontext.SaveChanges();
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
				_dbcontext.Countries.Remove(foundEntity);
				_dbcontext.SaveChanges();

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public Country RetrieveCountry(string name)
		{
			var Countries = ReadAll(); // Matching country names could exist

			if (Countries.Where(x => x.Name == name).Count() > 1)
			{
				throw new Exception("More than one matching countries with the same name exists!");
			}
			return Countries.Where(x => x.Name == name).FirstOrDefault();
		}
	}
}

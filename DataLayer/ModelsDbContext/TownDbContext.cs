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
	public class TownDbContext(PetExchangeDbContext dbcontext) : IDb<Town, Guid>
	{
		private readonly PetExchangeDbContext _dbcontext = dbcontext;

        public void Create(Town entity)
		{
			try
			{
				if (_dbcontext.Towns.Any(c => c.Name == entity.Name))
				{
					throw new ArgumentException("A town with this name already exists!");
				}
				_dbcontext.Towns.Add(entity);
				_dbcontext.SaveChanges();
			}
			catch
			{
				
			}
		}


        public Town? Read(Guid id, bool useNavigationalProperties = true)
		{
			Town? foundTown = _dbcontext.Towns.Where(x => x.Id == id).FirstOrDefault();

			if (foundTown == null) return null;

			return foundTown;
		}

		public List<Town> ReadAll(bool useNavigationalProperties = true)
		{
            List<Town> foundTowns = _dbcontext.Towns.ToList();
            return foundTowns;
		}

		public void Update(Town entity, bool useNavigationalProperties = true)
		{
			try
			{
				var foundEntity = Read(entity.Id) ?? throw new ArgumentException("Entity with id:" + entity.Id + " doesn't exist in the database!");
                _dbcontext.Entry(foundEntity).CurrentValues.SetValues(entity);
				_dbcontext.SaveChanges();
			}
			catch
			{
			}
		}

		public void Delete(Guid id)
		{
			try
			{
				var foundEntity = Read(id) ?? throw new ArgumentException("Entity with id:" + id + " doesn't exist in the database!");
                _dbcontext.Towns.Remove(foundEntity);
				_dbcontext.SaveChanges();
			}
			catch
			{

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

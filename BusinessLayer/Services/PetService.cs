using Azure;
using DataLayer;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Data;
using System.Drawing.Printing;

namespace BusinessLayer
{
	public class PetService : IDbWithNav<Pet,Guid>
	{
		public PetDbContext _PetContext;

        public PetService(PetExchangeDbContext _ProjectContext)
        {
            _PetContext = new PetDbContext(_ProjectContext);
        }
        public async Task<List<Pet>> ReadAllWithFilterAsync(string name,string petBreed, string petType, string gender, string ownerName,int  page,int pageSize,bool useNavigationalProperties = true, bool isReadOnly = true)
        {
            return await _PetContext.ReadAllWithFilterAsync(name, petBreed, petType, gender, ownerName, page, pageSize, useNavigationalProperties, isReadOnly);
        }
        public async Task<List<Pet>> ReadAllWithFilterAsyncOfUser(Guid userId, string name, string petBreed, string petType, string gender, int page, int pageSize, bool useNavigationalProperties = true, bool isReadOnly = true)
        {
            return await _PetContext.ReadAllWithFilterAsyncOfUser(userId,name, petBreed, petType, gender, page, pageSize, useNavigationalProperties, isReadOnly);
        }
        #region CRUD
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

        public async Task<List<Pet>> ReadWithFiltersAsync(List<PetTypeEnum> types,List<GenderEnum> genders, List<PetAgeEnum> ages, bool? withCage)
        {
            return await _PetContext.ReadWithFiltersAsync(types,genders,ages,withCage);
        }

        public async Task<List<Pet>>? Read4NewestAsync()
        {
            return await _PetContext.Read4NewestAsync();
        }

        public async Task<List<Pet>>? Read4OldestAsync()
        {
            return await _PetContext.Read4OldestAsync();
        }

        public async Task UpdateAsync(Pet entity, bool useNavigationalProperties = false)
        {
            await _PetContext.UpdateAsync(entity, useNavigationalProperties);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _PetContext.DeleteAsync(id);
        }
        #endregion
    }

}

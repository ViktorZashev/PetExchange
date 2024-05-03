using BusinessLayer.Models;
using DataLayer;
using DataLayer.ModelsDbContext;
using DataLayer.ProjectDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Functions
{
    public static class UserRequestsController
    {
        private static readonly PetExchangeDbContext _ProjectContext = new PetExchangeDbContext();
        private static readonly UserRequestsDbContext  _UserRequestsContext = new UserRequestsDbContext(_ProjectContext);

        public static void Create(UserRequests request)
        {
            // Validation
            _UserRequestsContext.Create(request);
        }
        public static UserRequests Read(Guid idt, bool useNav = false, bool isReadOnly = false)
        {
            
            return _UserRequestsContext.Read(idt, useNav,isReadOnly);
        }

        public static List<UserRequests> ReadAll(bool useNav = false, bool isReadOnly = false)
        {
  
            return _UserRequestsContext.ReadAll(useNav, isReadOnly);
        }
        public static void Update(UserRequests request)
        {
            // validation
            _UserRequestsContext.Update(request);
        }
        public static void Delete(Guid id)
        {
            // validation
            _UserRequestsContext.Delete(id);
        }
        public static void DeleteAll()
        {
            var Requests = ReadAll();
            foreach (var Request in Requests)
            {
                Delete(Request.Id);
            }
        }
    }
}

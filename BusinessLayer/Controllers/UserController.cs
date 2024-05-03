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
    public static class UserController
    {
        private static readonly PetExchangeDbContext _ProjectContext = new PetExchangeDbContext();
        private static readonly UserDbContext _UserContext = new UserDbContext(_ProjectContext);

        public static void Create(User user)
        {
            // Validation
            _UserContext.Create(user);
        }

        public static void Create(List<User> users)
        {
            foreach (var user in users)
            {
                Create(user);
            }
        }
        public static User Read(Guid idt, bool useNav = false, bool isReadOnly = false)
        {
            
            return _UserContext.Read(idt, useNav,isReadOnly);
        }

        public static List<User> ReadAll(bool useNav = false, bool isReadOnly = false)
        {
  
            return _UserContext.ReadAll(useNav, isReadOnly);
        }
        public static void Update(User user)
        {
            // validation
            _UserContext.Update(user);
        }
        public static void Delete(Guid id)
        {
            // validation
            _UserContext.Delete(id);
        }

        public static void DeleteAll()
        {
            var Users = ReadAll();
            foreach (var User in Users)
            {
                Delete(User.Id);
            }
        }
    }
}

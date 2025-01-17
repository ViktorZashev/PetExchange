﻿using BusinessLayer.Models;
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
    public static class UserRequestsService
    {
        private static readonly PetExchangeDbContext _ProjectContext = new PetExchangeDbContext();
        public static UserRequestsDbContext  _UserRequestsContext = new UserRequestsDbContext(_ProjectContext);

        public static void Create(UserRequests request)
        {
            // Validation
            _UserRequestsContext.Create(request);
        }

        public static void Create(List<UserRequests> requests)
        {
            foreach (var request in requests)
            {
                Create(request);
            }
        }
        public static UserRequests Read(Guid idt, bool useNav = true)
        {
            
            return _UserRequestsContext.Read(idt, useNav);
        }

        public static List<UserRequests> ReadAll(bool useNav = true)
        {
  
            return _UserRequestsContext.ReadAll(useNav);
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
        public static List<UserRequests> ReadAll(User user)
        {
          return ReadAll().Where(x => x.UserId == user.Id).ToList();
        }
        public static void DeleteRequest(User user, string petName)
        {
            var requests = ReadAll(user);
        
            var deletedRequest = requests.Where(x => PetService.Read(PublicOfferService.Read(x.PublicOfferId).PetId).Name == petName).FirstOrDefault();
            if (deletedRequest == null)
            {
                throw new Exception("No such request exists for this user!");
            }
            Delete(deletedRequest.Id);
        }
        public static void CreateRequest(User user, string petName)
        {
            var offer = PublicOfferService.ReadAll().Where(x => x.TownId == user.TownId).Where(x => x.Pet.Name == petName).FirstOrDefault();
            if(PetService.ReturnAllPets(user).Any(x => x.Name == petName) == true)
            {
                PrintFunctions.PrintCantRequestOwnPetMessage();
                throw new Exception("You can't create a request for a pet you own!");
            }
            if (offer == null)
            {
                throw new Exception("No such public offer exists for petName");
            }
            var request = new UserRequests(offer, user, false);
            Create(request);
        }
		public static void LoadDb()
		{
			try
			{
				Delete(Guid.NewGuid());
			}
			catch
			{

			}
		}
	}
}

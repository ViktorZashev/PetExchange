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
    public static class PublicOfferService
    {
        private static readonly PetExchangeDbContext _ProjectContext = new PetExchangeDbContext();
        private static readonly PublicOfferDbContext  _PublicOfferContext = new PublicOfferDbContext(_ProjectContext);

        public static void Create(PublicOffer offer)
        {
            // Validation
            _PublicOfferContext.Create(offer);
        }

        public static void Create(List<PublicOffer> offers)
        {
            foreach (var offer in offers)
            {
                Create(offer);
            }
        }
        public static PublicOffer Read(Guid idt, bool useNav = true)
        {
            
            return _PublicOfferContext.Read(idt, useNav);
        }

        public static List<PublicOffer> ReadAll(bool useNav = true)
        {
  
            return _PublicOfferContext.ReadAll(useNav);
        }
        public static void Update(PublicOffer offer)
        {
            // validation
            _PublicOfferContext.Update(offer);
        }
        public static void Delete(Guid id)
        {
            // validation
            _PublicOfferContext.Delete(id);
        }
        public static void DeleteByPetName(string name, User LoggedUser)
        {
            var offers = ReadAll(true);
            if (!offers.Exists(x => x.Pet.Name == name && x.Pet.UserId == LoggedUser.Id))
            {
                throw new Exception("Name doesn't match any of users pets!");
            }
            var foundOffer = offers.Where(x => x.Pet.Name == name && x.Pet.UserId == LoggedUser.Id).FirstOrDefault();
            Delete(foundOffer.Id);

		}
        public static void DeleteAll()
        {
            var Offers = ReadAll();
            foreach (var Offer in Offers)
            {
                Delete(Offer.Id);
            }
        }

		public static void RegisterPet(string petName, User loggedUser)
		{
            var offers = ReadAll();
            if (offers.Exists(x => x.Pet.Name == petName && x.Pet.UserId == loggedUser.Id))
            {
                throw new Exception("This pet is already registered!");
            }
            var pet = PetService.ReadAll().Where(x => x.UserId == loggedUser.Id && x.Name == petName).FirstOrDefault();
            if (pet == null) {
                throw new Exception("This pet doesn't exist in user's database!");
            }
            var newOffer = new PublicOffer(pet);
            Create(newOffer);
		}
	}
}

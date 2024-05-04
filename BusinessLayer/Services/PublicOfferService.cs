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
        public static PublicOffer Read(Guid idt, bool useNav = false, bool isReadOnly = false)
        {
            
            return _PublicOfferContext.Read(idt, useNav,isReadOnly);
        }

        public static List<PublicOffer> ReadAll(bool useNav = false, bool isReadOnly = false)
        {
  
            return _PublicOfferContext.ReadAll(useNav, isReadOnly);
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
        public static void DeleteAll()
        {
            var Offers = ReadAll();
            foreach (var Offer in Offers)
            {
                Delete(Offer.Id);
            }
        }
    }
}

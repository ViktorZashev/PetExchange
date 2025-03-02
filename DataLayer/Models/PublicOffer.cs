using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class PublicOffer
	{

		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();


		public Guid PetId { get; set; }

		[ForeignKey("PetId")]

		public Pet Pet { get; set; }


		public Guid TownId { get; set; }


		public Guid UserId { get; set; }

		public PublicOffer() { }

		public PublicOffer(Pet pet)
		{
			Id = Guid.NewGuid();
			Pet = pet;
			PetId = pet.Id;
			TownId = Pet.User.TownId;
			UserId = pet.UserId;
		}

	}
}

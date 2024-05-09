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
		[JsonPropertyName("id")]
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();

		[JsonPropertyName("pet_id")]
		public Guid PetId { get; set; }

		[ForeignKey("PetId")]
		[JsonPropertyName("pet")]
		public Pet Pet { get; set; }

		[JsonPropertyName("town_id")]
		public Guid TownId { get; set; }

		[JsonPropertyName("user_id")]
		public Guid UserId { get; set; }

		private PublicOffer() { }

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

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

		[JsonPropertyName("user_id")]
		[ForeignKey("User")]
		[Required]
		public Guid UserId { get; set; } = Guid.NewGuid();

		[JsonPropertyName("pet_id")]
		[ForeignKey("Pet")]
		[Required]
		public Guid PetId { get; set; } = Guid.NewGuid();

		[JsonPropertyName("town_id")]
		[ForeignKey("Town")]
		[Required]
		public Guid TownId { get; set; } = Guid.NewGuid();

        [JsonPropertyName("user")]
        public User User { get; set; }

        [JsonPropertyName("pet")]
        public Pet Pet { get; set; }

        [JsonPropertyName("town")]
        public Town Town { get; set; }

		[JsonPropertyName("is_resolved")]
		public bool IsResolved { get; set; } = false;

		private PublicOffer() { }

		public PublicOffer(Guid id,User user, Pet pet, Town town, bool isResolved)
		{
			Id = id;
			UserId = user.Id;
			PetId = pet.Id;
			TownId = town.Id;
			User = user;
			Pet = pet;
            Town = town;
			IsResolved = isResolved;
		}

	}
}

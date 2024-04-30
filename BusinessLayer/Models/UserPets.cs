using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
	internal class UserPets
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; } = Guid.NewGuid();

		[JsonPropertyName("user_id")]
		public Guid UserId { get; set; } = Guid.NewGuid();

		[JsonPropertyName("pet_id")]
		public Guid PetId { get; set; } = Guid.NewGuid();
	}
}

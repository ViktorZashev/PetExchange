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

        [JsonPropertyName("pet")]
        public Pet Pet { get; set; }

		[JsonPropertyName("is_resolved")]
		public bool IsResolved { get; set; } = false;

		private PublicOffer() { }

		public PublicOffer(Pet pet, bool isResolved)
		{
			Id = Guid.NewGuid();
			Pet = pet;
			IsResolved = isResolved;
		}

	}
}

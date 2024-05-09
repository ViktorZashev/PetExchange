using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class UserRequests
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; } = Guid.NewGuid();

		[JsonPropertyName("public_offer")]
		public PublicOffer PublicOffer { get; set; }

		[JsonPropertyName("public_offer_id")]
		public Guid PublicOfferId { get; set; }

		[JsonPropertyName("is_accepted")]
		public bool IsAccepted { get; set; } = false;

		private UserRequests() { }

		public UserRequests(PublicOffer publicOffer, bool isAccepted)
		{
			Id = Guid.NewGuid();
			PublicOffer = publicOffer;
			PublicOfferId = publicOffer.Id;
			IsAccepted = isAccepted;
		}
	}
}

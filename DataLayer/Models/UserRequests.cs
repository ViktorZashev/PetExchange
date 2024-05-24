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

		[JsonPropertyName("public_offer_id")]
		public Guid PublicOfferId { get; set; }

		[JsonPropertyName("user_id")]
		public Guid UserId { get; set; }

		[JsonPropertyName("is_accepted")]
		public bool IsAccepted { get; set; } = false;

		public UserRequests() { }

		public UserRequests(PublicOffer publicOffer, User user, bool isAccepted)
		{
			Id = Guid.NewGuid();
			PublicOfferId = publicOffer.Id;
			UserId = user.Id;
			IsAccepted = isAccepted;
		}
	}
}

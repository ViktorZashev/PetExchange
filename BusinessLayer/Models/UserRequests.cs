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

		[JsonPropertyName("offer_id")]
		[ForeignKey("PublicOffer")]
		public Guid OfferId { get; set; } = Guid.NewGuid();

		public PublicOffer PublicOffer { get; set; }

		[JsonPropertyName("user_id")]
		[ForeignKey("User")]
		public Guid UserId { get; set; } = Guid.NewGuid();

        public User User { get; set; }

        [JsonPropertyName("is_accepted")]
		public bool IsAccepted { get; set; } = false;

		private UserRequests() { }

		public UserRequests(Guid id, PublicOffer publicOffer, User user, bool isAccepted)
		{
			Id = id;
			OfferId = publicOffer.Id;
			UserId = user.Id;
			PublicOffer = publicOffer;
			User = user;
			IsAccepted = isAccepted;
		}
	}
}

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
		public PublicOffer PublicOffer { get; set; }

        [JsonPropertyName("is_accepted")]
		public bool IsAccepted { get; set; } = false;

		private UserRequests() { }

		public UserRequests(Guid id, PublicOffer publicOffer, bool isAccepted)
		{
			Id = id;
			PublicOffer = publicOffer;
			IsAccepted = isAccepted;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
	internal class UserRequests
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; } = Guid.NewGuid();

		[JsonPropertyName("offer_id")]
		public Guid OfferId { get; set; } = Guid.NewGuid();

		[JsonPropertyName("user_id")]
		public Guid UserId { get; set; } = Guid.NewGuid();

		[JsonPropertyName("is_accepted")]
		public bool IsAccepted { get; set; } = false;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
	internal class User
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; } = Guid.NewGuid();

		[JsonPropertyName("town_id")]
		public Guid UserId { get; set; } = Guid.NewGuid();

		[JsonPropertyName("name")]
		public string Name { get; set; } = string.Empty;

		[JsonPropertyName("photo_path")] // Only for Web View
		public string PhotoPath { get; set; } = string.Empty;

		[JsonPropertyName("is_admin")]
		public bool isAdmin { get; set; } = false;

		[JsonPropertyName("contact_info")]
		public string ContactInfo { get; set; } = string.Empty;

		[JsonPropertyName("username")]
		public string Username { get; set; } = string.Empty;

		[JsonPropertyName("password")]
		public string Password { get; set; } = string.Empty;
	}
}

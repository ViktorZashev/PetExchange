using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class User
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; } = Guid.NewGuid();

		[JsonPropertyName("town")]
		public Town Town { get; set; }

		[JsonPropertyName("pets")]
		public List<Pet> Pets { get; set; }

        [JsonPropertyName("requests")]
        public List<UserRequests> Requests { get; set; }

        [JsonPropertyName("name")]
		public string Name { get; set; } = string.Empty;

		[JsonPropertyName("photo_path")] // Only for Web View
		public string PhotoPath { get; set; } = string.Empty;

		[JsonPropertyName("is_admin")]
		public bool IsAdmin { get; set; } = false;

		[JsonPropertyName("contact_info")]
		public string ContactInfo { get; set; } = string.Empty;

		[JsonPropertyName("username")]
		public string Username { get; set; } = string.Empty;

		[JsonPropertyName("password")]
		public string Password { get; set; } = string.Empty;

		private User() {
			Pets = new List<Pet>();
			Requests = new List<UserRequests>();
		
		}

		public User(Town town, List<Pet> pets, List<UserRequests> requests, string name, string photo_path, bool isAdmin, string contactInfo, string username, string password)
		{
			Id = Guid.NewGuid();
			Town = town;
			Pets = pets;
			Requests = requests;
			Name = name;
			PhotoPath = photo_path;
			IsAdmin = isAdmin;
			ContactInfo = contactInfo;
			Username = username;
			Password = password;
		}
	}
}

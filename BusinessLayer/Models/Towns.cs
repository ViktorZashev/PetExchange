using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
	internal class Towns
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; } = Guid.NewGuid();

		[JsonPropertyName("country_id")]
		public Guid CountryId { get; set; } = Guid.NewGuid();

		[JsonPropertyName("name")]
		public string Name { get; set; } = string.Empty;
	}
}

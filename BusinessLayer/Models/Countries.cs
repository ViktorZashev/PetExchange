using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
	internal class Countries
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; } = Guid.NewGuid();

		[JsonPropertyName("name")]
		public string Name { get; set; } = string.Empty;
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
	public class Country
	{
		[JsonPropertyName("id")]
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();

		[JsonPropertyName("name")]
		[Required]
		public string Name { get; set; } = string.Empty;

		private Country() { }

		public Country(Guid id, string name)
		{
			Id = id;
			Name = name;
		}
	}
}

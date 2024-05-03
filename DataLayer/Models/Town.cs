using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class Town
	{
		[JsonPropertyName("id")]
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();

		public Country Country { get; set; }

		[JsonPropertyName("name")]
		[Required]
		public string Name { get; set; } = string.Empty;

		private Town() { }

		public Town(Guid id, Country country, string name)
		{
			Id = id;
			Country = country;
			Name = name;
		}
	}
}
